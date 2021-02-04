using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.Provider;
using Project.Model;
using Project.RequestModel;
using Project.Service;

namespace Project.Server.Controllers
{
    public class SaleController : ApiController
    {
        private readonly ISaleService _service;
        private readonly ISaleDetailsService _salesDetailsService;
        private readonly IProductService _productService;
        private readonly IPurchaseService _purchaseService;
        private readonly ICompanyService _companyService;

        public SaleController(
            ISaleService service,
            ISaleDetailsService salesDetailsService,
            IProductService productService,
            IPurchaseService purchaseService,
            ICompanyService companyService
            )
        {
            _service = service;
            _salesDetailsService = salesDetailsService;
            _productService = productService;
            _purchaseService = purchaseService;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("api/sale/save")]
        public IHttpActionResult SaveSale(NewSaleRequestModel request)
        {
            Sale sale = new Sale();
            sale.InvoiceNo = _service.GetRecordId();
            sale.Amount = request.Total;
            sale.Discount = request.TotalDiscount;
            sale.Due = request.Due;
            sale.Profit = request.Profit;
            sale.DiscountPercent = 0;
            sale.CustomerId = request.CustomerId;

            if (request.TotalDiscount > 0)
            {
                sale.DiscountPercent = (request.TotalDiscount / request.Total) * 100;
            }
            sale.Commint = "N/A";

            var savedSale = _service.AddwithReturnId(sale);

            foreach (var item in request.SalesItem)
            {
                SalesDetail salesDetail = new SalesDetail();
                salesDetail.Amount = item.Subtotal-(item.Discount*item.Qty);
                salesDetail.Discount = item.Discount;
                salesDetail.DiscountPercent = item.DiscountPer;
                salesDetail.Quantity = item.Qty;
                salesDetail.ProductId = item.Id;
                salesDetail.SalesId = savedSale.Id;

                _salesDetailsService.Add(salesDetail);

                _productService.DecreaseStock(salesDetail.ProductId, salesDetail.Quantity);

            }

            _companyService.AddBalence(savedSale.Amount);

            return Ok(savedSale.InvoiceNo );
        }



        [HttpPost]
        [Route("api/sale/get")]
        public IHttpActionResult GetProduct(GridRequestModel request, DateTime startDate, DateTime endDate)
        {
            return Ok(_service.GetGridData(request, startDate, endDate));
        }

        [HttpPost]
        [Route("api/sale/report")]
        public IHttpActionResult GetRepost(GridRequestModel request, DateTime startDate, DateTime endDate)
        {
            return Ok(_service.GetSalesReportGridData(request, startDate, endDate));
        }

        [HttpGet]
        [Route("api/sales/stat")]
        public IHttpActionResult GetStat()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var saleCount = _service.GetAllActive()
                .Count(x => x.Created >= today && x.Created <= tomorrow);

            var saleSum = _service.GetAllActive()
                .Where(x => x.Created >= today && x.Created <= tomorrow).Sum(x=>x.Amount);

            var purchaseCount = _purchaseService.GetAllActive()
                .Count(x => x.Created >= today && x.Created <= tomorrow);

            var purchaseSum = _purchaseService.GetAllActive()
                .Where(x => x.Created >= today && x.Created <= tomorrow).Sum(x=>x.Amount);

            return Ok(new
            {
                saleCount = saleCount,
                saleSum = saleSum,
                purchaseCount = purchaseCount,
                purchaseSum = purchaseSum
            });
        }

        [HttpPost]
        [Route("api/sale/area-chart-data")]
        public IHttpActionResult SaleAreaChart(List<DateTime> dates)
        {
            List<decimal> values = new List<decimal>();

            foreach (var date in dates)
            {
                var tomorrow = date.AddDays(1);
                var saleSum = _service.GetAllActive()
                    .Where(x => x.Created >= date && x.Created < tomorrow).Sum(x => x.Amount);
                values.Add(saleSum);
            }
            return Ok(values);
        }

        [HttpGet]
        [Route("api/sale/dueUpadate")]
        public IHttpActionResult DueUpadate(string invoiceNo, decimal due)
        {
            return Ok(_service.DueUpdate(invoiceNo, due));
        }

        [HttpDelete]
        [Route("api/sale/return")]
        public IHttpActionResult ReturnSale(string invoiceNo)
        {
            var returnedSale = _service.ReturnSale(invoiceNo);
            return Ok(_companyService.DeductBalence(returnedSale.Amount));
        }


    }

}
