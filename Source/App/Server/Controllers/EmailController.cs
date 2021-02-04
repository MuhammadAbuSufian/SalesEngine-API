using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.RequestModel;
using Project.Service;

namespace Project.Server.Controllers
{
    public class EmailController : ApiController
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("api/Email/Attendance")]
        public bool SendAttendance(EmailRequestModel request)
        {
            return _emailService.Send(request);
        }



    }
}
