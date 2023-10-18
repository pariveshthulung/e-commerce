using System.ComponentModel.DataAnnotations.Schema;
using ebay.Constants;
using ebay.Entity;
using Microsoft.Identity.Client;

namespace ebay.Models
{

    public class Order
    {
        public int id { get; set; }
        public int User_id { get; set; }
        [ForeignKey("User_id")]
        public virtual User? User { get; set; }
        [Column(TypeName = "decimal(18,4)")] // <--

        public decimal Order_total { get; set; }
        public DateTime Order_date = DateTime.Now;

        public string? Order_status { get; set; }
    }
}