using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.Admin.Register
{
    public class AdminRegisterRequestDto : IDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
