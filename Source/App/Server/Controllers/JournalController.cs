using Project.Model;
using Project.RequestModel;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Project.Server.Controllers
{
    public class JournalController : ApiController
    {
        private IJournalService _service;
        private ICompanyService _companyService;
        public JournalController(IJournalService service, ICompanyService companyService)
        {
            _service = service;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("api/journal/save")]
        public IHttpActionResult SaveJournal(Journal model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                return Ok(_service.Add(model));
            }
            else
            {
                return Ok(_service.Update(model));
            }
        }


        [HttpPost]
        [Route("api/journal/get")]
        public IHttpActionResult GetJournal(GridRequestModel request)
        {
            return Ok(_service.GetGridData(request));
        }

        [HttpPost]
        [Route("api/journal/getByDate")]
        public IHttpActionResult GetJournal(GridRequestModel request, DateTime startDate, DateTime endDate)
        {
            return Ok(_service.GetGridData(request,startDate, endDate));
        }
        [HttpPost]
        [Route("api/journal/getCurrentUser")]
        public IHttpActionResult GetGridDataByUser(GridRequestModel request)
        {
            return Ok(_service.GetGridDataByUser(request));
        }

        [HttpGet]
        [Route("api/journal/makeApprove")]
        public IHttpActionResult JournalMakeApprove(string id)
        {
            return Ok(_service.MakeApprove(id));
        }

        [HttpGet]
        [Route("api/journal/makePaid")]
        public IHttpActionResult JournalMakePaid(string id, decimal amount)
        {
            var statusChanged = _service.MakePaid(id);
            var amountDeducted = _companyService.DeductBalence(amount);
            var result = false;
            if (statusChanged == true && amountDeducted == true) result = true;
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/journal/delete")]
        public IHttpActionResult DeleteJournal(string id)
        {
            return Ok(_service.Delete(id));
        }


        [HttpPost]
        [Route("api/journaltype/get")]
        public IHttpActionResult GetJournalType(GridRequestModel request)
        {
            return Ok(_service.GetJournalTypeGridData(request));
        }


        [HttpPost]
        [Route("api/journaltype/save")]
        public IHttpActionResult SaveJournalType(JournalType model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                return Ok(_service.SaveJournalType(model));
            }
            else
            {
                return Ok(_service.UpdateJournalType(model));
            }
        }

        [HttpGet]
        [Route("api/journalType/dropdown")]
        public IHttpActionResult JournalTypeAhead()
        {
            return Ok(_service.GetJournalTypeDropdownData());
        }
    }
}