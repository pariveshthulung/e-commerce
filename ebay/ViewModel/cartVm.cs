using System.ComponentModel.DataAnnotations.Schema;
using ebay.Entity;
using ebay.Models;

namespace ebay.ViewModel;

public class cartVm
{
    public int? User_id { get; set; }
    public Product? Product;

    public int Cart_id { get; set; }
    public int Product_id { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")] // <--
    public List<CartItem>? CartItemList ;

    public decimal Subtotal { get; set; }
}
