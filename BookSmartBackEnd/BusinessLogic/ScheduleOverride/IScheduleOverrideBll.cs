using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IScheduleOverrideBll
    {
        void CreateScheduleOverride(PostScheduleOverrideModel data);
        ScheduleOverrideResponse? GetScheduleOverride(Guid scheduleOverrideId);
        List<ScheduleOverrideResponse> GetScheduleOverridesByStaff(Guid staffUserId);
        void UpdateScheduleOverride(Guid scheduleOverrideId, PostScheduleOverrideModel data);
        void DeleteScheduleOverride(Guid scheduleOverrideId);
    }
}
