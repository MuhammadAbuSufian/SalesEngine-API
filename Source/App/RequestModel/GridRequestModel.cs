using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RequestModel
{
    public class GridRequestModel
    {
        public string Keyword { get; set; }
        public string OrderBy { get; set; }
        public bool IsAscending { get; set; }

        public int Page { get; set; }
        public int PerPageCount { get; set; }
    }
}
