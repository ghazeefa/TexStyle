using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class LoanPartyInOutC5ViewModel: DefaultViewModel
    {
        public   LoanPartyInOutC5ViewModel()
        {
             OGP_IGPDetailListViewModel = new List<OGP_IGPDetailListViewModel>();
        }

        public DateTime Date { get; set; }
        public string Item { get; set; }
        public long? OGPNo { get; set; }
        public decimal? Qty { get; set; }

        public List<OGP_IGPDetailListViewModel> OGP_IGPDetailListViewModel { get; set; }
    }
}
