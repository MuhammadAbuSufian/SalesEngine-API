using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipDataAccess.Enums
{
    public enum EpbReferenceNo
    {
        DEV = ServiceOffice.All,
        DH = ServiceOffice.Dhaka,
        NR = ServiceOffice.Narayanganj,
        CH = ServiceOffice.Chittagong,
        CO = ServiceOffice.Comilla,
        ED = ServiceOffice.Epz
    }

    public enum ExporterCategory
    {
        Textile = 1,
        NonTextile = 2
    }

    public enum ServiceOffice
    {
        All = 0,
        Dhaka = 1,
        Narayanganj = 2,
        Chittagong = 3,
        Comilla = 4,
        Epz = 5
    }


    public enum SyncTable
    {
        Exporter = 1,
    }
}
