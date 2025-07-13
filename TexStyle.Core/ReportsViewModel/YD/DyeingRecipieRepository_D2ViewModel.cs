using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.YD
{
   public  class DyeingRecipieRepository_D2ViewModel
    {
        public string LPS { get; set; }
        public string MachineType { get; set; }
        public string Format { get; set; }
        public decimal Weight { get; set; }
        public int LotNo { get; set; }
        public string Color { get; set; }
        public string Buyer { get; set; }
        public decimal LiquorRate { get; set; }
        public DateTime Date { get; set; }
        public decimal No { get; set; }
        public decimal? Kgs { get; set; }
        public decimal Gpl { get; set; }
        public decimal Percentage { get; set; }
        public decimal? Water { get; set; }
        public long?  RecipeStepId { get; set; }
        public decimal Rate { get; set; }
        public decimal? Cost { get; set; }
        public string ItemName { get; set; }
        public string RecipeName { get; set; }
        public bool? IsReprocessed { get; set; }
        public string Party { get; set; }
        public bool IsYarn { get; set; }
        public bool IsWithoutLPS { get; set; }
        public string YarnType { get; set; }
        public string FabricType { get; set; }
        public int Status { get; set; }

        public string ShiftName { get; set; }
        public string ShiftInchargeName { get; set; }
        public long? ShiftInchargeCode { get; set; }
        public string MachineOperatorName { get; set; }
        public long? MachineOperatorCode { get; set; }
        public string HelperName { get; set; }
        public long? HelperCode { get; set; }
        public string CStoreOperatorName { get; set; }
        public long? CStoreOperatorCode { get; set; }
        public DateTime? MachineStartTime { get; set; }
        public DateTime? MachineUnloadTime { get; set; }
        public DateTime? SoapingDrainTime { get; set; }
        public bool? IsGarmentPrinting { get; set; }
        public bool? IsGarmentDyeing { get; set; }
        public bool? IsFabricPrinting { get; set; }
        public decimal Pcs { get; set; }

    }
}
