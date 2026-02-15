using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ScheduleController(IScheduleBll scheduleBll, ILogger<ScheduleController> logger) : ControllerBase
    {
        [Authorize(Policy = "Staff")]
        [HttpPost(Name = "CreateSchedule")]
        public ActionResult CreateSchedule(PostScheduleModel data)
        {
            scheduleBll.CreateSchedule(data);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        [HttpPost(Name = "CreateBulkSchedules")]
        public ActionResult CreateBulkSchedules(List<PostScheduleModel> data)
        {
            scheduleBll.CreateBulkSchedules(data);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetSchedule")]
        public ActionResult<ScheduleResponse> GetSchedule(Guid scheduleId)
        {
            ScheduleResponse? schedule = scheduleBll.GetSchedule(scheduleId);

            if (schedule == null) return NotFound();

            return Ok(schedule);
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetSchedulesByStaff")]
        public ActionResult<List<ScheduleResponse>> GetSchedulesByStaff(Guid staffUserId)
        {
            List<ScheduleResponse> schedules = scheduleBll.GetSchedulesByStaff(staffUserId);
            return Ok(schedules);
        }

        [Authorize(Policy = "Staff")]
        [HttpPut(Name = "UpdateSchedule")]
        public ActionResult UpdateSchedule(Guid scheduleId, PostScheduleModel data)
        {
            scheduleBll.UpdateSchedule(scheduleId, data);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        [HttpDelete(Name = "DeleteSchedule")]
        public ActionResult DeleteSchedule(Guid scheduleId)
        {
            scheduleBll.DeleteSchedule(scheduleId);
            return Ok();
        }
    }
}