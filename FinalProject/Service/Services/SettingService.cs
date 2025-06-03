using AutoMapper;
using Domain.Entities;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Setting;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _repo;
        private readonly IMapper _mapper;

        public SettingService(ISettingRepository repo,
                              IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<DataOperationResponse> CreateAsync(SettingCreateDto setting)
        {
            var hasData = await _repo.CheckDataWithExpression(x => x.Key.ToLower().Trim() == setting.Key.ToLower().Trim());
            if (hasData) throw new AlreadyCreatedException(ExceptionMessages.AlreadyCreatedMessage);
            var result = _mapper.Map<Setting>(setting);
            await _repo.CreateAsync(result);
            return new DataOperationResponse
            {
                Success = true,
                Message = "Successfully Created"
            };
        }

        public async Task<DataOperationResponse> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id); 
            if (entity == null)
            {
                return new DataOperationResponse
                {
                    Success = false,
                    Message = "Entity not found"
                };
            }

            await _repo.DeleteAsync(entity);
            return new DataOperationResponse
            {
                Success = true,
                Message = "Successfully Deleted"
            };
        }

        public async Task<DataOperationResponse> EditAsync(int id, SettingEditDto setting)
        {
            var data = await _repo.GetByIdAsync(id);
            if (setting.Key != data.Key)
            {
                var hasData = await _repo.CheckDataWithExpression(x => x.Key.ToLower().Trim() == setting.Key.ToLower().Trim());
                if (hasData) throw new AlreadyCreatedException(ExceptionMessages.AlreadyCreatedMessage);
            }
            _mapper.Map(setting, data);

            await _repo.EditAsync(data);
            return new DataOperationResponse
            {
                Success = true,
                Message = "Successfully Updated"
            };
        }

        public async Task<IEnumerable<SettingDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<SettingDto>>(await _repo.GetAllAsync());
        }

        public async Task<SettingDto> GetByIdAsync(int id)
        {
            return _mapper.Map<SettingDto>(await _repo.GetByIdAsync(id));
        }
    }
}
