using GameDeveloperBusiness.Abstract;
using GameDeveloperCore.Util;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete.EntityFramework.Context;
using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto;
using GameDeveloperEntity.Dto.Admin.Login;
using GameDeveloperEntity.Dto.Admin.Register;
using GameDeveloperEntity.Dto.Admin.Update;

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

        private static Admin CreateUser(AdminRegisterRequestDto adminRegisterRequestDto)
        {
            return new Admin
            {
                Email = adminRegisterRequestDto.Email,
                Password = adminRegisterRequestDto.Password,
                Active = 1
            };
        }

        private static ProcessResult ErrorHandler(string message = "")
        {
            return ProcessResultHandler.ErrorHandler(message);
        }

        private static ProcessResult ExceptionHandler(Exception ex)
        {
            return ProcessResultHandler.ExceptionHandler(ex);
        }

        private static ProcessResult SuccessHandler()
        {
            return ProcessResultHandler.SuccessHandler();
        }

        public AdminRegisterResponseDto Register(AdminRegisterRequestDto adminRegisterRequestDto)
        {
            try
            {
                var user = CreateUser(adminRegisterRequestDto);
                if (string.IsNullOrEmpty(user.Email))
                {
                    return new AdminRegisterResponseDto
                    {
                        Result = ErrorHandler(),
                        Data = null
                    };
                }
                else
                {
                    var result = _adminDal.Add(user);
                    if (result > 0)
                    {
                        return new AdminRegisterResponseDto
                        {
                            Data = new AdminRegisterResponseData
                            {
                                Email = user.Email,
                                Password = user.Password,
                                Active = user.Active > 0
                            },
                            Result = SuccessHandler()
                        };
                    }
                    else
                    {
                        return new AdminRegisterResponseDto
                        {
                            Result = ErrorHandler(),
                            Data = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new AdminRegisterResponseDto
                {
                    Result = ExceptionHandler(ex),
                    Data = null
                };
            }
        }

        public AdminLoginResponseDto Login(AdminLoginRequestDto adminLoginRequestDto)
        {
            try
            {
                using var context = new SimpleContextDb();
                var admin = context.Admin?.SingleOrDefault(u => u.Email == adminLoginRequestDto.Email);

                if (admin != null)
                {
                    if (admin.Password == adminLoginRequestDto.Password)
                    {
                        if (admin.Active == 1)
                        {
                            return new AdminLoginResponseDto
                            {
                                Result = SuccessHandler(),
                                Data = new AdminLoginResponseData
                                {
                                    Email = admin.Email,
                                    Password = admin.Password,
                                    Id = admin.Id
                                }
                            };
                        }
                        else
                        {
                            return new AdminLoginResponseDto
                            {
                                Result = ErrorHandler("Hesap pasif durumda"),
                                Data = null
                            };
                        }
                    }
                    else
                    {
                        return new AdminLoginResponseDto
                        {
                            Result = ErrorHandler("Şifre hatalı"),
                            Data = null
                        };
                    }
                }
                else
                {
                    return new AdminLoginResponseDto
                    {
                        Result = ErrorHandler(),
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new AdminLoginResponseDto
                {
                    Result = ExceptionHandler(ex),
                    Data = null
                };
            }
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

        public AdminUpdateResponseDto UpdateAdmin(AdminUpdateRequestDto adminUpdateRequestDto)
        {
            try
            {
                using var context = new SimpleContextDb();
                var update = context.Admin?.Find(adminUpdateRequestDto.Id);
                if (update != null)
                {
                    update.Email = adminUpdateRequestDto.Email;
                    update.Password = adminUpdateRequestDto.Password;
                    update.Active = adminUpdateRequestDto.Active ? 1 : 0;

                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        return new AdminUpdateResponseDto
                        {
                            Result = SuccessHandler(),
                            Data = new AdminUpdateResponseData
                            {
                                Active = update.Active > 0,
                                Email = update.Email,
                                Password = update.Password
                            }
                        };
                    }
                    else
                    {
                        return new AdminUpdateResponseDto
                        {
                            Result = ErrorHandler(),
                            Data = null
                        };
                    }
                }
                else
                {
                    return new AdminUpdateResponseDto
                    {
                        Result = ErrorHandler(),
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new AdminUpdateResponseDto
                {
                    Result = ExceptionHandler(ex),
                    Data = null
                };
            }
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
