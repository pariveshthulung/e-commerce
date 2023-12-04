using ebay.Models;

namespace ebay.Repository.IRepository;

public interface ICategoryRepository :IRepository<ProductCategory> 
{
    Task AddAsync (List<int>? Id , int ProductId);
    Task UpdateAsync (List<int>? Id , int ProductId);
}
