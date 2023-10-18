namespace GameDeveloperEntity.Dto.Admin.Update
{
    public class AdminUpdateResponseDto
    {
        public ProcessResult? Result { get; set; }
        public AdminUpdateResponseData? Data { get; set; }
    }

    public class AdminUpdateResponseData
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
    }
}
