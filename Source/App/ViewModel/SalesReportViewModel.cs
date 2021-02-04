using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class SalesReportViewModel<TVm> where TVm : class
    {
        public List<TVm> Data = new List<TVm>();
        public int Count;
        public int DueCount;
        public decimal? TotalAmount;
        public decimal? TotalDue;
        public decimal? Profit;

    }
}
