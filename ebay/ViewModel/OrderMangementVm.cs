using ebay.Models;

namespace ebay.ViewModel;

public class OrderMangementVm
{
      public int? UserId { get; set; }
      public List<Order>? OrderList { get; set; }
      public List<OrderItems>? OrderItemsList { get; set; }
}
