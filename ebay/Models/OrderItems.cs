namespace ebay.Models{

    public class OrderItems
    {
        public int id { get; set; }
        public int OrderDetailsId { get; set; }
        public virtual OrderDetails OrderDetails { get; set; }
        public int ProductId { get; set; }
        public virtual List<Product> Product { get; set; } = new List<Product>();

    }
}