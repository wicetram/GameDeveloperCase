using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.User.Login
{
    public class LoginRequestDto : IDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
