using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class P10EcruStockSummaryViewModal : DefaultViewModel
    {
        public P10EcruStockSummaryViewModal()
        {
            P10EcruStockSummaryDetailViewModal = new List<P10EcruStockSummaryDetailViewModal>();
        }
       
        public string Buyer { get; set; }
        public decimal? Sum { get; set; }

        public List<P10EcruStockSummaryDetailViewModal> P10EcruStockSummaryDetailViewModal { get; set; }




    }
}
