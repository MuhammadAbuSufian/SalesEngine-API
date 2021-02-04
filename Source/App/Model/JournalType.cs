using Project.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class JournalType : EntityBase
    {
        public string Name { get; set; }
        public string Note { get; set; }
    }
}
