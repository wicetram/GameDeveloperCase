namespace GameDeveloperEntity.Dto.User.Register
{
    public class RegisterRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int PVP { get; set; }
    }
}
