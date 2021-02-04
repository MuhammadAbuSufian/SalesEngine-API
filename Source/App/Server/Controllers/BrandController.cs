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
    public class BrandController : ApiController
    {
        private IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/brand/save")]
        public IHttpActionResult SaveBrand(Brand brand)
        {
            if (string.IsNullOrEmpty(brand.Id))
            {
                return Ok(_service.Add(brand));
            }
            else
            {
                return Ok(_service.UpdateBrand(brand));
            }
        }


        [HttpPost]
        [Route("api/brand/get")]
        public IHttpActionResult GetCompany(GridRequestModel request)
        {
            return Ok(_service.GetGridData(request));
        }

        [HttpGet]
        [Route("api/brand/typeahead")]
        public IHttpActionResult CompanyTypeAhead(string key)
        {
            return Ok(_service.GetDropdownData(key));
        }
    }
}
