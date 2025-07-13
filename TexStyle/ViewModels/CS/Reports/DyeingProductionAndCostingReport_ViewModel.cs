using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DyeingProductionAndCostingReport_ViewModel: DefaultViewModel
    {
        public string ProcessName { get; set; }
        public decimal? TotalKgs { get; set; }
        public decimal? ChemicalAmount { get; set; }
        public decimal? DyeAmount { get; set; }
        public decimal? ChemicalCostPerKgs { get; set; }
        public decimal? DyeCostPerKgs { get; set; }
        public decimal? TotalCostPerKg { get; set; }
    }
}
