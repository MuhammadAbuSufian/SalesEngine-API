using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{ 
    public interface ICustomerService : IBaseService<Customer, CustomerViewModel>
    {
        List<DropdownViewModel> GetCompanyDropdownData();
        bool UpdateCustomer(Customer customer);
        GridResponseModel<CustomerViewModel> GetGridData(GridRequestModel request);
        List<DropdownViewModel> CustomerSerach(string key);
    }

    public class CustomerService : BaseService<Customer, CustomerViewModel>, ICustomerService
    {
        
        private readonly ICustomerRepository _repository;


        public CustomerService(ICustomerRepository repository) : base(repository)
        {
            _repository = repository;


        }

        public List<DropdownViewModel> GetCompanyDropdownData()
        {
            List<DropdownViewModel> customers = GetAllActive().Select(
                x => new DropdownViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }
            ).ToList();

            return customers;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var updateBrand = _repository.GetById(customer.Id);
            updateBrand.Name = customer.Name;
            updateBrand.Phone = customer.Phone;
            updateBrand.Address = customer.Address;

            var user = this.GetUserFromToken();

            updateBrand.Modified = DateTime.Now;
            updateBrand.ModifiedBy = user.Id;

            return _repository.Commit();
        }

        public GridResponseModel<CustomerViewModel> GetGridData(GridRequestModel request)
        {
            GridResponseModel<CustomerViewModel> gridData = new GridResponseModel<CustomerViewModel>();

            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId());

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

            List<Customer> customers = query.ToList();

            foreach (var customer in customers)
            {
                gridData.Data.Add(new CustomerViewModel(customer));
            }

            return gridData;
        }
        public List<DropdownViewModel> CustomerSerach(string key)
        {
            var query = _repository.GetAllActive(getCreatedCompanyId()).Where(x => x.Name.Contains(key));

            List<Customer> customers = query.OrderBy(x => x.Name).Take(10).ToList();

            List<DropdownViewModel> returnable = new List<DropdownViewModel>();

            foreach (var cus in customers)
            {
                returnable.Add(new DropdownViewModel(cus.Id, cus.Name, cus.Name));
            }

            return returnable;
        }
    }
}
