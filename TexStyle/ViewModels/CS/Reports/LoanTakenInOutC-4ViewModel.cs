using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class LoanTakenInOutC_4ViewModel: DefaultViewModel
    {
        public LoanTakenInOutC_4ViewModel()
        {
            OGP_IGPDetailListViewModel = new List<OGP_IGPDetailListViewModel>();
        }

        public DateTime Date{ get; set; }
        public string Item{ get; set; }
        public long? IGPNo  { get; set; }
        public decimal? Qty  { get; set; }

        public List<OGP_IGPDetailListViewModel> OGP_IGPDetailListViewModel { get; set; }
    }
}
