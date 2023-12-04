using ebay.Models;

namespace ebay.Repository.IRepository;

public interface ICategoryRepository :IRepository<ProductCategory> 
{
    Task AddAsync (int Id , int ProductId);
}
