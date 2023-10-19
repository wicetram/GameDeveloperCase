using GameDeveloperCore.DataAccess.EntityFramework;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete.EntityFramework.Context;
using GameDeveloperEntity.Concrete;

namespace GameDeveloperDataAccess.Concrete
{
    public class EfQueueDal : EfEntityRepositoryBase<Queue, SimpleContextDb>, IQueueDal
    {
    }
}
