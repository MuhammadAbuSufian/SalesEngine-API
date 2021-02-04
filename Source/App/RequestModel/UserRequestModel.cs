using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.RequestModel
{
    public class UserRequestModel: BaseRequestModel<User>
    {
        public UserRequestModel(string keyword, string orderBy, string isAscending) :base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<User, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x =>  x.Email.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
}
