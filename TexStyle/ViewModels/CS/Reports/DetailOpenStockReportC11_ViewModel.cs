using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DetailOpenStockReportC11_ViewModel: DefaultViewModel
    {
        public DetailOpenStockReportC11_ViewModel()
        {
           DetailOpenStockDetailReportC11_ViewModel = new List<DetailOpenStockDetailReportC11_ViewModel>();
        }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public List<DetailOpenStockDetailReportC11_ViewModel> DetailOpenStockDetailReportC11_ViewModel { get; set; }
        

    }
}
