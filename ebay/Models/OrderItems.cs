namespace ebay.Models{

    public class OrderItems
    {
        public int id { get; set; }
        public int OrderDetailsId { get; set; }
        public virtual Order? Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

    }
}