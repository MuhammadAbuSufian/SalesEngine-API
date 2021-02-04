using System;
using System.Linq.Expressions;
using Project.Model;

namespace Project.RequestModel
{
    public class ProductRequestModel : BaseRequestModel<Product>
    {
        public ProductRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<Product, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x => x.Name.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
}