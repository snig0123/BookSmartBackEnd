using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase.Models;
using BookSmartBackEndDatabase.Repositories;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ScheduleOverrideBll(IUserRepository userRepository, IScheduleOverrideRepository scheduleOverrideRepository) : IScheduleOverrideBll
    {
        public void CreateScheduleOverride(PostScheduleOverrideModel data)
        {
            userRepository.GetStaffUser(data.UserId);

            ScheduleOverride scheduleOverride = new ScheduleOverride
            {
                SCHEDULEOVERRIDE_ID = Guid.NewGuid(),
                SCHEDULEOVERRIDE_USERID = data.UserId,
                SCHEDULEOVERRIDE_DATE = data.Date,
                SCHEDULEOVERRIDE_STARTTIME = data.StartTime,
                SCHEDULEOVERRIDE_ENDTIME = data.EndTime,
                SCHEDULEOVERRIDE_ISAVAILABLE = data.IsAvailable,
                SCHEDULEOVERRIDE_CREATED = DateTime.UtcNow,
                SCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow,
                SCHEDULEOVERRIDE_DELETED = false
            };

            scheduleOverrideRepository.Add(scheduleOverride);
        }

        public ScheduleOverrideResponse? GetScheduleOverride(Guid scheduleOverrideId)
        {
            ScheduleOverride? scheduleOverride = scheduleOverrideRepository.GetById(scheduleOverrideId);

            if (scheduleOverride == null) return null;

            return MapToResponse(scheduleOverride);
        }

        public List<ScheduleOverrideResponse> GetScheduleOverridesByStaff(Guid staffUserId)
        {
            return scheduleOverrideRepository.GetByStaff(staffUserId)
                .Select(s => MapToResponse(s))
                .ToList();
        }

        public void UpdateScheduleOverride(Guid scheduleOverrideId, PostScheduleOverrideModel data)
        {
            ScheduleOverride scheduleOverride = scheduleOverrideRepository.GetById(scheduleOverrideId)
                ?? throw new ArgumentException("Schedule override not found.");

            scheduleOverride.SCHEDULEOVERRIDE_DATE = data.Date;
            scheduleOverride.SCHEDULEOVERRIDE_STARTTIME = data.StartTime;
            scheduleOverride.SCHEDULEOVERRIDE_ENDTIME = data.EndTime;
            scheduleOverride.SCHEDULEOVERRIDE_ISAVAILABLE = data.IsAvailable;
            scheduleOverride.SCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow;

            scheduleOverrideRepository.Update(scheduleOverride);
        }

        public void DeleteScheduleOverride(Guid scheduleOverrideId)
        {
            ScheduleOverride scheduleOverride = scheduleOverrideRepository.GetById(scheduleOverrideId)
                ?? throw new ArgumentException("Schedule override not found.");

            scheduleOverride.SCHEDULEOVERRIDE_DELETED = true;
            scheduleOverride.SCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow;

            scheduleOverrideRepository.Update(scheduleOverride);
        }

        private static ScheduleOverrideResponse MapToResponse(ScheduleOverride scheduleOverride)
        {
            return new ScheduleOverrideResponse
            {
                ScheduleOverrideId = scheduleOverride.SCHEDULEOVERRIDE_ID,
                UserId = scheduleOverride.SCHEDULEOVERRIDE_USERID,
                Date = scheduleOverride.SCHEDULEOVERRIDE_DATE,
                IsAvailable = scheduleOverride.SCHEDULEOVERRIDE_ISAVAILABLE,
                StartTime = scheduleOverride.SCHEDULEOVERRIDE_STARTTIME,
                EndTime = scheduleOverride.SCHEDULEOVERRIDE_ENDTIME
            };
        }
    }
}
