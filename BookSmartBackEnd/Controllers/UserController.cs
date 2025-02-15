using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
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
        public ActionResult Login(string email, string password)
        {
            return Ok(_userBll.LoginUser(email, password));
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