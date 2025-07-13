using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class P10EcruStockSummaryYarnContGsmViewModal : DefaultViewModel
    {       
        public string Buyer { get; set; }
        public string FabricType { get; set; }
        public string FabricQuality { get; set; }
        public long? GSM { get; set; }
        public string YarnCountOfFabric { get; set; }
        public decimal? TotalWeight { get; set; }      
    }
}
