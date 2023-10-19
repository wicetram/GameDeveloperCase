namespace GameDeveloperEntity.Dto.User.Inventory.Get
{
    public class GetInventoryResponseDto
    {
        public ProcessResult? Result { get; set; }
        public List<GetInventoryResponseData>? Data { get; set; }
    }

    public class GetInventoryResponseData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string? ItemType { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
    }
}
