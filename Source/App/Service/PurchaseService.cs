using System.Collections.Generic;
using System.Data.Entity;
using Project.Model;
using Project.Repository;
using Project.ViewModel;
using System.Linq;
using Project.RequestModel;

namespace Project.Service
{
    public interface IPurchaseService : IBaseService<Purchase, PurchaseViewModel>
    {
        string GetRecordId();

        GridResponseModel<PurchaseViewModel> GetGridData(GridRequestModel request);

    }

    public class PurchaseService : BaseService<Purchase, PurchaseViewModel>, IPurchaseService
    {
        private readonly IPurchaseRepository _repository;

        public PurchaseService(IPurchaseRepository repository) : base(repository)
        {
            _repository = repository;

        }
        public string GetRecordId()
        {
            int count = this.GetAllActive().Count();
            string recordId = "PO-";

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
                recordId = "PO-00000000001";
            }
            return recordId;
        }

        public GridResponseModel<PurchaseViewModel> GetGridData(GridRequestModel request)
        {
            GridResponseModel<PurchaseViewModel> gridData = new GridResponseModel<PurchaseViewModel>();

            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId())
                .Include(x => x.PurchaseDetails)
                .Include(x => x.PurchaseDetails.Select(y => y.Product))
                .Include(x => x.PurchaseDetails.Select(y => y.Product.Group))
                .Include(x => x.PurchaseDetails.Select(y => y.Product.Brand));

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

            List<Purchase> purchases = query.ToList();

            foreach (var purchase in purchases)
            {
                gridData.Data.Add(new PurchaseViewModel(purchase));
            }

            return gridData;
        }
    }
}