using System;
using System.Linq.Expressions;
using Project.Model;

namespace Project.RequestModel
{
    public class CompanyRequestModel : BaseRequestModel<Company>
    {
        public CompanyRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<Company, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x => x.Email.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
    
}