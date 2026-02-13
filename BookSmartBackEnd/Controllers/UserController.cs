using System.Security.Claims;
using BookSmartBackEnd.BusinessLogic.Interfaces;
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
        public ActionResult<Models.GET.UserProfile> Login(string email, string password)
        {
            var result = _userBll.LoginUser(email, password);

            if (result == null)
            {
                return Unauthorized();
            }

            Response.Cookies.Append("token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.Now.AddMinutes(60)
            });

            return Ok(result.Profile);
        }

        [HttpPost(Name = "Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok();
        }

        [HttpGet(Name = "GetUserProfile")]
        [Authorize]
        public ActionResult<Models.GET.UserProfile> GetUserProfile()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var profile = _userBll.GetUserProfile(userId);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

    }
}