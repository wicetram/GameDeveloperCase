using System.Linq.Expressions;

namespace GameDeveloperCore.DataAccess
{
    public interface IEntityRepository<T>
    {
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);

        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
    }
}
