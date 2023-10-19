using GameDeveloperBusiness.Abstract;
using GameDeveloperCore.Util;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete.EntityFramework.Context;
using GameDeveloperEntity.Concrete;
using GameDeveloperEntity.Dto;
using GameDeveloperEntity.Dto.Matchmaking.Finish;
using GameDeveloperEntity.Dto.Matchmaking.Queue;

namespace GameDeveloperBusiness.Concrete
{
    public class MatchManager : IMatchService
    {
        private readonly IQueueDal _quequeDal;
        private readonly IJwtService<User> _jwtService;

        public MatchManager(IQueueDal quequeDal, IJwtService<User> jwtService)
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

        public QueueResponseDto Matchmaking(QueueRequestDto quequeRequestDto, string token)
        {
            try
            {
                if (!VerifyUserWithJwt(token))
                {
                    return new QueueResponseDto { Result = ErrorHandler("Token hatası."), Data = null };
                }
                else
                {
                    using var context = new SimpleContextDb();
                    var user = context.User?.FirstOrDefault(u => u.Id == quequeRequestDto.Id);
                    if (user != null)
                    {
                        if (user.Active > 0)
                        {
                            if (user.PVP > 0)
                            {
                                var hasQueue = context.Queue?.FirstOrDefault(u => u.HomeUserId == user.Id && u.Active == 1);
                                if (hasQueue != null)
                                {
                                    return new QueueResponseDto { Result = ErrorHandler("Kullanıcı için aktif bir maç var"), Data = null };
                                }

                                // Diğer oyuncuları sıralama puanlarına göre sırala
                                var potentialOpponents = context.User?.Where(u => u.Id != quequeRequestDto.Id && u.Active == 1 && u.PVP > 0)
                                                                     .OrderBy(u => Math.Abs(u.Level - user.Level))
                                                                     .ThenBy(u => Math.Abs(u.PlayTime - user.PlayTime))
                                                                     .ThenBy(u => Math.Abs(u.Rank - user.Rank))
                                                                     .ToList();
                                if (potentialOpponents != null)
                                {
                                    if (potentialOpponents?.Count > 0)
                                    {
                                        User? opponent;
                                        if (potentialOpponents?.Count == 1)
                                        {
                                            opponent = potentialOpponents?.First();
                                        }
                                        else
                                        {
                                            opponent = potentialOpponents?[new Random().Next(0, potentialOpponents.Count)];
                                        }

                                        // İlk sıradaki oyuncuyu rakip olarak seç
                                        //opponent = potentialOpponents.First();

                                        if (opponent != null)
                                        {
                                            // Oyuncuları Queque tablosuna ekleyin
                                            var queue = new Queue
                                            {
                                                HomeUserId = quequeRequestDto.Id,
                                                AwayUserId = opponent.Id,
                                                StartDate = DateTime.Now
                                            };

                                            context.Queue?.Add(queue);
                                            var result = context.SaveChanges();
                                            if (result > 0)
                                            {
                                                // Sonuçları döndür
                                                return new QueueResponseDto
                                                {
                                                    Result = SuccessHandler(),
                                                    Data = new QuequeResponseData
                                                    {
                                                        Id = queue.Id, //Queuq Id
                                                        Email = opponent.Email, // Rakip oyuncunun e-postası
                                                        Level = opponent.Level, // Rakip oyuncunun seviyesi
                                                        PlayTime = opponent.PlayTime, // Rakip oyuncunun oyun süresi
                                                        Rank = opponent.Rank // Rakip oyuncunun rank seviyesi
                                                    }
                                                };
                                            }
                                            else
                                            {
                                                return new QueueResponseDto { Result = ErrorHandler("Bulunan rakip kayıt edilemedi."), Data = null };
                                            }
                                        }
                                        else
                                        {
                                            return new QueueResponseDto { Result = ErrorHandler("Rakip bulunamadı."), Data = null };
                                        }
                                    }
                                    else
                                    {
                                        return new QueueResponseDto { Result = ErrorHandler("Uygun rakipler bulunamadı."), Data = null };
                                    }
                                }
                                else
                                {
                                    return new QueueResponseDto { Result = ErrorHandler("Uygun rakipler bulunamadı."), Data = null };
                                }
                            }
                            else
                            {
                                return new QueueResponseDto { Result = ErrorHandler("PVP modu kapalı."), Data = null };
                            }
                        }
                        else
                        {
                            return new QueueResponseDto { Result = ErrorHandler("Kullanıcı aktif değil."), Data = null };
                        }
                    }
                    else
                    {
                        return new QueueResponseDto { Result = ErrorHandler("Kayıtlı kullanıcı bulunamadı."), Data = null };
                    }
                }
            }
            catch (Exception ex)
            {
                return new QueueResponseDto { Result = ExceptionHandler(ex), Data = null };
            }
        }

        public FinishQueueResponseDto FinishMatch(FinishQueueRequestDto finishQuequeRequestDto, string token)
        {
            try
            {
                if (!VerifyUserWithJwt(token))
                {
                    return new FinishQueueResponseDto { Result = ErrorHandler("Token hatası."), Data = null };
                }
                else
                {
                    using var context = new SimpleContextDb();
                    var winner = context.User?.FirstOrDefault(u => u.Id == finishQuequeRequestDto.WinnerId && u.Active == 1);
                    if (winner != null)
                    {
                        var queue = context.Queue?.FirstOrDefault(q => q.HomeUserId == finishQuequeRequestDto.WinnerId);
                        queue ??= context.Queue?.FirstOrDefault(q => q.AwayUserId == finishQuequeRequestDto.WinnerId);

                        if (queue != null)
                        {
                            queue.WinnerId = finishQuequeRequestDto.WinnerId;
                            queue.FinishDate = DateTime.Now;
                            queue.Active = 0;

                            // Kazananın Level ve Rank özelliklerine kazandığı kadar ekleyin
                            winner.Level += finishQuequeRequestDto.GainedLevel; // Örnek olarak kazandığında seviyesini artırın
                            winner.Rank += finishQuequeRequestDto.GainedRank; // Örnek olarak kazandığında sıralama puanını artırın

                            var result = context.SaveChanges();

                            if (result > 0)
                            {
                                return new FinishQueueResponseDto
                                {
                                    Result = SuccessHandler(),
                                    Data = new FinishQueueResponseData
                                    {
                                        Id = queue.Id,
                                        GainedLevel = winner.Level, // Kazandığı seviye
                                        GainedRank = winner.Rank, // Kazandığı sıralama puanı
                                        StartDate = queue.StartDate,
                                        FinishDate = queue.FinishDate.Value,
                                        Active = queue.Active
                                    }
                                };
                            }
                            else
                            {
                                return new FinishQueueResponseDto { Result = ErrorHandler("Bilgiler güncellenemedi."), Data = null };
                            }
                        }
                        else
                        {
                            return new FinishQueueResponseDto { Result = ErrorHandler("Kullanıcıya ait aktif bir maç yok."), Data = null };
                        }
                    }
                    else
                    {
                        return new FinishQueueResponseDto { Result = ErrorHandler("Kullanıcı bulunamadı."), Data = null };
                    }
                }
            }
            catch (Exception ex)
            {
                return new FinishQueueResponseDto { Result = ExceptionHandler(ex), Data = null };
            }
        }
    }
}
