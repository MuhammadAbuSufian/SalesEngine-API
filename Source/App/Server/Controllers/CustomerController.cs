using Project.Model;
using Project.RequestModel;
using Project.Service;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Server.Controllers
{
    public class CustomerController : BaseController<Customer, CustomerViewModel, CustomerRequestModel>
    {

        private readonly CustomerService _service;
        public CustomerController(CustomerService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/customer/Add")]
        public IHttpActionResult AddCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Id))
            {
                return Ok(_service.Add(customer));
            }
            else
            {
                return Ok(_service.UpdateCustomer(customer));
            }
        }

        [HttpPost]
        [Route("api/customer/get")]
        public IHttpActionResult GetCustomer(GridRequestModel request)
        {
            return Ok(_service.GetGridData(request));
        }


        [HttpGet]
        [Route("api/customer/typeahead")]
        public IHttpActionResult GetCustomerTypeAhead(string key)
        {
            return Ok(_service.CustomerSerach(key));
        }

        [HttpGet]
        [Route("api/customer/dropdown")]
        public IHttpActionResult CompanyDropdown()
        {
            return Ok(_service.GetCompanyDropdownData());
        }

    }
}
