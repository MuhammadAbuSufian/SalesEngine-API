using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Project.Model;

namespace Project.Server.Filters
{
    public class CustomAuthorization : ActionFilterAttribute
    {
        public bool Disable { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            return;
            if (Disable) return;

            var authorizationParameter = actionContext.Request.Headers.Authorization.ToString();

            BusinessDbContext _context = new BusinessDbContext();

            var user = _context.Users.FirstOrDefault();

            if (user == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("User Not Authenticated") };
            }

            base.OnActionExecuting(actionContext);
        }
    }
}