using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class SalesDetail: EntityBase
    {
        public decimal Amount { get; set; }

        public decimal Discount { get; set; }

        public decimal DiscountPercent { get; set; }

        public int Quantity { get; set; }

        public string SalesId { get; set; }

        [ForeignKey("SalesId")]
        public Sale Sale { get; set; }

        public string ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}