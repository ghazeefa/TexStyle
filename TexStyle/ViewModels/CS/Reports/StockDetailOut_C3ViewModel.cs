using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StockDetailOut_C3ViewModel: DefaultViewModel
    {
        public StockDetailOut_C3ViewModel()
        {
            StockDetailOut1_C3ViewModels = new List<StockDetailOut1_C3ViewModel>();
        }
        public string Name { get; set; }

        public List<StockDetailOut1_C3ViewModel> StockDetailOut1_C3ViewModels { get; set; }

    }
}
