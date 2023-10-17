using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto.User.Login;
using GameDeveloperEntity.Dto.User.Register;

namespace GameDeveloperBusiness.Abstract
{
    public interface IUserService
    {
        int Register(RegisterRequestDto registerRequestDto);
        User Login(LoginRequestDto loginrequestdto);
        int UpdateUser(User user);
    }
}
