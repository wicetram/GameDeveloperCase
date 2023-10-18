namespace GameDeveloperEntity.Dto.User.Register
{
    public class RegisterResponseDto
    {
        public ProcessResult? Result { get; set; }
        public RegisterResponseData? Data { get; set; }
    }

    public class RegisterResponseData
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool PVP { get; set; }
    }
}
