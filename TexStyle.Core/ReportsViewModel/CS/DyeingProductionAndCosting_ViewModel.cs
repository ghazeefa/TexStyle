using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
    public class DyeingProductionAndCosting_ViewModel
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
