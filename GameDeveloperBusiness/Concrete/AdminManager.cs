using GameDeveloperBusiness.Abstract;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete;
using GameDeveloperDataAccess.Concrete.EntityFramework.Context;
using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto.Admin.Login;
using GameDeveloperEntity.Dto.Admin.Register;

namespace GameDeveloperBusiness.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminDal _adminDal;
        private readonly IUserDal _userDal;

        public AdminManager(IAdminDal adminDal, IUserDal userDal)
        {
            _adminDal = adminDal;
            _userDal = userDal;
        }

        public int Register(AdminRegisterRequestDto adminRegisterRequestDto)
        {
            int result = 0;
            try
            {
                var user = CreateUser(adminRegisterRequestDto);
                result = _adminDal.Add(user);
            }
            catch (Exception)
            {
            }
            return result;
        }

        private static Admin CreateUser(AdminRegisterRequestDto adminRegisterRequestDto)
        {
            return new Admin
            {
                Email = adminRegisterRequestDto.Email,
                Password = adminRegisterRequestDto.Password,
                Active = 1
            };
        }

        public Admin Login(AdminLoginRequestDto adminLoginRequestDto)
        {
            var result = new Admin();
            try
            {
                using var context = new SimpleContextDb();
                var admin = context.Admin?.SingleOrDefault(u => u.Email == adminLoginRequestDto.Email);

                if (admin != null)
                {
                    if (admin.Password == adminLoginRequestDto.Password)
                    {
                        result.Id = admin.Id;
                        result.Email = admin.Email;
                        result.Password = admin.Password;
                        result.Active = admin.Active;
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public List<Admin> GetAdmins()
        {
            try
            {
                return _adminDal.GetAll();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int UpdateAdmin(Admin admin)
        {
            using var context = new SimpleContextDb();
            var update = context.Admin?.Find(admin.Id) ?? throw new InvalidOperationException("Admin bulunamadı");

            update.Email = admin.Email;
            update.Password = admin.Password;
            update.Active = admin.Active;

            return context.SaveChanges();
        }

        public List<User> GetUsers()
        {
            try
            {
                return _userDal.GetAll();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
