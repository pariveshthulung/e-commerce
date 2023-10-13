namespace ebay.Repository.IRepository;

public interface IUnitOfWork
{
    IProductRepository ProductRepo {get;}
    // ICategoryRepository CategoryRepo {get;}
    Task Save();
}
