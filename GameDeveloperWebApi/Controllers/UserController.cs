using GameDeveloperBusiness.Abstract;
using GameDeveloperEntity.Dto.User.LevelUpdate;
using GameDeveloperEntity.Dto.User.Login;
using GameDeveloperEntity.Dto.User.Matchmaking;
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
        private readonly IMatchService _matchService;

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

        public UserController(IUserService userService, IMatchService matchService)
        {
            _userService = userService;
            _matchService = matchService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public object Register(RegisterRequestDto registerRequestDto)
        {
            return _userService.Register(registerRequestDto);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
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
        [Route("matchMaking")]
        public object Matchmaking(MatchmakingRequestDto matchmakingRequestDto)
        {
            if (!string.IsNullOrEmpty(Authentication))
            {
                return _matchService.Matchmaking(matchmakingRequestDto, Authentication);
            }
            return Unauthorized();
        }

    }
}
