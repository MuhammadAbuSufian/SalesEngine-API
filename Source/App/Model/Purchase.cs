using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Purchase: EntityBase
    {
        public string InvoiceNo { get; set; }

        public string Comment { get; set; }

        public decimal Amount { get; set; }
        public string DiscountId { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        [ForeignKey("DiscountId")]
        public virtual Discount PurchaseDiscount { get; set; }
    }
}