using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class CustomerViewModel: BaseViewModel<Customer>
    {
        public CustomerViewModel(Customer model): base(model)
        {
            this.Name = model.Name;
            this.Phone = model.Phone;
            this.Address = model.Address;
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}

