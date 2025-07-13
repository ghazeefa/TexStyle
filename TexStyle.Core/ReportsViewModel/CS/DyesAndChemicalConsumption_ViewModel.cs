using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
    public class DyesAndChemicalConsumption_ViewModel
    {
        public string ItemName { get; set; }
        public int IsChemical { get; set; }
        public decimal? WeightUsed { get; set; }
        public decimal? RatePerKg { get; set; }
        public decimal? Cost { get; set; }
        public decimal FabricKGs { get; set; }
    }
}
