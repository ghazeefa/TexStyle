using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class P7EcruYarnStockReportViewModel: DefaultViewModel


    {
        public P7EcruYarnStockReportViewModel()
        {
            P7EcruYarnStockReportDetailViewModel = new List<P7EcruYarnStockReportDetailViewModel>();
        }
        public long No { get; set; }
        public string Date { get; set; }
        public bool IsYarn { get; set; }
        public decimal TotalSum { get; set; }
    
        public List<P7EcruYarnStockReportDetailViewModel> P7EcruYarnStockReportDetailViewModel { get; set; }
    }
}
