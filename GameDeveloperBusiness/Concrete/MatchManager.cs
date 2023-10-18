using GameDeveloperBusiness.Abstract;
using GameDeveloperCore.Util;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete.EntityFramework.Context;
using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto;
using GameDeveloperEntity.Dto.User.Matchmaking;

namespace GameDeveloperBusiness.Concrete
{
    public class MatchManager : IMatchService
    {
        private readonly IQuequeDal _quequeDal;
        private readonly IJwtService<User> _jwtService;
        
        private const int MatchmakingThreshold = 5;

        public MatchManager(IQuequeDal quequeDal, IJwtService<User> jwtService)
        {
            _quequeDal = quequeDal;
            _jwtService = jwtService;
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

        private static User? FindOpponent(int userId, decimal userLevel, int userRank, decimal userPlayTime)
        {
            try
            {
                var eligibleUsers = GetEligibleUsersForMatchmaking(userLevel, userRank, userPlayTime);
                if (eligibleUsers != null && eligibleUsers.Count > 0)
                {
                    foreach (var potentialOpponent in eligibleUsers)
                    {
                        if (potentialOpponent.Id == userId)
                        {
                            continue;
                        }

                        if (IsMatched(userLevel, potentialOpponent.Level))
                        {
                            return potentialOpponent;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static List<User>? GetEligibleUsersForMatchmaking(decimal userLevel, int userRank, decimal userPlayTime)
        {
            try
            {
                using var context = new SimpleContextDb();
                var eligibleUsers = context.User?
                    .Where(u => u.PVP > 0 && u.Level >= userLevel && u.Rank >= userRank && u.PlayTime <= userPlayTime)
                    .ToList();

                if (eligibleUsers != null && eligibleUsers.Count > 0)
                {
                    return eligibleUsers;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool IsMatched(decimal userLevel, decimal opponentLevel)
        {
            return Math.Abs(userLevel - opponentLevel) <= MatchmakingThreshold;
        }

        public MatchmakingResponseDto Matchmaking(MatchmakingRequestDto matchmakingRequestDto, string token)
        {
            try
            {
                if (!VerifyUserWithJwt(token))
                {
                    return new MatchmakingResponseDto { Result = ErrorHandler("Token hatası."), Data = null };
                }
                else
                {
                    using var context = new SimpleContextDb();
                    var user = context.User?.Find(matchmakingRequestDto.Id);
                    if (user != null)
                    {
                        if (user.Active > 0)
                        {
                            if (user.PVP > 0)
                            {
                                var opponent = FindOpponent(user.Id, user.Level, user.Rank, user.PlayTime);
                                if (opponent != null)
                                {
                                    return new MatchmakingResponseDto
                                    {
                                        Result = SuccessHandler(),
                                        Data = new MatchmakingResponseData
                                        {
                                            Id = opponent.Id,
                                            Level = opponent.Level
                                        }
                                    };
                                }
                                else
                                {
                                    return new MatchmakingResponseDto { Result = ErrorHandler("Kullanıcı bulunamadı."), Data = null };
                                }
                            }
                            else
                            {
                                return new MatchmakingResponseDto { Result = ErrorHandler("PVP özelliği aktif değil."), Data = null };
                            }
                        }
                        else
                        {
                            return new MatchmakingResponseDto { Result = ErrorHandler("Aktif değil."), Data = null };
                        }
                    }
                    else
                    {
                        return new MatchmakingResponseDto { Result = ErrorHandler(), Data = null };
                    }
                }
            }
            catch (Exception ex)
            {
                return new MatchmakingResponseDto() { Result = ExceptionHandler(ex), Data = null };
            }
        }
    }
}
