using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers
{
    [ApiController]
    [Authorize(Policy = "Staff")]
    [Route("[controller]/[action]")]
    public class StaffController(IStaffBll staffBll, ILogger<StaffController> logger) : ControllerBase
    {
        [Authorize(Policy = "Admin")]
        [HttpPost(Name = "Create")]
        public ActionResult Create(PostRegisterModel data)
        {
            staffBll.RegisterUser(data);
            return Ok();
        }

    }
}