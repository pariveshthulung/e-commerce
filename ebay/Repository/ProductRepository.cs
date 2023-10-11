using System.Linq.Expressions;
using ebay.Data;
using ebay.Models;
using ebay.Repository.IRepository;

namespace ebay.Repository;

public class ProductRepository : Repository<Product> , IProduct
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Add(Product entity)
    {
        throw new NotImplementedException();
    }

    public Product Get(Expression<Func<Product, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Remove(Product entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<Product> entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Product obj)
    {
        throw new NotImplementedException();
    }
}
