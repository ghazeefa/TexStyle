using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.ViewModels.PPC.Reports 
{
   public class DyeingLossPOWiseColorWiseReportViewModel : DefaultViewModel
    {
        public string Po { get; set; }
        public int? LotNo { get; set; }
        public decimal? DyedKgs { get; set; }
        public decimal? DispatchedKgs { get; set; }
        public decimal? BalanceKgs { get; set; }
        public decimal? PercentageBalance { get; set; }
        public string YarnType { get; set; }
        public string FabricType { get; set; }
        public string Party { get; set; }
        public string Buyer { get; set; }
        public string BuyerColor { get; set; }
        
    }
}
