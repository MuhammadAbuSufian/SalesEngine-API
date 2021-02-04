using System;
using System.Linq.Expressions;
using Project.Model;

namespace Project.RequestModel
{
    public class PurchaseRequestModel : BaseRequestModel<Purchase>
    {
        public PurchaseRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<Purchase, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x => x.InvoiceNo.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
}