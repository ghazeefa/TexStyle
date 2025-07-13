using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.CS;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StockSummary2ViewModel
    {
       public List<StockOutSummaryViewModel> StockOutSummaryViewModel { get; set; }
        public List<StockOutSummaryReportRepository_ViewModel1> StockOutSummaryViewModel1 { get; set; }
        public List<StockOutSummaryReportRepository_ViewModel2> StockOutSummaryViewModel2 { get; set; }
        public List<DyeChemicalDetailsTrTypeWiseRepository_ViewModel> DyeChemicalDetailsTrTypeWise { get; set; }
    }
}
