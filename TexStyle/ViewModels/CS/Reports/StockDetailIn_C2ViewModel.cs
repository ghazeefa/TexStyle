using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StockDetailIn_C2ViewModel:DefaultViewModel
    {
        public StockDetailIn_C2ViewModel()
        {
            StockDetailIn1_C2ViewModels = new List<StockDetailIn1_C2ViewModel>();
        }
        public string Name { get; set; }

        public List<StockDetailIn1_C2ViewModel> StockDetailIn1_C2ViewModels { get; set; }
    }
}
