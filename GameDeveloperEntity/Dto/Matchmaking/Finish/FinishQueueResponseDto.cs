namespace GameDeveloperEntity.Dto.Matchmaking.Finish
{
    public class FinishQueueResponseDto
    {
        public ProcessResult? Result { get; set; }
        public FinishQueueResponseData? Data { get; set; }
    }

    public class FinishQueueResponseData
    {
        public int Id { get; set; }
        public decimal GainedLevel { get; set; }
        public decimal GainedRank { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Active { get; set; }
    }
}
