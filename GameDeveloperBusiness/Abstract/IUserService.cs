using GameDeveloperEntity.Dto.User.Inventory.Add;
using GameDeveloperEntity.Dto.User.Inventory.Get;
using GameDeveloperEntity.Dto.User.LevelUpdate;
using GameDeveloperEntity.Dto.User.Login;
using GameDeveloperEntity.Dto.User.Register;
using GameDeveloperEntity.Dto.User.Update;

namespace GameDeveloperBusiness.Abstract
{
    public interface IUserService
    {
        RegisterResponseDto Register(RegisterRequestDto registerRequestDto);
        LoginResponseDto Login(LoginRequestDto loginrequestdto);
        UpdateResponseDto UpdateUser(UpdateRequestDto updateRequestDto, string token);
        LevelUpdateResponseDto LevelUpdate(LevelUpdateRequestDto levelUpdateRequestDto);
        GetInventoryResponseDto GetUserInventory(GetInventoryRequestDto inventoryRequestDto, string token);
        AddInventoryResponseDto AddInventory(AddInventoryRequestDto addInventoryRequestDto, string token);
    }
}
