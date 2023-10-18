using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto.Admin.Login;
using GameDeveloperEntity.Dto.Admin.Register;
using GameDeveloperEntity.Dto.Admin.Update;

namespace GameDeveloperBusiness.Abstract
{
    public interface IAdminService
    {
        AdminRegisterResponseDto Register(AdminRegisterRequestDto adminRegisterRequestDto);
        AdminLoginResponseDto Login(AdminLoginRequestDto adminLoginRequestDto);
        List<Admin> GetAdmins();
        AdminUpdateResponseDto UpdateAdmin(AdminUpdateRequestDto adminUpdateRequestDto);
        List<User> GetUsers();
    }
}
