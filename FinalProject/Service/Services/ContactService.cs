using AutoMapper;
using Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Contact;
using Service.Helpers;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly EmailSettings _emailSettings;
        public ContactService(IContactRepository contactRepository, IMapper mapper, IOptions<EmailSettings> emailSettings)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _emailSettings = emailSettings.Value;

        }

        public async Task CreateAsync(ContactCreateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var contact = _mapper.Map<Contact>(model);
            contact.CreatedDate = DateTime.UtcNow;

            await _contactRepository.CreateAsync(contact);
            await _contactRepository.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var contact = await _contactRepository.GetByIdAsync(id.Value);
            if (contact == null) throw new NotFoundException("Contact not found.");

            await _contactRepository.DeleteAsync(contact);
            await _contactRepository.SaveChanges();
        }

        public async Task<IEnumerable<ContactDto>> GetAllAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);
        }

        public async Task<ContactDto> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var contact = await _contactRepository.GetByIdAsync(id.Value);
            if (contact == null) throw new NotFoundException("Contact not found.");

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task ReplyAsync(int contactId, string replyMessage)
        {
            var contact = await _contactRepository.GetByIdAsync(contactId);
            if (contact == null) throw new NotFoundException("Contact not found.");

            string toEmail = contact.Email;
            string toName = contact.FullName;

            string subject = "Reply to your message";
            string htmlBody = $@"
                <p>Dear {toName},</p>
                <p>{replyMessage}</p>
                <br/>
                <p>Best regards,<br/>Admin Team</p>";

            await SendEmailAsync(toEmail, subject, htmlBody);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.FromAddress));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
