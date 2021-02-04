using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Model;
using Project.RequestModel;
using Project.Service;

namespace Project.Server.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _service;

        private readonly IBrandService _brandService;

        private readonly IGroupService _groupService;

        public ProductController(IProductService service, IBrandService brandService, IGroupService groupService)
        {
            _service = service;
            _brandService = brandService;
            _groupService = groupService;
        }

        [HttpPost]
        [Route("api/product/save")]
        public IHttpActionResult SaveProduct(Product product)
        {   
            if(product.GroupId == null || product.BrandId == null)
            {
                return Ok(false);
            }

            if (string.IsNullOrEmpty(product.Id))
            {
                Random rnd = new Random();
                product.BarCodeNo = rnd.Next(100) + "" + _service.Count() +""+ rnd.Next(100);
                return Ok(_service.Add(product));
            }
            else
            {
                return Ok(_service.UpdateProduct(product));
            }
        }


        [HttpGet]
        [Route("api/product/stockupdate")]
        public IHttpActionResult UpdateProduct(string id, int addStock)
        {
             return Ok(_service.UpdateStock(id, addStock));
        }

        [HttpPost]
        [Route("api/Product/restore")]
        public IHttpActionResult RestoreProduct(ProductRestoreModel request)
        {
            var brandId = _brandService.RestoreBrand(request.CompanyName);
            var groupId = _groupService.RestoreGroup(request.GroupName);

            var product = new Product();
            product.BrandId = brandId;
            product.GroupId = groupId;
            product.Name = request.ProductName;
            product.CostPrice = 0;
            product.SellingPrice = request.Price;
            product.Power = request.Power;
            return Ok(_service.RestoreProduct(product));
        }


        [HttpPost]
        [Route("api/product/get")]
        public IHttpActionResult GetProduct(GridRequestModel request)
        {
            return Ok(_service.GetGridData(request));
        }


        [HttpGet]
        [Route("api/product/search")]
        public IHttpActionResult GetProduct(string key, string groupId, string brandId)
        {
            return Ok(_service.ProductSearch(key, groupId, brandId));
        }
    }
}
