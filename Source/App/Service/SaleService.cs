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
    public interface ISaleService : IBaseService<Sale, SaleViewModel>
    {
        string GetRecordId();

        GridResponseModel<SaleViewModel> GetGridData(GridRequestModel request, DateTime startDate, DateTime endDate);
        SalesReportViewModel<SaleViewModel> GetSalesReportGridData(GridRequestModel request, DateTime startDate, DateTime endDate);
        bool DueUpdate(string invoiceNo, decimal due);
        SaleViewModel ReturnSale(string invoiceNo);

    }

    public class SaleService : BaseService<Sale, SaleViewModel>, ISaleService
    {
        private readonly ISaleRepository _repository;

        private readonly ISaleDetailsRepository _repositoryDetails;

        private readonly IProductRepository _productRepository;
        public SaleService(ISaleRepository repository, IProductRepository productRepository, ISaleDetailsRepository repositoryDetails) : base(repository)
        {
            _repository = repository;
            _repositoryDetails = repositoryDetails;
            _productRepository = productRepository;

        }

   
        public string GetRecordId()
        {
            int count = this.GetAllActive().Count();
            string recordId = "INV-";

            if (count > 0)
            {
                string countString = (count + 1).ToString();
                int len = countString.Length;
                for (int i = 0; i < (11 - len); i++)
                {
                    recordId = recordId + "0";
                }

                recordId = recordId + countString;
            }
            else
            {
                recordId = "INV-00000000001";
            }
            return recordId;
        }

        public GridResponseModel<SaleViewModel> GetGridData(GridRequestModel request, DateTime startDate, DateTime endDate)
        {
            GridResponseModel<SaleViewModel> gridData = new GridResponseModel<SaleViewModel>();

            endDate = endDate.AddDays(1);

            gridData.Count = _repository.GetAllActive(getCreatedCompanyId())
                .Where(x=>x.Created >= startDate && x.Created <=endDate ).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId())
                .Where(x => x.Created >= startDate && x.Created <= endDate)
                .Include(x => x.SalesDetails)
                .Include(x=>x.SalesDetails.Select(y=>y.Product))
                .Include(x=>x.SalesDetails.Select(y=>y.Product.Group))
                .Include(x=>x.SalesDetails.Select(y=>y.Product.Brand))
                .Include(x=>x.Customer);

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.InvoiceNo.Contains(request.Keyword));
            }

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderBy(l => l.InvoiceNo); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderByDescending(l => l.InvoiceNo); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }


            query = query.Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Sale> sales = query.ToList();

            foreach (var sale in sales)
            {
                gridData.Data.Add(new SaleViewModel(sale));
            }

            return gridData;
        }

        public SalesReportViewModel<SaleViewModel> GetSalesReportGridData(GridRequestModel request, DateTime startDate, DateTime endDate)
        {
            SalesReportViewModel<SaleViewModel> gridData = new SalesReportViewModel<SaleViewModel>();

            endDate = endDate.AddDays(1);

            var query = _repository.GetAllActive(getCreatedCompanyId())
                .Where(x => x.Created >= startDate && x.Created <= endDate)
                .Include(x => x.SalesDetails)
                .Include(x => x.SalesDetails.Select(y => y.Product))
                .Include(x => x.SalesDetails.Select(y => y.Product.Group))
                .Include(x => x.SalesDetails.Select(y => y.Product.Brand))
                .Include(x => x.Customer);

            var invoiceNo = request.Keyword.Contains("INV-");

            if (request.Keyword == "")
            {
                gridData.Count = _repository.GetAllActive(getCreatedCompanyId())
                                .Where(x => x.Created >= startDate && x.Created <= endDate).Count();

                gridData.DueCount = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.Created >= startDate && x.Created <= endDate && x.Due > 0).Count();

                gridData.TotalAmount = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.Created >= startDate && x.Created <= endDate).Sum(x => (decimal?)x.Amount) ?? 0;

                gridData.TotalDue = _repository.GetAllActive(getCreatedCompanyId())
                   .Where(x => x.Created >= startDate && x.Created <= endDate && x.Due > 0).Sum(x => (decimal?)x.Due) ?? 0;

                gridData.Profit = _repository.GetAllActive(getCreatedCompanyId())
                  .Where(x => x.Created >= startDate && x.Created <= endDate && x.Profit > 0).Sum(x => (decimal?)x.Profit) ?? 0;

            }
            else if (invoiceNo)
            {
                gridData.Count = _repository.GetAllActive(getCreatedCompanyId())
                                                .Where(x => x.InvoiceNo.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate).Count();

                gridData.DueCount = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.InvoiceNo.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Due > 0).Count();

                gridData.TotalAmount = _repository.GetAllActive(getCreatedCompanyId())
                    .Where(x => x.InvoiceNo.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate).Sum(x => (decimal?)x.Amount) ?? 0;

                gridData.TotalDue = _repository.GetAllActive(getCreatedCompanyId())
                   .Where(x => x.InvoiceNo.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Due > 0).Sum(x => (decimal?)x.Due) ?? 0;

                gridData.Profit = _repository.GetAllActive(getCreatedCompanyId())
                   .Where(x => x.InvoiceNo.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Profit > 0).Sum(x => (decimal?)x.Profit) ?? 0;

                query = query.Where(x => x.InvoiceNo.Contains(request.Keyword));
            }
            else
            {
                if (request.Keyword.Contains("01"))
                {
                    gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                                .Where(x => x.Customer.Phone.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate).Count();

                    gridData.DueCount = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                        .Where(x => x.Customer.Phone.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Due > 0).Count();

                    gridData.TotalAmount = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                        .Where(x => x.Customer.Phone.Contains(request.Keyword) && x.Customer.Name.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate).Sum(x => (decimal?)x.Amount) ?? 0;

                    gridData.TotalDue = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                       .Where(x => x.Customer.Phone.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Due > 0).Sum(x => (decimal?)x.Due) ?? 0;

                    gridData.Profit = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                       .Where(x => x.Customer.Phone.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Profit > 0).Sum(x => (decimal?)x.Profit) ?? 0;

                    query = query.Where(x => x.Customer.Phone.Contains(request.Keyword));

                }
                else
                {
                    gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                                .Where(x => x.Customer.Name.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate).Count();

                    gridData.DueCount = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                        .Where(x => x.Customer.Name.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Due > 0).Count();

                    gridData.TotalAmount = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                        .Where(x => x.Customer.Name.Contains(request.Keyword) && x.Customer.Name.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate).Sum(x => (decimal?)x.Amount) ?? 0;

                    gridData.TotalDue = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                       .Where(x => x.Customer.Name.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Due > 0).Sum(x => (decimal?)x.Due) ?? 0;

                    gridData.Profit = _repository.GetAllActive(getCreatedCompanyId()).Include(x => x.Customer)
                       .Where(x => x.Customer.Name.Contains(request.Keyword) && x.Created >= startDate && x.Created <= endDate && x.Profit > 0).Sum(x => (decimal?)x.Profit) ?? 0;

                    query = query.Where(x => x.Customer.Name.Contains(request.Keyword));

                }

            }


          

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "InvoiceNo": query = query.OrderBy(l => l.InvoiceNo); break;
                    case "Due": query = query.OrderBy(l => l.Due); break;
                    case "Created": query = query.OrderBy(l => l.Created); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "InvoiceNo": query = query.OrderByDescending(l => l.InvoiceNo); break;
                    case "Due": query = query.OrderByDescending(l => l.Due); break;
                    case "Created": query = query.OrderByDescending(l => l.Created); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }


            query = query.Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Sale> sales = query.ToList();

            foreach (var sale in sales)
            {
                gridData.Data.Add(new SaleViewModel(sale));
            }

            return gridData;
        }

        public bool DueUpdate(string invoiceNo, decimal due)
        {
            Sale record = _repository.GetAllActive().Where(x => x.InvoiceNo == invoiceNo).FirstOrDefault();
            if(due > -1 && due < record.Due)
            {
                record.Due = due;
                _repository.Commit();
                return true;
            }
            return false;
            
        }

        public SaleViewModel ReturnSale(string invoiceNo)
        {
            Sale returnableSale = _repository.GetAllActive().Include(x=>x.SalesDetails).FirstOrDefault(x=>x.InvoiceNo == invoiceNo);
            
            foreach(var item in returnableSale.SalesDetails)
            {
                Product product = _productRepository.GetById(item.ProductId);
                product.Stock = product.Stock + item.Quantity;
                _productRepository.Commit();
            }

            returnableSale.Active = false;
            _repository.Commit();
            return new SaleViewModel(returnableSale);
        }
    }
}