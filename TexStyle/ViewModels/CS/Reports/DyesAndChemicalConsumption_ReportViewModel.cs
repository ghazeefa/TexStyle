using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DyesAndChemicalConsumption_ReportViewModel : DefaultViewModel
    {
        public string ItemName { get; set; }
        public int IsChemical { get; set; }
        public decimal? WeightUsed { get; set; }
        public decimal? Cost { get; set; }
        public decimal? RatePerKg { get; set; }
        public decimal FabricKGs { get; set; }

    }
}
