namespace GameDeveloperEntity.Dto.User.Matchmaking
{
    public class MatchmakingResponseDto
    {
        public ProcessResult? Result { get; set; }
        public MatchmakingResponseData? Data { get; set; }
    }

    public class MatchmakingResponseData
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public decimal Level { get; set; }
        public bool PVP { get; set; }
    }
}
