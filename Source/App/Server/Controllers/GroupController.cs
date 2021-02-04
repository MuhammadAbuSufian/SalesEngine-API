using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Project.Model;
using Project.RequestModel;
using Project.Service;

namespace Project.Server.Controllers
{
    public class GroupController : ApiController
    {
        private IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/group/save")]
        public IHttpActionResult SaveGroup(Group group)
        {
            if (string.IsNullOrEmpty(group.Id))
            {
                return Ok(_service.Add(group));
            }
            else
            {
                return Ok(_service.UpdateGroup(group));
            }
        }


        [HttpPost]
        [Route("api/group/get")]
        public IHttpActionResult GetGroup(GridRequestModel request)
        {
            return Ok(_service.GetGridData(request));
        }

        [HttpGet]
        [Route("api/group/typeahead")]
        public IHttpActionResult MedicineGroupTypeAhead(string key)
        {
            return Ok(_service.GetDropdownData(key));
        }
    }
}
