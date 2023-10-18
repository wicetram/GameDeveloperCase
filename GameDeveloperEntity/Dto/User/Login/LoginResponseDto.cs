namespace GameDeveloperEntity.Dto.User.Login
{
    public class LoginResponseDto
    {
        public ProcessResult? Result { get; set; }
        public LoginResponseData? Data { get; set; }
    }
    public class LoginResponseData
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
        public bool PVP { get; set; }
        public string? Token { get; set; }
        public int Rank { get; set; }
        public decimal PlayTime { get; set; }
    }
}
