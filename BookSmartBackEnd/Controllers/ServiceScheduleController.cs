using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServiceScheduleController(IServiceScheduleBll serviceScheduleBll, ILogger<ServiceScheduleController> logger) : ControllerBase
    {
        [Authorize(Policy = "Staff")]
        [HttpPost(Name = "AddServiceToSchedule")]
        public ActionResult AddServiceToSchedule(PostServiceScheduleModel data)
        {
            serviceScheduleBll.AddServiceToSchedule(data);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        [HttpDelete(Name = "RemoveServiceFromSchedule")]
        public ActionResult RemoveServiceFromSchedule(Guid serviceId, Guid scheduleId)
        {
            serviceScheduleBll.RemoveServiceFromSchedule(serviceId, scheduleId);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetServicesBySchedule")]
        public ActionResult<List<ServiceResponse>> GetServicesBySchedule(Guid scheduleId)
        {
            List<ServiceResponse> services = serviceScheduleBll.GetServicesBySchedule(scheduleId);
            return Ok(services);
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetSchedulesByService")]
        public ActionResult<List<ScheduleResponse>> GetSchedulesByService(Guid serviceId)
        {
            List<ScheduleResponse> schedules = serviceScheduleBll.GetSchedulesByService(serviceId);
            return Ok(schedules);
        }

        [Authorize(Policy = "Staff")]
        [HttpPost(Name = "AddServiceToScheduleOverride")]
        public ActionResult AddServiceToScheduleOverride(PostServiceScheduleOverrideModel data)
        {
            serviceScheduleBll.AddServiceToScheduleOverride(data);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        [HttpDelete(Name = "RemoveServiceFromScheduleOverride")]
        public ActionResult RemoveServiceFromScheduleOverride(Guid serviceId, Guid scheduleOverrideId)
        {
            serviceScheduleBll.RemoveServiceFromScheduleOverride(serviceId, scheduleOverrideId);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetServicesByScheduleOverride")]
        public ActionResult<List<ServiceResponse>> GetServicesByScheduleOverride(Guid scheduleOverrideId)
        {
            List<ServiceResponse> services = serviceScheduleBll.GetServicesByScheduleOverride(scheduleOverrideId);
            return Ok(services);
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetScheduleOverridesByService")]
        public ActionResult<List<ScheduleOverrideResponse>> GetScheduleOverridesByService(Guid serviceId)
        {
            List<ScheduleOverrideResponse> overrides = serviceScheduleBll.GetScheduleOverridesByService(serviceId);
            return Ok(overrides);
        }
    }
}
