using System.ComponentModel.DataAnnotations.Schema;
using ebay.Entity;

namespace ebay.Models;

public class Address
{
    public int id { get; set; }
    public string? Address_Line { get; set; }
    public string? Landmark { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? Postal_Code { get; set; }

    public int Country_id { get; set; }
    [ForeignKey("Country_id")]
    public virtual Country? Country { get; set; }
    public int? User_id { get; set; }
    [ForeignKey("User_id")]
    public virtual User? User { get; set; }


    public bool Is_Default { get; set; }
}
