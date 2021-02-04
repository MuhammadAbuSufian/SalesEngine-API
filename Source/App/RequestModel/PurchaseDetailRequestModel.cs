using System;
using System.Linq.Expressions;
using Project.Model;

namespace Project.RequestModel
{
    public class PurchaseDetailRequestModel : BaseRequestModel<PurchaseDetail>
    {
        public PurchaseDetailRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<PurchaseDetail, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x => x.Purchase.InvoiceNo.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
}