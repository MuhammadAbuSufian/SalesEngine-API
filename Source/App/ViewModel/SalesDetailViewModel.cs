using Project.Model;

namespace Project.ViewModel
{
    public class SalesDetailViewModel: BaseViewModel<SalesDetail>
    {
        public SalesDetailViewModel(SalesDetail model) : base(model)
        {
            Amount = model.Amount;
            Discount = model.Discount;
            DiscountPercent = model.DiscountPercent;
            Quantity = model.Quantity;
            SalesId = model.SalesId;
            ProductId = model.ProductId;
//
//            if (model.Sale != null)
//            {
//                Sale = new SaleViewModel(model.Sale);
//            }

            if (model.Product != null)
            {
                Product = new ProductViewModel(model.Product);
            }
        }

        public decimal Amount { get; set; }

        public decimal Discount { get; set; }

        public decimal DiscountPercent { get; set; }

        public int Quantity { get; set; }

        public string SalesId { get; set; }

        public SaleViewModel Sale { get; set; }

        public string ProductId { get; set; }

        public ProductViewModel Product { get; set; }
    }
}