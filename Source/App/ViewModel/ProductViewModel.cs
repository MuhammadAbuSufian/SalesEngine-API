using System.Collections.Generic;
using Project.Model;

namespace Project.ViewModel
{
    public class ProductViewModel: BaseViewModel<Product>
    {
        public ProductViewModel(Product model) : base(model)
        {
            Name = model.Name;
            ApplicationFor = model.ApplicationFor;
            ApplicationTo = model.ApplicationTo;
            CostPrice = model.CostPrice;
            SellingPrice = model.SellingPrice;
            DiscountPrice = model.DiscountPrice;
            Power = model.Power;
            Stock = model.Stock;
            GroupId = model.GroupId;
            BrandId = model.BrandId;
            BarCodeNo = model.BarCodeNo;
            VendorBarcodeNo = model.VendorBarcodeNo;

            if (model.Group != null)
            {
                Group = new GroupViewModel(model.Group);
            }

            if (model.Brand != null)
            {
                Brand = new BrandViewModel(model.Brand);
            }
        }

        public string Name { get; set; }

        public string ApplicationFor { get; set; }

        public string ApplicationTo { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal DiscountPrice { get; set; }

        public string Power { get; set; }

        public long Stock { get; set; }

        public string GroupId { get; set; }

        public GroupViewModel Group { get; set; }

        public string BrandId { get; set; }

        public BrandViewModel Brand { get; set; }

        public string BarCodeNo { get; set; }

        public string VendorBarcodeNo { get; set; }

        public virtual ICollection<SalesDetailViewModel> SalesDetails { get; set; }

        public virtual ICollection<PurchaseDetailViewModel> PurchaseDetails { get; set; }
    }
}