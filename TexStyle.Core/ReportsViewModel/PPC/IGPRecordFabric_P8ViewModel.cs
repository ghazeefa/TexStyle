using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
   public class IGPRecordFabric_P8ViewModel
    {

        public long? DetailId { get; set; }
        public long? HeaderId { get; set; }
        public long? FabricTypesId { get; set; }
        public long? PartyId { get; set; }
        public long? FabricQualityId { get; set; }
        public long? KnitingPartyId { get; set; }

      
        public decimal? RemainingKgs { get; set; }
        public decimal? NetWeightInKg { get; set; }


        public decimal? Plannedkgs { get; set; }
        public decimal? NoOfRolls { get; set; }

        public DateTime? IgpDate { get; set; }
        public string Buyer { get; set; }

        public long GateReffId { get; set; }
        public string FabricType { get; set; }

        public string FabricQuality { get; set; }
        public string StoreLocation { get; set; }
        
        public string Party { get; set; }
        public string Knitter { get; set; }

        public long? GSM { get; set; }
        public long? Dia { get; set; }





    }
}
