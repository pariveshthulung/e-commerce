
using ebay.Models;

namespace ebay.ViewModel;

public class ImageVm
{
    public List<ProductImages>? Images { get; set; }
    public int ProductId { get; set; }
    public List<IFormFile>? Files { get; set; }
}