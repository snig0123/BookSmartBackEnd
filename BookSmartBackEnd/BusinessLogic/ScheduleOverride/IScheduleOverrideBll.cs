using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IScheduleOverrideBll
    {
        void CreateScheduleOverride(PostScheduleOverrideModel data);
        ScheduleOverrideResponse? GetScheduleOverride(Guid overrideId);
        List<ScheduleOverrideResponse> GetScheduleOverridesByStaff(Guid staffUserId);
        void UpdateScheduleOverride(Guid overrideId, PostScheduleOverrideModel data);
        void DeleteScheduleOverride(Guid overrideId);
    }
}
