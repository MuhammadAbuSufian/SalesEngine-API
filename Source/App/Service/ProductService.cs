using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{
    public interface IProductService : IBaseService<Product, ProductViewModel>
    {
        GridResponseModel<ProductViewModel> GetGridData(GridRequestModel request);

        List<ProductViewModel> ProductSearch(string key, string groupId, string brandId);

        void IncreaseStock(string productId, int qty);

        void DecreaseStock(string productId, int qty);

        bool UpdateProduct(Product product);

        bool UpdateStock(string id, int stock);

        bool RestoreProduct(Product product);

        int Count();
    }

    public class ProductService : BaseService<Product, ProductViewModel>, IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) : base(repository)
        {
            _repository = repository;

        }

        public GridResponseModel<ProductViewModel> GetGridData(GridRequestModel request)
        {
            var firstCharOfKey = " ";

            if (request.Keyword != null && request.Keyword != "")
            {
                firstCharOfKey = request.Keyword[0].ToString();
            }
            GridResponseModel<ProductViewModel> gridData = new GridResponseModel<ProductViewModel>();
            List<Product> products = new List<Product>();
            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId()).Include(x=>x.Group).Include(x=>x.Brand);
            if (firstCharOfKey.All(char.IsDigit))
            {
                if (request.Keyword.Length > 9)
                {
                    products = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.VendorBarcodeNo.Equals(request.Keyword)).Include(x => x.Group).Include(x => x.Brand).OrderBy(l => l.Name).Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount).ToList();
                }
                else
                {
                    products = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.BarCodeNo.Equals(request.Keyword)).Include(x => x.Group).Include(x => x.Brand).OrderBy(l => l.Name).Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount).ToList();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.Name.Contains(request.Keyword));
                }

                if (request.IsAscending)
                {
                    switch (request.OrderBy)
                    {
                        case "Name": query = query.OrderBy(l => l.Name); break;
                        default: query = query.OrderBy(l => l.Created); break;
                    }
                }
                else
                {
                    switch (request.OrderBy)
                    {
                        case "Name": query = query.OrderByDescending(l => l.Name); break;
                        default: query = query.OrderByDescending(l => l.Created); break;
                    }
                }
                query = query.Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

                products = query.ToList();
            }
            

            foreach (var product in products)
            {
                gridData.Data.Add(new ProductViewModel(product));
            }

            return gridData;
        }

        public List<ProductViewModel> ProductSearch(string key, string groupId, string brandId)
        {   

            var firstCharOfKey = " ";

            if (key != null)
            {
                firstCharOfKey = key[0].ToString();
            }

            List<Product> products;
            if (firstCharOfKey.All(char.IsDigit))
            {
                if (key.Length > 9)
                {
                    products = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.VendorBarcodeNo.Equals(key)).Include(x => x.Group).Include(x => x.Brand).ToList();
                }
                else
                {
                    products = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.BarCodeNo.Equals(key)).Include(x => x.Group).Include(x => x.Brand).ToList();
                }
            }
            else
            {
                var query = _repository.GetAllActive(getCreatedCompanyId()).Where(x => x.Name.Contains(key)).Include(x => x.Group).Include(x => x.Brand);

                if (!string.IsNullOrEmpty(brandId))
                {
                    query = query.Where(x => x.Brand.Id == brandId);
                }

                if (!string.IsNullOrEmpty(groupId))
                {
                    query = query.Where(x => x.Group.Id == groupId);
                }
                products = query.OrderBy(x => x.Name).Take(10).ToList();
            }
            
            //query = query.Where(x => x.VendorBarcodeNo == key || x.BarCodeNo == key);

            List<ProductViewModel> returnable = new List<ProductViewModel>();

            foreach (var product in products)
            {
                returnable.Add(new ProductViewModel(product));
            }

            return returnable;
        }

        public void IncreaseStock(string productId, int qty)
        {
            var product = _repository.GetById(productId);
            product.Stock = product.Stock + qty;
            _repository.Commit();
        }

        public void DecreaseStock(string productId, int qty)
        {
            var product = _repository.GetById(productId);
            product.Stock = product.Stock - qty;
            _repository.Commit();
        }

        public bool UpdateProduct(Product product)
        {
            var updateProduct = _repository.GetById(product.Id);
            updateProduct.Name = product.Name;
            updateProduct.CostPrice = product.CostPrice;
            updateProduct.SellingPrice = product.SellingPrice;
            updateProduct.Power = product.Power;
            updateProduct.BrandId = product.BrandId;
            updateProduct.GroupId = product.GroupId;
            updateProduct.DiscountPrice = product.DiscountPrice;
            updateProduct.VendorBarcodeNo = product.VendorBarcodeNo;
            var user = this.GetUserFromToken();

            updateProduct.Modified = DateTime.Now;
            updateProduct.ModifiedBy = user.Id;

            return _repository.Commit();
        }
        public bool UpdateStock(string id, int addStock)
        {
            var updateProduct = _repository.GetById(id);
            
            updateProduct.Stock = updateProduct.Stock + addStock;

            var user = this.GetUserFromToken();

            updateProduct.Modified = DateTime.Now;
            updateProduct.ModifiedBy = user.Id;

            return _repository.Commit();
        }

        public bool RestoreProduct(Product product)
        {
            var existProduct = _repository.GetAllActive().FirstOrDefault(x => x.Name == product.Name);
            if (existProduct == null)
            {
                return this.Add(product);
            }

            return true;
        }
        public int Count()
        {
            return _repository.GetAll().Count();
        }
    }
}