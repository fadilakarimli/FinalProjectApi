using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Activity;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepo;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepo, IMapper mapper)
        {
            _activityRepo = activityRepo;
            _mapper = mapper;
        }
        public async Task CreateAsync(ActivityCreateDto model)
        {
            var activity = _mapper.Map<Activity>(model);
            await _activityRepo.CreateAsync(activity);
        }
        public async Task DeleteAsync(int id)
        {
            var activity = await _activityRepo.GetWithExpressionAsync(x => x.Id == id);
            if (activity == null) throw new Exception("Activity not found");
            await _activityRepo.DeleteAsync(activity);
        }

        public async Task EditAsync(int id, ActivityEditDto model)
        {
            var activity = await _activityRepo.GetByIdAsync(id);
            if (activity == null) throw new Exception("Activity not found");

            _mapper.Map(model, activity);
            await _activityRepo.EditAsync(activity);
        }

        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            var activities = await _activityRepo.GetAllAsync();
            return _mapper.Map<List<ActivityDto>>(activities);
        }

        public async Task<ActivityDto> GetByIdAsync(int id)
        {
            var activity = await _activityRepo.GetByIdAsync(id);
            if (activity == null) throw new Exception("Activity not found");

            return _mapper.Map<ActivityDto>(activity);
        }

     
    }
}
