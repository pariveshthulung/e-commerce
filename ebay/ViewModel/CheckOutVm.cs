using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ebay.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ebay.ViewModel;

public class CheckOutVm
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Street_no { get; set; }
    public string? Address_Line { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? Landmark { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Postal_Code { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public Decimal OrderTotal { get; set; }
    public List<Address>? Address;
    public List<Country>? Countries;
    public List<CartItem>? CartItemList;
    public Cart? Cart;
    public Order? Order { get; set; }
    public List<Order>? OderList { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? SessionId { get; set; }

    public int? User_id { get; set; }
    public long PhoneNo { get; set; }

    public Product? Product;

    public int Cart_id { get; set; }
    public int Product_id { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
    public int CountryId { get; set; }
    public int ProductIdBuyNow { get; set; }
    public int QuantityBuyNow { get; set; }

    public SelectList CountryList()
    {
        return new SelectList(
            Countries,
            nameof(Country.id),
            nameof(Country.Name),
            CountryId
        );
    }


}
