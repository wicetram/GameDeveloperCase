using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.User.Update
{
    public class UpdateRequestDto : IDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool Active { get; set; }
        public bool PVP { get; set; }
    }
}
