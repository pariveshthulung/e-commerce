using ebay.Models;

namespace ebay.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}
