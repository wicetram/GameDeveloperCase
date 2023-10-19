using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.Admin.Login
{
    public class AdminLoginRequestDto : IDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
