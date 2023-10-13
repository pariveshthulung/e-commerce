using ebay.Data;
using ebay.Repository.IRepository;

namespace ebay.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IProductRepository ProductRepo {get;private set;}
    // public ICategoryRepository CategoryRepo {get;private set;}

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        ProductRepo = new ProductRepository(_context);
        // CategoryRepo = new CategoryRepository(_context); 
    }
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
    }
