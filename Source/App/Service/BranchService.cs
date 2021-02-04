using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.ViewModel;

namespace Project.Service
{
    public interface IBranchService : IBaseService<Branch, BranchViewModel>
    {

    }

    public class BranchService : BaseService<Branch, BranchViewModel>, IBranchService
    {
        public BranchService(IBranchRepository repository) : base(repository)
        {

        }
    }
}
