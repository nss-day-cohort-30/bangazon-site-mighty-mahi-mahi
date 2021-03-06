namespace Bangazon.Models.OrderViewModels {
    public class OrderLineItem {
        public Product Product { get; set; }
        public int Units { get; set; }
        public double Total { get; set; }
        public UserProductRating UserProductRating { get; set; }
    }
}