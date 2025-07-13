using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
    public class LoanTakenInOutRepository_C4ViewModel
    {

        public DateTime? IGPDate { get; set; }
        public string Name { get; set; }
        public long? IGPNo { get; set; }
        public decimal? IGPQty { get; set; }
        public DateTime? OGPDate { get; set; }
        public long? OGPNo { get; set; }
        public decimal? OGPQty { get; set; }

        //public List<OGP_IGPDetailListViewModel> OGP_IGPDetailListViewModel { get; set; }
    }
}
