using System.Linq.Expressions;
using ebay.Data;
using ebay.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ebay.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private  readonly ApplicationDbContext _context;
    internal DbSet<T> dbset;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        this.dbset = _context.Set<T>(); //dbset == _context.products
    }

    public async Task<T> Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbset;
        query = query.Where(filter);
        return await query.FirstAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        IQueryable<T> query = dbset;
        return await query.ToListAsync();
    }

    public void Remove(T entity)
    {
        dbset.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbset.RemoveRange(entity);
    }

   
}
