using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IScheduleBll
    {
        void CreateSchedule(PostScheduleModel data);
        void CreateBulkSchedules(List<PostScheduleModel> data);
        ScheduleResponse? GetSchedule(Guid scheduleId);
        List<ScheduleResponse> GetSchedulesByStaff(Guid staffUserId);
        void UpdateSchedule(Guid scheduleId, PostScheduleModel data);
        void DeleteSchedule(Guid scheduleId);
    }
}