using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
    public class DailyProductionRepository_P3ViewModel
    {
        public long? LPSId { get; set; }
        public int? LotNo { get; set; }
        public decimal? EcruKg { get; set; }
        public decimal? Loss { get; set; }

        public decimal? DyedKg { get; set; }

        public DateTime IssuedDate { get; set; }

        public string Buyer { get; set; }
        public long? Po { get; set; }
        public int? Cones { get; set; }
        public string Color { get; set; }
        public decimal? Bags { get; set; }
        public string ReceivedQualityStatus { get; set; }
        public string YarnType { get; set; }
        public string YarnQualities { get; set; } 
        public string Status { get; set; }
        public string FabricType { get; set; }

        public long? GSM { get; set; }
        public long? Dia { get; set; }

        public bool? IsYarn { get; set; }
        //public bool IsIssued { get; set; }


    }
}
