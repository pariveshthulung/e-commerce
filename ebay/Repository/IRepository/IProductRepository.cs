using ebay.Models;
using ebay.ViewModel;

namespace ebay.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(ProductUpdateVm vm);
    void AddAsync (ProductAddVm vm)
}
