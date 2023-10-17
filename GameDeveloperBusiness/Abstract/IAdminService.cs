using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto.Admin.Login;
using GameDeveloperEntity.Dto.Admin.Register;

namespace GameDeveloperBusiness.Abstract
{
    public interface IAdminService
    {
        int Register(AdminRegisterRequestDto adminRegisterRequestDto);
        Admin Login(AdminLoginRequestDto adminLoginRequestDto);
        List<Admin> GetAdmins();
        int UpdateAdmin(Admin admin);
        List<User> GetUsers();
    }
}
