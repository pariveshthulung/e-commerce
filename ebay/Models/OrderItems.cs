using System.ComponentModel.DataAnnotations.Schema;
using ebay.Constants;

namespace ebay.Models
{

    public class OrderItems
    {
        public int id { get; set; }
        public int Order_id { get; set; }
        [ForeignKey("Order_id")]
        public virtual Order? Order { get; set; }
        public int Product_id { get; set; }
        [ForeignKey("Product_id")]
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }
        public string? PaymentStatus { get; set; }

        public string? Order_status { get; set; } 


    }
}