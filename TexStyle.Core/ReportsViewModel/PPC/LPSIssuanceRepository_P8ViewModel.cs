using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
   public class LPSIssuanceRepository_P8ViewModel
    {


        public long? LPSId { get; set; }
        public int? LotNo { get; set; }
        public decimal? EcruKg { get; set; }        
        public DateTime IssuanceDate { get; set; }
        public string Buyer { get; set; }
        public long? Po { get; set; }
        public string FactoryPo { get; set; }
        public string Color { get; set; } 
        public string FabricType { get; set; }
        public long? GSM { get; set; }
        public long? Dia { get; set; }
        public bool? IsYarn { get; set; }
        public bool IsIssued { get; set; }



    }
}
