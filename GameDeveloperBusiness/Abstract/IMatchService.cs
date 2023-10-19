using GameDeveloperEntity.Dto.Matchmaking.Finish;
using GameDeveloperEntity.Dto.Matchmaking.Queue;
using GameDeveloperEntity.Dto.Matchmaking.Queue;

namespace GameDeveloperBusiness.Abstract
{
    public interface IMatchService
    {
        QueueResponseDto Matchmaking(QueueRequestDto quequeRequestDto, string token);

        FinishQueueResponseDto FinishMatch(FinishQueueRequestDto finishQuequeRequestDto, string token);
    }
}
