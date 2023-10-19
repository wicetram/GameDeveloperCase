using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.Admin.Update
{
    public class AdminUpdateRequestDto : IDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool Active { get; set; }
    }
}
