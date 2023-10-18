namespace GameDeveloperEntity.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Active { get; set; }
        public int PVP { get; set; }
        public decimal Level { get; set; }
        public int Rank { get; set; }
        public decimal PlayTime { get; set; }
    }
}
