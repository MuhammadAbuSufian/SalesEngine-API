using Project.Model;

namespace Project.ViewModel
{
    public class PurchaseDetailViewModel: BaseViewModel<PurchaseDetail>
    {
        public PurchaseDetailViewModel(PurchaseDetail model) : base(model)
        {
            Amount = model.Amount;
            Quantity = model.Quantity;
            PurchaseId = model.PurchaseId;
            ProductId = model.ProductId;

            if (model.Product != null)
            {
                Product = new ProductViewModel(model.Product);
            }

        }

        public decimal Amount { get; set; }

        public int Quantity { get; set; }

        public string PurchaseId { get; set; }

        public string ProductId { get; set; }

        public ProductViewModel Product { get; set; }
    }
}