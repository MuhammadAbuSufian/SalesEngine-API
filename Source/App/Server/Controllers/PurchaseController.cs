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
    public class PurchaseController : ApiController
    {
        private readonly IPurchaseService _service;
        private readonly IPurchaseDetailsService _purchaseDetailsService;
        private readonly IProductService _productService;
        private readonly ICompanyService _companyService;
        public PurchaseController(
            IPurchaseService service,
            IPurchaseDetailsService purchaseDetailsService,
            IProductService productService,
            ICompanyService companyService
            )
        {
            _service = service;
            _purchaseDetailsService = purchaseDetailsService;
            _productService = productService;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("api/purhcase/save")]
        public IHttpActionResult SavePurchase(Purchase purchase)
        {

            return Ok(_service.Add(purchase));
        }

        [HttpDelete]
        [Route("api/purhcase/movetrash")]
        public IHttpActionResult DeleteBrand(string id)
        {

            return Ok(_service.Trash(id));
        }


        [HttpPost]
        [Route("api/purchase/save")]
        public IHttpActionResult SaveNewPurchase(NewPurchaseRequestModel request)
        {
            Purchase purchase = new Purchase();
            purchase.InvoiceNo = _service.GetRecordId();
            purchase.Amount = request.Total;
           
            purchase.Comment = "N/A";

            var savedPurchase = _service.AddwithReturnId(purchase);

            foreach (var item in request.PurchaseItem)
            {
                PurchaseDetail purchasDetail = new PurchaseDetail();
                purchasDetail.Amount = item.Subtotal;
                purchasDetail.Quantity = item.Qty;
                purchasDetail.ProductId = item.Id;
                purchasDetail.PurchaseId = savedPurchase.Id;

                _purchaseDetailsService.Add(purchasDetail);

                _productService.IncreaseStock(purchasDetail.ProductId, purchasDetail.Quantity);

            }
            _companyService.DeductBalence(savedPurchase.Amount);
            return Ok(purchase.InvoiceNo);
        }

        [HttpPost]
        [Route("api/purchase/get")]
        public IHttpActionResult GetProduct(GridRequestModel request)
        {
            return Ok(_service.GetGridData(request));
        }

        [HttpPost]
        [Route("api/purchase/report")]
        public IHttpActionResult GetRepost(GridRequestModel request, DateTime startDate, DateTime endDate)
        {
            return Ok(_service.GetPurchaseReportGridData(request, startDate, endDate));
        }
    }
}
