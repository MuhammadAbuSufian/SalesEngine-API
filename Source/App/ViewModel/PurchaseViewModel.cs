using System.Collections.Generic;
using Project.Model;

namespace Project.ViewModel
{
    public class PurchaseViewModel: BaseViewModel<Purchase>
    {
        public PurchaseViewModel(Purchase model) : base(model)
        {
            InvoiceNo = model.InvoiceNo;
            Comment = model.Comment;
            Amount = model.Amount;

            PurchaseDetails = new List<PurchaseDetailViewModel>();

            if (model.PurchaseDetails != null)
            {
                foreach (var purchaseDetail in model.PurchaseDetails)
                {
                    PurchaseDetails.Add(new PurchaseDetailViewModel(purchaseDetail));
                }
            }
        }

        public string InvoiceNo { get; set; }

        public string Comment { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<PurchaseDetailViewModel> PurchaseDetails { get; set; }


    }


}