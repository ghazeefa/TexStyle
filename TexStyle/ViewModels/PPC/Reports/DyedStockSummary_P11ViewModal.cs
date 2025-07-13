using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class DyedStockSummary_P11ViewModal : DefaultViewModel
    {
        public DyedStockSummary_P11ViewModal()
        {
            DyedStockSummaryDetail_P11ViewModal = new List<DyedStockSummaryDetail_P11ViewModal>();
        }
        public decimal? Total { get; set; }
        public decimal? Total2 { get; set; }
        public string Buyer { get; set; }
        public string Shade { get; set; }

        public List<DyedStockSummaryDetail_P11ViewModal> DyedStockSummaryDetail_P11ViewModal { get; set; }



    }
}
