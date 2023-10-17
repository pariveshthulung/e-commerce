namespace ebay.Models;

public class CartItem
{
    public int id { get; set; }
    public int Cart_id { get; set; }
    public virtual Cart? Cart { get; set; }
    public int Product_id { get; set; }
    public virtual Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
