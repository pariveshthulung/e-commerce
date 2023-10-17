using ebay.Entity;

namespace ebay.Models;

public class Cart
{
    public int id { get; set; }
    public int User_id { get; set; }
    public virtual User? User { get; set; }
    public string? CartStatus { get; set; }
}
