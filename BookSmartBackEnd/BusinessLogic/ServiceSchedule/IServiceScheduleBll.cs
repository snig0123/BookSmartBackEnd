using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IServiceScheduleBll
    {
        // Schedule associations
        void AddServiceToSchedule(PostServiceScheduleModel data);
        void RemoveServiceFromSchedule(Guid serviceId, Guid scheduleId);
        List<ServiceResponse> GetServicesBySchedule(Guid scheduleId);
        List<ScheduleResponse> GetSchedulesByService(Guid serviceId);

        // ScheduleOverride associations
        void AddServiceToScheduleOverride(PostServiceScheduleOverrideModel data);
        void RemoveServiceFromScheduleOverride(Guid serviceId, Guid scheduleOverrideId);
        List<ServiceResponse> GetServicesByScheduleOverride(Guid scheduleOverrideId);
        List<ScheduleOverrideResponse> GetScheduleOverridesByService(Guid serviceId);
    }
}
