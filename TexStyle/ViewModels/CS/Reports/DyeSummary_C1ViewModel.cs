using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DyeSummary_C1ViewModel:DefaultViewModel
    {
        public string Status { get; set; }
        public DyeSummary_C1ViewModel()
        {
            SummaryDetail = new List<DyeSummaryDetail_C1ViewModel>();
        }

        public string Date { get; set; }
        public decimal KgSum { get; set; }



        public List<DyeSummaryDetail_C1ViewModel> SummaryDetail { get; set; }
    }
}
