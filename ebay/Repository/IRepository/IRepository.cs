using System.Linq.Expressions;

namespace ebay.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(Expression<Func<T,bool>> filter);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
}
