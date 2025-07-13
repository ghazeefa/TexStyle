using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
   public class EcruFabricIssuenceViewModel
    {
        public long LpsId { get; set; }
        public string FactoryPO { get; set; }
        public string Fabric { get; set; }
        public DateTime IssuedDate { get; set; }
        public int LotNo { get; set; }
        public decimal IssuedKgs { get; set; }
        public string PartyName { get; set; }
        public string BuyerName { get; set; }
        public string BuyerColorName { get; set; }
    }
}
