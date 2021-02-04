using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.RequestModel
{
    public class CustomerRequestModel : BaseRequestModel<Customer>
    {
        public CustomerRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<Customer, bool>> GetExpression()
        {
            return null;
        }
    }
}
