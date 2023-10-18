namespace GameDeveloperEntity.Dto.User.Update
{
    public class UpdateResponseDto
    {
        public ProcessResult? Result { get; set; }
        public UpdateResponseData? Data { get; set; }
    }
    public class UpdateResponseData
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
        public bool PVP { get; set; }
    }
}
