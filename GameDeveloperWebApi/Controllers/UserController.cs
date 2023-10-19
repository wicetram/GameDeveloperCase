using GameDeveloperBusiness.Abstract;
using GameDeveloperEntity.Dto.User.Inventory.Add;
using GameDeveloperEntity.Dto.User.Inventory.Get;
using GameDeveloperEntity.Dto.User.LevelUpdate;
using GameDeveloperEntity.Dto.User.Login;
using GameDeveloperEntity.Dto.User.Register;
using GameDeveloperEntity.Dto.User.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameDeveloperWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private string Authentication
        {
            get
            {
                string result = "";
                try
                {
                    if (Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                    {
                        var bearerToken = authorizationHeader.ToString();
                        if (bearerToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            var token = bearerToken["Bearer ".Length..];
                            if (!string.IsNullOrEmpty(token))
                            {
                                result = token;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }
        }


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

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public object Login(LoginRequestDto loginRequestDto)
        {
            return _userService.Login(loginRequestDto);
        }

        [HttpPost]
        [Route("Update")]
        public object Update(UpdateRequestDto updateRequestDto)
        {
            if (!string.IsNullOrEmpty(Authentication))
            {
                return _userService.UpdateUser(updateRequestDto, Authentication);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("level")]
        public object Level(LevelUpdateRequestDto levelUpdateRequestDto)
        {
            return _userService.LevelUpdate(levelUpdateRequestDto);
        }

        [HttpPost]
        [Route("getInventory")]
        public object GetInventory(GetInventoryRequestDto getInventoryRequestDto)
        {
            if (!string.IsNullOrEmpty(Authentication))
            {
                return _userService.GetUserInventory(getInventoryRequestDto, Authentication);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("addInventory")]
        public object AddInventory(AddInventoryRequestDto addInventoryRequestDto)
        {
            if (!string.IsNullOrEmpty(Authentication))
            {
                return _userService.AddInventory(addInventoryRequestDto, Authentication);
            }
            return Unauthorized();
        }
    }
}
