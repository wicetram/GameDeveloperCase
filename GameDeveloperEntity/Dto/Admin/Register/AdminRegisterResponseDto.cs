namespace GameDeveloperEntity.Dto.Admin.Register
{
    public class AdminRegisterResponseDto
    {
        public ProcessResult? Result { get; set; }
        public AdminRegisterResponseData? Data { get; set; }
    }

    public class AdminRegisterResponseData
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
    }
}
