using GameDeveloperBusiness.Abstract;
using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto.User.Login;
using GameDeveloperEntity.Dto.User.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameDeveloperWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public object Register(RegisterRequestDto registerRequestDto)
        {
            return _userService.Register(registerRequestDto);
        }

        [Authorize]
        [HttpPost]
        [Route("Login")]
        public object Login(LoginRequestDto loginRequestDto)
        {
            return _userService.Login(loginRequestDto);
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]
        public object Update(User user)
        {
            return _userService.UpdateUser(user);
        }
    }
}
