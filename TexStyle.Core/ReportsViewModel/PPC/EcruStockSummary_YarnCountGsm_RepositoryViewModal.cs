using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
   public class EcruStockSummary_YarnCountGsm_RepositoryViewModal
    {
        public string Buyer { get; set; }
        public string FabricType { get; set; }
        public string FabricQuality { get; set; }
        public long? GSM { get; set; }
        public string YarnCountOfFabric { get; set; }
        public decimal? TotalWeight { get; set; }
    }
}
