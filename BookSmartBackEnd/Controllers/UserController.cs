using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBll _userBll;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBll userBll, ILogger<UserController> logger)
        {
            _userBll = userBll;
            _logger = logger;
        }

        [HttpPost(Name = "Register")]
        public ActionResult Register(PostRegisterModel data)
        {
            _userBll.RegisterUser(data);
            return Ok();
        }

        [HttpGet(Name = "Login")]
        public ActionResult<Models.GET.LoginResponse> Login(string email, string password)
        {
            string token = _userBll.LoginUser(email, password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new Models.GET.LoginResponse { Token = token });
        }

        [HttpGet(Name = "GetUserProfile")]
        [Authorize]
        public ActionResult<Models.GET.UserProfile> GetUserProfile(Guid userId)
        {
            var profile = _userBll.GetUserProfile(userId);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        [HttpGet(Name = "GetSomething")]
        public ActionResult<string> GetSomething()
        {
            return "value123";
        }

        [HttpGet(Name = "GetSomethingAuth")]
        [Authorize]
        public ActionResult<string> GetSomethingAuth()
        {
            return "valueAuth123";
        }
    }
}