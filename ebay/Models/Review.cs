using System.ComponentModel.DataAnnotations.Schema;
using ebay.Entity;

namespace ebay.Models;

public class Review
{
    public int id { get; set; }
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
    public DateTime ReviewDate { get; set; }

    public int User_id { get; set; }
    [ForeignKey("User_id")]
    public virtual User? User { get; set; }
    public int Product_id { get; set; }
    [ForeignKey("Product_id")]
    public virtual Product? Product { get; set; }
}
