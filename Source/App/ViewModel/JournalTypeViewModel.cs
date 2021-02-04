using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class JournalTypeViewModel : BaseViewModel<JournalType>
    {
        public JournalTypeViewModel(JournalType model) : base(model)
        {
            Name = model.Name;
            Note = model.Note;
        }
        public string Name { get; set; }
        public string Note { get; set; }
    }
}
