using System.Collections.Generic;
using Project.Model;

namespace Project.RequestModel
{
    public class NewSaleRequestModel
    {
        public List<SalesItemRequestModel> SalesItem { get; set; }
        public decimal Total { get; set; }
        public decimal Due { get; set; }
        public decimal Profit { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Vat { get; set; }
        public string CustomerId { get; set; }
    }
}