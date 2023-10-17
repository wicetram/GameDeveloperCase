using GameDeveloperCore.DataAccess;
using GameDeveloperEntity.Concrete;

namespace GameDeveloperDataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
