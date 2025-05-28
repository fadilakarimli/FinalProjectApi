using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.SpecialOffer;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SpecialOfferModelService : ISpecialOfferModelService
    {
        private readonly ISpecialOfferModelRepository _modelRepository;
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly IMapper _mapper;
        public SpecialOfferModelService(ISpecialOfferModelRepository modelRepository,
                                   ICloudinaryManager cloudinaryManager, IMapper mapper)
        {
            _cloudinaryManager = cloudinaryManager;
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(SpecialOfferCreateDto model)
        {
            var offer = _mapper.Map<SpecialOfferModel>(model);

            offer.BackgroundImageUrl = await _cloudinaryManager.FileCreateAsync(model.BackgroundImageUrl);
            offer.DiscountImageUrl = await _cloudinaryManager.FileCreateAsync(model.DiscountImageUrl);
            offer.BagImageUrl = await _cloudinaryManager.FileCreateAsync(model.BagImageUrl);

            await _modelRepository.CreateAsync(offer);
        }

        public async Task DeleteAsync(int id)
        {
            var offer = await _modelRepository.GetByIdAsync(id);
            if (offer == null) throw new Exception("Special offer not found");
            await _modelRepository.DeleteAsync(offer);
        }

        public async Task EditAsync(int id, SpecialOfferEditDto model)
        {
            var offer = await _modelRepository.GetByIdAsync(id);
            if (offer == null) throw new Exception("Special offer not found");

            _mapper.Map(model, offer);

            if (model.BackgroundImageUrl != null)
                offer.BackgroundImageUrl = await _cloudinaryManager.FileCreateAsync(model.BackgroundImageUrl);

            if (model.DiscountImageUrl != null)
                offer.DiscountImageUrl = await _cloudinaryManager.FileCreateAsync(model.DiscountImageUrl);

            if (model.BagImageUrl != null)
                offer.BagImageUrl = await _cloudinaryManager.FileCreateAsync(model.BagImageUrl);

            await _modelRepository.EditAsync(offer);
        }
        public async Task<IEnumerable<SpecialOfferDto>> GetAllAsync()
        {
            var offers = await _modelRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SpecialOfferDto>>(offers);
        }

        public async Task<SpecialOfferDto> GetByIdAsync(int id)
        {
            var offer = await _modelRepository.GetByIdAsync(id);
            if (offer == null) throw new Exception("Special offer not found");

            return _mapper.Map<SpecialOfferDto>(offer);
        }

    }
}
