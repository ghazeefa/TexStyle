using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
    public class DyedStockRepository_P4ViewModel
    {
        public long? LPSId { get; set; }
        public int? LotNo { get; set; }
        public decimal? EcruKg { get; set; }
        public decimal? RemainingKgs { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string Buyer { get; set; }
        public long? Po { get; set; }
        public string Shade { get; set; }
        public decimal? Loss { get; set; }
        //public string ReceivedQualityStatus { get; set; }
        public string YarnType { get; set; }
      
        public string YarnQuality { get; set; }

        //public DateTime? ProductionDate { get; set; }
        public string StoreLocation { get; set; }
        public string Status { get; set; }
        public decimal? DyedBags { get; set; }
        public string FabricType { get; set; }
  
        public long? GSM { get; set; }
        public long? Dia { get; set; }

        public bool IsYarn { get; set; }
        public decimal? TotalWeight { get; set; }

    }
}
