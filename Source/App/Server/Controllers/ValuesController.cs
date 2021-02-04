using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Project.Model;
using Project.Server.Filters;

namespace Project.Server.Controllers
{
    [CustomAuthorization]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            //BusinessDbContext context=new BusinessDbContext();
            //return this.Ok(context.Accounts);
            return Ok("Test 2 Success");
        }

        // GET api/values/5
        [AllowAnonymous]
        public async Task<string> Get(int id)
        {
            Console.WriteLine("requested");
            await Task.Delay(10000);
            return "value is "+ id;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
