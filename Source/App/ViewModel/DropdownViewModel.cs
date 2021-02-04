using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class DropdownViewModel 
    {
        public DropdownViewModel()
        {
            
        }

        public DropdownViewModel(string id, string name, string common)
        {
            Id = id;
            Name = name;
            Common = common;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Common { get; set; }

        public string ExtraData { get; set; }
    }
}
