using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class IGPRecordFabric_P8_ViewModel
    {
        public long? DetailId { get; set; }
        public long? HeaderId { get; set; }

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
