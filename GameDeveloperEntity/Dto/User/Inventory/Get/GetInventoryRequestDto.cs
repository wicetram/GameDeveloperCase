using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.User.Inventory.Get
{
    public class GetInventoryRequestDto : IDto
    {
        public int UserId { get; set; }
    }
}
