using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class BranchViewModel : BaseViewModel<Branch>
    {
        public BranchViewModel(Branch model)
        {
            Id = model.Id;
            Created = model.Created;
            CreatedBy = model.CreatedBy;
            Modified = model.Modified;
            ModifiedBy = model.ModifiedBy;
            Active = model.Active;

            Name = model.Name;
           
        }

        public string Name { get; set; }
    }
}
