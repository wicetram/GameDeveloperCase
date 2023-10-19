namespace GameDeveloperEntity.Dto.User.Inventory.Add
{
    public class AddInventoryResponseDto
    {
        public ProcessResult? Result { get; set; }
        public AddInventoryResponseData? Data { get; set; }
    }

    public class AddInventoryResponseData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string? ItemType { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
    }
}
