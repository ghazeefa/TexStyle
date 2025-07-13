using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Reports
{
    public class LocalPurchaseDetailListViewModel
    {
        public long No { get; set; }
        public DateTime Date { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
