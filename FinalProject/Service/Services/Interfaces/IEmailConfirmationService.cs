using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEmailConfirmationService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
