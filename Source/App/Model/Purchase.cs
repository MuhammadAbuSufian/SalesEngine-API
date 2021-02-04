using System.Collections.Generic;

namespace Project.Model
{
    public class Purchase: EntityBase
    {
        public string InvoiceNo { get; set; }

        public string Comment { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

    }
}