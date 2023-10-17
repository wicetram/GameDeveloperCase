using GameDeveloperBusiness.Abstract;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete.EntityFramework.Context;
using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto.User.Login;
using GameDeveloperEntity.Dto.User.Register;

namespace GameDeveloperBusiness.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IJwtService<User> _jwtService;

        public UserManager(IUserDal userDal, IJwtService<User> jwtService)
        {
            _userDal = userDal;
            _jwtService = jwtService;
        }

        private static User CreateUser(RegisterRequestDto registerRequestDto)
        {
            return new User
            {
                Email = registerRequestDto.Email,
                Password = registerRequestDto.Password,
                Active = 1,
                PVP = registerRequestDto.PVP
            };
        }

        public User Login(LoginRequestDto loginrequestdto)
        {
            var result = new User();
            try
            {
                using var context = new SimpleContextDb();
                var users = context.User?.SingleOrDefault(u => u.Email == loginrequestdto.Email);

                if (users != null)
                {
                    if (users.Password == loginrequestdto.Password)
                    {
                        string jwtToken = _jwtService.GenerateToken(users);
                        result.Token = jwtToken;
                        result.Email = users.Email;
                        result.Password = users.Password;
                        result.Active = users.Active;
                        result.PVP = users.PVP;
                        result.Id = users.Id;
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public int UpdateUser(User user)
        {
            if (!VerifyUserWithJwt(user))
            {
                throw new UnauthorizedAccessException("Kimlik doğrulama başarısız.");
            }
            else
            {
                using var context = new SimpleContextDb();
                var update = context.User?.Find(user.Id) ?? throw new InvalidOperationException("Kullanıcı bulunamadı");

                update.Email = user.Email;
                update.Password = user.Password;
                update.Active = user.Active;
                update.PVP = user.PVP;

                return context.SaveChanges();
            }
        }

        private bool VerifyUserWithJwt(User user)
        {
            bool isVerified = _jwtService.ValidateToken(user.Token, out User payload);

            if (isVerified)
            {
                return true;
            }

            return false;
        }

        public int Register(RegisterRequestDto registerRequestDto)
        {
            int result = 0;
            try
            {
                var users = CreateUser(registerRequestDto);
                result = _userDal.Add(users);
            }
            catch (Exception)
            {

            }
            return result;
        }
    }
}
