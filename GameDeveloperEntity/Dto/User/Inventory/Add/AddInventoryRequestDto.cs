using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.User.Inventory.Add
{
    public class AddInventoryRequestDto : IDto
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string? ItemType { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
    }
}
