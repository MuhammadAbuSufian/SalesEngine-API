using System;
using System.Linq.Expressions;
using Project.Model;

namespace Project.RequestModel
{
    public class BrandRequestModel : BaseRequestModel<Brand>
    {
        public BrandRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<Brand, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x => x.Name.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
}