using System;
using System.Linq.Expressions;
using Project.Model;

namespace Project.RequestModel
{
    public class SalesDetailRequestModel : BaseRequestModel<SalesDetail>
    {
        public SalesDetailRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<SalesDetail, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x => x.Sale.InvoiceNo.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
}