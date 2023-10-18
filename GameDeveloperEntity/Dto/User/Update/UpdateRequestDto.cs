namespace GameDeveloperEntity.Dto.User.Update
{
    public class UpdateRequestDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool Active { get; set; }
        public bool PVP { get; set; }
    }
}
