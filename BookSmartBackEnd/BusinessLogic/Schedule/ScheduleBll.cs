using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase.Models;
using BookSmartBackEndDatabase.Repositories;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ScheduleBll(IUserRepository userRepository, IScheduleRepository scheduleRepository) : IScheduleBll
    {
        public void CreateSchedule(PostScheduleModel data)
        {
            userRepository.GetStaffUser(data.StaffUserId);

            Schedule schedule = new Schedule
            {
                SCHEDULE_ID = Guid.NewGuid(),
                SCHEDULE_USERID = data.StaffUserId,
                SCHEDULE_DAYOFWEEK = data.DayOfWeek,
                SCHEDULE_STARTTIME = data.StartTime,
                SCHEDULE_ENDTIME = data.EndTime,
                SCHEDULE_ACTIVE = true,
                SCHEDULE_CREATED = DateTime.UtcNow,
                SCHEDULE_UPDATED = DateTime.UtcNow,
                SCHEDULE_DELETED = false
            };

            scheduleRepository.Add(schedule);
        }

        public void CreateBulkSchedules(List<PostScheduleModel> data)
        {
            if (data.Count == 0)
            {
                throw new ArgumentException("No schedules provided.");
            }

            Guid userId = data[0].StaffUserId;
            if (data.Any(d => d.StaffUserId != userId))
            {
                throw new ArgumentException("All schedules must belong to the same user.");
            }

            userRepository.GetStaffUser(userId);

            DateTime now = DateTime.UtcNow;

            IEnumerable<Schedule> schedules = data.Select(entry => new Schedule
            {
                SCHEDULE_ID = Guid.NewGuid(),
                SCHEDULE_USERID = userId,
                SCHEDULE_DAYOFWEEK = entry.DayOfWeek,
                SCHEDULE_STARTTIME = entry.StartTime,
                SCHEDULE_ENDTIME = entry.EndTime,
                SCHEDULE_ACTIVE = true,
                SCHEDULE_CREATED = now,
                SCHEDULE_UPDATED = now,
                SCHEDULE_DELETED = false
            });

            scheduleRepository.AddRange(schedules);
        }

        public ScheduleResponse? GetSchedule(Guid scheduleId)
        {
            Schedule? schedule = scheduleRepository.GetById(scheduleId);

            if (schedule == null) return null;

            return MapToResponse(schedule);
        }

        public List<ScheduleResponse> GetSchedulesByStaff(Guid staffUserId)
        {
            return scheduleRepository.GetByStaff(staffUserId)
                .Select(s => MapToResponse(s))
                .ToList();
        }

        public void UpdateSchedule(Guid scheduleId, PostScheduleModel data)
        {
            Schedule schedule = scheduleRepository.GetById(scheduleId)
                ?? throw new ArgumentException("Schedule not found.");

            schedule.SCHEDULE_DAYOFWEEK = data.DayOfWeek;
            schedule.SCHEDULE_STARTTIME = data.StartTime;
            schedule.SCHEDULE_ENDTIME = data.EndTime;
            schedule.SCHEDULE_UPDATED = DateTime.UtcNow;

            scheduleRepository.Update(schedule);
        }

        public void DeleteSchedule(Guid scheduleId)
        {
            Schedule schedule = scheduleRepository.GetById(scheduleId)
                ?? throw new ArgumentException("Schedule not found.");

            schedule.SCHEDULE_DELETED = true;
            schedule.SCHEDULE_ACTIVE = false;
            schedule.SCHEDULE_UPDATED = DateTime.UtcNow;

            scheduleRepository.Update(schedule);
        }

        private static ScheduleResponse MapToResponse(Schedule schedule)
        {
            return new ScheduleResponse
            {
                ScheduleId = schedule.SCHEDULE_ID,
                UserId = schedule.SCHEDULE_USERID,
                DayOfWeek = schedule.SCHEDULE_DAYOFWEEK,
                StartTime = schedule.SCHEDULE_STARTTIME,
                EndTime = schedule.SCHEDULE_ENDTIME,
                Active = schedule.SCHEDULE_ACTIVE
            };
        }
    }
}
