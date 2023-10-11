using ebay.Models;

namespace ebay.Repository.IRepository;

public interface IProduction : IRepository<Product>
{
    void Update(Product obj);
}
