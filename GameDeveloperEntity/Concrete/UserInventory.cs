namespace GameDeveloperEntity.Concrete
{
    public class UserInventory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string? ItemType { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
    }
}
