using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.Matchmaking.Finish
{
    public class FinishQueueRequestDto : IDto
    {
        public int WinnerId { get; set; }
        public int GainedRank { get; set; }
        public decimal GainedLevel { get; set; }
    }
}
