using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class EmployeeViewModel : BaseViewModel<Employee>
    {
        public EmployeeViewModel(Employee model)
        {
            Id = model.Id;
            Created = model.Created;
            CreatedBy = model.CreatedBy;
            Modified = model.Modified;
            ModifiedBy = model.ModifiedBy;
            Active = model.Active;

            Name = model.Name;
            Address = model.Address;
            Age = model.Age;
            Phone = model.Phone;

        }

        public string Name { get; set; }

        public string Address { get; set; }
        public string Age { get; set; }

        public string Phone { get; set; }
    }
}
