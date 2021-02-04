using System;
using System.Linq.Expressions;
using Project.Model;

namespace Project.RequestModel
{
    public class GroupRequestModel : BaseRequestModel<Group>
    {
        public GroupRequestModel(string keyword, string orderBy, string isAscending) : base(keyword, orderBy, isAscending)
        {

        }
        public override Expression<Func<Group, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x => x.Name.Contains(Keyword);
            }

            return ExpressionObj;
        }
    }
}