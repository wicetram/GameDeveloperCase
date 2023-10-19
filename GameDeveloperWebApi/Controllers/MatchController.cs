using GameDeveloperBusiness.Abstract;
using GameDeveloperEntity.Dto.Matchmaking.Finish;
using GameDeveloperEntity.Dto.Matchmaking.Queue;
using Microsoft.AspNetCore.Mvc;

namespace GameDeveloperWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
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

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost]
        [Route("matchMaking")]
        public object Matchmaking(QueueRequestDto queueRequestDto)
        {
            if (!string.IsNullOrEmpty(Authentication))
            {
                return _matchService.Matchmaking(queueRequestDto, Authentication);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("finish")]
        public object Finish(FinishQueueRequestDto finishQueueRequestDto)
        {
            if (!string.IsNullOrEmpty(Authentication))
            {
                return _matchService.FinishMatch(finishQueueRequestDto, Authentication);
            }
            return Unauthorized();
        }
    }
}
