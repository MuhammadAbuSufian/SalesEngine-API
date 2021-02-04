using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Product:  EntityBase
    {
        public string Name { get; set; }

        public string ApplicationFor { get; set; }

        public string ApplicationTo { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal DiscountPrice { get; set; }
        public string Power { get; set; }

        public string BarCodeNo { get; set; }

        public string VendorBarcodeNo { get; set; }

        public long Stock { get; set; }

        public string GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public string BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public virtual ICollection<SalesDetail> SalesDetails { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}