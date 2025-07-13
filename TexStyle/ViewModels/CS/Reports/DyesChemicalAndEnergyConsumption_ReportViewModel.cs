using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DyesChemicalAndEnergyConsumption_ReportViewModel: DefaultViewModel
    {
        public string ItemName { get; set; }
        public int IsChemical { get; set; }
        public decimal? WeightUsed { get; set; }
        public decimal? RatePerKg { get; set; }
        public decimal? Cost { get; set; }
        public decimal FabricKGs { get; set; }
        public DateTime? EnergyEntryDate { get; set; }
        public decimal? ElectricityCost { get; set; }
        public decimal? GassCost { get; set; }
        public decimal? CoalCost { get; set; }
        public decimal? SalaryCost { get; set; }
        public decimal? DispatchedKgs { get; set; }
        public decimal? DispatchedAmount { get; set; }

    }
}
