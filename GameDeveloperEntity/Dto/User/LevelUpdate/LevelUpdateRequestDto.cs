using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.User.LevelUpdate
{
    public class LevelUpdateRequestDto : IDto
    {
        public int Id { get; set; }
        public decimal Level { get; set; }
        public int Rank { get; set; }
        public decimal PlayTime { get; set; }
    }
}
