using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Sale: EntityBase
    {
        public string InvoiceNo { get; set; }

        public string Commint { get; set; }

        public decimal Amount { get; set; }

        public decimal Discount { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal Due { get; set; }
        public decimal Profit { get; set; }

        public string CustomerId { get; set; }
        public string ProfitId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<SalesDetail> SalesDetails { get; set; }

        [ForeignKey("ProfitId")]
        public virtual Profit SalesProfit { get; set; }
    }
}