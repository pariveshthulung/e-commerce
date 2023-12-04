using ebay.Data;
using ebay.Repository.IRepository;

namespace ebay.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IProductRepository ProductRepo {get;private set;}
    public IWebHostEnvironment _webHostEnvironment { get; }

    public ICategoryRepository CategoryRepo {get;private set;}

    public UnitOfWork(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment  )
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        ProductRepo = new ProductRepository(_context,_webHostEnvironment);
        CategoryRepo = new CategoryRepository(_context); 
    }
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
    }
