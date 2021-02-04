using Project.Model;

namespace Project.RequestModel
{
    public class SalesItemRequestModel: Product
    {
        public int Qty { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal Subtotal { get; set; }
   
    }
}