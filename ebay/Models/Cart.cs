using System.ComponentModel.DataAnnotations.Schema;
using ebay.Entity;

namespace ebay.Models;

public class Cart
{
    public int id { get; set; }
    // public Guid Id { get; set; }
    public int? User_id { get; set; }
    [ForeignKey("User_id")]
    public virtual User? User { get; set; }
    public string? CartStatus { get; set; }
}
