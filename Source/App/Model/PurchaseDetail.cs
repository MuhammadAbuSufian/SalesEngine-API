using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class PurchaseDetail: EntityBase
    {
        public decimal Amount { get; set; }

        public int Quantity { get; set; }

        public string PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public Purchase Purchase { get; set; }

        public string ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}