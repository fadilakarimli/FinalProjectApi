using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Plan;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _repository;
        private readonly IMapper _mapper;
        public PlanService(IPlanRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task CreateAsync(PlanCreateDto model)
        {
            // Eyni TourId və Day ilə plan varmı?
            var existingPlans = await _repository.GetAllAsync();
            bool isDuplicate = existingPlans.Any(p => p.TourId == model.TourId && p.Day == model.Day);

            if (isDuplicate)
                throw new Exception("Bu gün artıq bu tura əlavə olunub.");

            var plan = _mapper.Map<Plan>(model);
            await _repository.CreateAsync(plan);
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) throw new Exception("Plan tapılmadı");
            await _repository.DeleteAsync(plan);
        }
        public async Task EditAsync(int id, PlanEditDto dto)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) throw new Exception("Plan tapılmadı");

            var existingPlans = await _repository.GetAllAsync();

            Console.WriteLine($"Edit attempt: TourId={dto.TourId}, Day={dto.Day}, PlanId={id}");
            foreach (var p in existingPlans)
            {
                Console.WriteLine($"Existing plan: Id={p.Id}, TourId={p.TourId}, Day={p.Day}");
            }

            bool isDuplicate = existingPlans.Any(p => p.TourId == dto.TourId && p.Day == dto.Day && p.Id != id);

            if (isDuplicate)
                throw new Exception("Bu gun artiq bu tura elave olunub.");

            _mapper.Map(dto, plan);
            await _repository.EditAsync(plan);
        }


        public async Task<IEnumerable<PlanDto>> GetAllAsync()
        {
            var plans = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlanDto>>(plans);
        }
        public async Task<PlanDto> GetByIdAsync(int id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) throw new Exception("Plan tapılmadı");
            return _mapper.Map<PlanDto>(plan);
        }
    }
}
