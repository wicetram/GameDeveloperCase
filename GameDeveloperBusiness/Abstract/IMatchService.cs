using GameDeveloperEntity.Dto.User.Matchmaking;

namespace GameDeveloperBusiness.Abstract
{
    public interface IMatchService
    {
        MatchmakingResponseDto Matchmaking(MatchmakingRequestDto matchmakingRequestDto, string token);
    }
}
