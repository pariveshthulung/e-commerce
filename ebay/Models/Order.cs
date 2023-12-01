using System.ComponentModel.DataAnnotations.Schema;
using ebay.Constants;
using ebay.Entity;
using Microsoft.Identity.Client;

namespace ebay.Models
{

    public class Order
    {
        public int id { get; set; }
        public int? User_id { get; set; }
        [ForeignKey("User_id")]
        public virtual User? User { get; set; }
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Order_total { get; set; }
        public DateTime Order_date { get; set; } = DateTime.Now;

        public string? PaymentIntentId { get; set; }
        public string? SessionId { get; set; }
        public DateTime PaymentDate { get; set; }
        
    }
}