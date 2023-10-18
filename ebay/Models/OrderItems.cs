using System.ComponentModel.DataAnnotations.Schema;

namespace ebay.Models
{

    public class OrderItems
    {
        public int id { get; set; }
        public int Order_id { get; set; }
        public virtual Order? Order { get; set; }
        public int Product_id { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,4)")] // <--

        public decimal Price { get; set; }

    }
}