using System.ComponentModel.DataAnnotations.Schema;
using ebay.Entity;
using ebay.Models;

namespace ebay.ViewModel;

public class ReviewVm
{
    public int Id { get; set; }
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
    public DateTime ReviewDate { get; set; }
    public List<Review>? ReviewExist { get; set; }
    public Review? Review { get; set; }
    public int? UserID { get; set; }
    public List<int>? OrderIdList { get; set; }


    public int? User_id { get; set; }
    [ForeignKey("User_id")]
    public virtual User? User { get; set; }
    public int Product_id { get; set; }
    [ForeignKey("Product_id")]
    public virtual Product? Product { get; set; }
    public List<Order>? OrderFrmDb { get; set; }
    public List<OrderItems>? OrderItemFrmDb { get; set; }

}
