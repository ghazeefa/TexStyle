using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;
namespace TexStyle.ViewModels.CS.Reports
{
    public class LatestRateReportC14_ViewModel : DefaultViewModel
    {

        //public DateTime TransactionDate { get; set; }
        //public DateTime Date { get; set; }

        //public string Item { get; set; }

        //public Decimal Rate { get; set; }



        public LatestRateReportC14_ViewModel()
        {
            this.LatestRateReportDetailC14_ViewModel = new List<LatestRateReportDetailC14_ViewModel>();
        }


        public string Date { get; set; }

        public List<LatestRateReportDetailC14_ViewModel> LatestRateReportDetailC14_ViewModel { get; set; }








    }
}
