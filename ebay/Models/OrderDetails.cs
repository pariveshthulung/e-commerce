namespace ebay.Models{

    public class OrderDetails
    {
        public int id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }


    }
}