using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServiceController(IServiceBll serviceBll, ILogger<ServiceController> logger) : ControllerBase
    {
        [Authorize(Policy = "Staff")]
        [HttpPost(Name = "CreateService")]
        public ActionResult CreateService(PostServiceModel data)
        {
            serviceBll.CreateService(data);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetService")]
        public ActionResult<ServiceResponse> GetService(Guid serviceId, bool excludeUnavailable = false)
        {
            ServiceResponse? service = serviceBll.GetService(serviceId, excludeUnavailable);

            if (service == null) return NotFound();

            return Ok(service);
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetServicesByBusiness")]
        public ActionResult<List<ServiceResponse>> GetServicesByBusiness(Guid businessId, bool excludeUnavailable = false)
        {
            List<ServiceResponse> services = serviceBll.GetServicesByBusiness(businessId, excludeUnavailable);
            return Ok(services);
        }

        [Authorize(Policy = "Staff")]
        [HttpPut(Name = "UpdateService")]
        public ActionResult UpdateService(Guid serviceId, PostServiceModel data)
        {
            serviceBll.UpdateService(serviceId, data);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        [HttpDelete(Name = "DeleteService")]
        public ActionResult DeleteService(Guid serviceId)
        {
            serviceBll.DeleteService(serviceId);
            return Ok();
        }
    }
}