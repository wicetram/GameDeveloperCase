namespace GameDeveloperEntity.Dto.Admin.Login
{
    public class AdminLoginResponseDto
    {
        public ProcessResult? Result { get; set; }
        public AdminLoginResponseData? Data { get; set; }
    }

    public class AdminLoginResponseData
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
