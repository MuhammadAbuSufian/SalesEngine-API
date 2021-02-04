using System.Collections.Generic;
using Project.Model;

namespace Project.ViewModel
{
    public class SaleViewModel: BaseViewModel<Sale>
    {
        public SaleViewModel(Sale model) : base(model)
        {
            InvoiceNo = model.InvoiceNo;
            Commint = model.Commint;
            Amount = model.Amount;
            Due = model.Due;
            Discount = model.Discount;
            DiscountPercent = model.DiscountPercent;

            SalesDetails = new List<SalesDetailViewModel>();

            if (model.SalesDetails != null)
            {
                foreach (var salesDetail in model.SalesDetails)
                {
                    SalesDetails.Add(new SalesDetailViewModel(salesDetail));
                }
            }

            if(model.Customer != null)
            {
                Customer = new CustomerViewModel(model.Customer);
            }
        }

        public string InvoiceNo { get; set; }

        public string Commint { get; set; }

        public decimal Amount { get; set; }
        public decimal Due { get; set; }
        public decimal Discount { get; set; }

        public decimal DiscountPercent { get; set; }

        public virtual ICollection<SalesDetailViewModel> SalesDetails { get; set; }
        public virtual CustomerViewModel Customer { get; set; }
    }
}