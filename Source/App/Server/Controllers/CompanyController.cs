using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.Service;
using Project.ViewModel;

namespace Project.Server.Controllers
{
    public class CompanyController : BaseController<Company, CompanyViewModel, CompanyRequestModel>
    {

        private readonly CompanyService _service;
        public CompanyController(CompanyService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/company/Add")]
        public bool AddCompany(Company request)
        {
            var company = _service.Add(request);
            if (company == null)
            {
                return false;
                        
            }
            return true;
        }
        
        [HttpGet]
        [Route("api/company/get")]
        public List<CompanyViewModel> GetCompany()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("api/company/getBalence")]
        public IHttpActionResult GetBalence()
        {
            var user = _service.GetUserFromToken();
            return Ok(_service.GetById(user.CompanyId).Balence);
        }

        [HttpGet]
        [Route("api/company/dropdown")]
        public IHttpActionResult CompanyDropdown()
        {
            return Ok(_service.GetCompanyDropdownData());
        }

    }
}
