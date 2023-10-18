using GameDeveloperBusiness.Abstract;
using GameDeveloperCore.Util;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete.EntityFramework.Context;
using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto;
using GameDeveloperEntity.Dto.User.LevelUpdate;
using GameDeveloperEntity.Dto.User.Login;
using GameDeveloperEntity.Dto.User.Matchmaking;
using GameDeveloperEntity.Dto.User.Register;
using GameDeveloperEntity.Dto.User.Update;

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
                Level = 1,
                PVP = registerRequestDto.PVP ? 1 : 0,
                PlayTime = 0,
                Rank = 1
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

        private bool VerifyUserWithJwt(string token)
        {
            bool isVerified = _jwtService.ValidateToken(token, out User payload);

            if (isVerified)
            {
                return true;
            }

            return false;
        }

        public LoginResponseDto Login(LoginRequestDto loginrequestdto)
        {
            try
            {
                using var context = new SimpleContextDb();
                var users = context.User?.SingleOrDefault(u => u.Email == loginrequestdto.Email);

                if (users != null)
                {
                    if (users.Password == loginrequestdto.Password)
                    {
                        if (users.Active > 0)
                        {
                            string jwtToken = _jwtService.GenerateToken(users);
                            if (string.IsNullOrEmpty(jwtToken))
                            {
                                return new LoginResponseDto
                                {
                                    Result = ErrorHandler("Token oluşturulamadı"),
                                    Data = null
                                };
                            }
                            else
                            {
                                return new LoginResponseDto
                                {
                                    Data = new LoginResponseData
                                    {
                                        Token = jwtToken,
                                        Email = users.Email,
                                        Password = users.Password,
                                        Active = users.Active > 0,
                                        PVP = users.PVP > 0,
                                        Id = users.Id,
                                        PlayTime = users.PlayTime,
                                        Rank = users.Rank
                                    },
                                    Result = SuccessHandler()
                                };
                            }
                        }
                        else
                        {
                            return new LoginResponseDto
                            {
                                Result = ErrorHandler("Aktif durumda değil"),
                                Data = null
                            };
                        }
                    }
                    else
                    {
                        return new LoginResponseDto
                        {
                            Result = ErrorHandler(),
                            Data = null
                        };
                    }
                }
                else
                {
                    return new LoginResponseDto { Result = ErrorHandler(), Data = null };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponseDto
                {
                    Result = ExceptionHandler(ex),
                    Data = null
                };
            }
        }

        public UpdateResponseDto UpdateUser(UpdateRequestDto updateRequestDto, string token)
        {
            try
            {
                if (!VerifyUserWithJwt(token))
                {
                    return new UpdateResponseDto { Result = ErrorHandler("Token hatası."), Data = null };
                }
                else
                {
                    using var context = new SimpleContextDb();
                    var update = context.User?.Find(updateRequestDto.Id);
                    if (update != null)
                    {
                        update.Email = updateRequestDto.Email;
                        update.Password = updateRequestDto.Password;
                        update.Active = updateRequestDto.Active ? 1 : 0;
                        update.PVP = updateRequestDto.PVP ? 1 : 0;

                        var response = context.SaveChanges();
                        if (response > 0)
                        {
                            return new UpdateResponseDto
                            {
                                Result = SuccessHandler(),
                                Data = new UpdateResponseData
                                {
                                    Email = update.Email,
                                    Password = update.Password,
                                    Active = update.Active > 0,
                                    PVP = update.PVP > 0
                                }
                            };
                        }
                        else
                        {
                            return new UpdateResponseDto { Result = ErrorHandler(), Data = null };
                        }
                    }
                    else
                    {
                        return new UpdateResponseDto { Result = ErrorHandler(), Data = null };
                    }
                }
            }
            catch (Exception ex)
            {
                return new UpdateResponseDto { Result = ExceptionHandler(ex), Data = null };
            }
        }

        public RegisterResponseDto Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                var users = CreateUser(registerRequestDto);
                if (!string.IsNullOrEmpty(users.Email))
                {
                    var response = _userDal.Add(users);
                    if (response > 0)
                    {
                        return new RegisterResponseDto
                        {
                            Result = SuccessHandler(),
                            Data = new RegisterResponseData
                            {
                                Email = users.Email,
                                Password = users.Password,
                                PVP = users.PVP > 0
                            }
                        };
                    }
                    else
                    {
                        return new RegisterResponseDto { Result = ErrorHandler(), Data = null };
                    }
                }
                else
                {
                    return new RegisterResponseDto { Result = ErrorHandler(), Data = null };
                }
            }
            catch (Exception ex)
            {
                return new RegisterResponseDto() { Result = ExceptionHandler(ex), Data = null };
            }
        }

        public LevelUpdateResponseDto LevelUpdate(LevelUpdateRequestDto levelUpdateRequestDto)
        {
            try
            {
                using var context = new SimpleContextDb();
                var update = context.User?.Find(levelUpdateRequestDto.Id);
                if (update != null)
                {
                    update.Level = levelUpdateRequestDto.Level;
                    update.PlayTime = levelUpdateRequestDto.PlayTime;
                    update.Rank = levelUpdateRequestDto.Rank;

                    var response = context.SaveChanges();
                    if (response > 0)
                    {
                        return new LevelUpdateResponseDto
                        {
                            Result = SuccessHandler(),
                            Data = new LevelUpdateResponseData
                            {
                                Id = update.Id,
                                Level = update.Level,
                                PlayTime = update.PlayTime,
                                Rank = update.Rank,
                            }
                        };
                    }
                    else
                    {
                        return new LevelUpdateResponseDto { Result = ErrorHandler(), Data = null };
                    }
                }
                else
                {
                    return new LevelUpdateResponseDto { Result = ErrorHandler(), Data = null };
                }
            }
            catch (Exception ex)
            {
                return new LevelUpdateResponseDto() { Result = ExceptionHandler(ex), Data = null };
            }
        }
    }
}
