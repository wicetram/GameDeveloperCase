namespace GameDeveloperEntity.Dto.User.LevelUpdate
{
    public class LevelUpdateResponseDto
    {
        public ProcessResult? Result { get; set; }
        public LevelUpdateResponseData? Data { get; set; }
    }

    public class LevelUpdateResponseData
    {
        public int Id { get; set; }
        public decimal Level { get; set; }
        public int Rank { get; set; }
        public decimal PlayTime { get; set; }
    }
}
