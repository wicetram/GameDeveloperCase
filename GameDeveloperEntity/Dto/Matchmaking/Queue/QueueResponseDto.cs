namespace GameDeveloperEntity.Dto.Matchmaking.Queue
{
    public class QueueResponseDto
    {
        public ProcessResult? Result { get; set; }
        public QuequeResponseData? Data { get; set; }
    }

    public class QuequeResponseData
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public decimal Level { get; set; }
        public int Rank { get; set; }
        public decimal PlayTime { get; set; }
    }
}
