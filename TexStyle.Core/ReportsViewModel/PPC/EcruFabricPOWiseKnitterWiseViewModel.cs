using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
   public class EcruFabricPOWiseKnitterWiseViewModel
    {
        public long GateTrId { get; set; }
        public DateTime IgpDate { get; set; }
        public string Buyer { get; set; }
        public string FactoryPO { get; set; }
        public string FabricQuality { get; set; }
        public string FabricType { get; set; }
        public long Dia { get; set; }
        public long GSM { get; set; }
        public decimal NetWeightInKg { get; set; }
        public string Knitter { get; set; }
    }
}
