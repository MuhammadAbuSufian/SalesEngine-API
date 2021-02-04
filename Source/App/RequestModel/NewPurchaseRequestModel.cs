using System.Collections.Generic;

namespace Project.RequestModel
{
    public class NewPurchaseRequestModel
    {
        public List<SalesItemRequestModel> PurchaseItem { get; set; }
        public decimal Total { get; set; }
        public decimal Vat { get; set; }
    }
}