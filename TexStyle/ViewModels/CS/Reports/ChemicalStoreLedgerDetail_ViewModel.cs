using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Reports
{
    public class ChemicalStoreLedgerDetail_ViewModel
    {
        public string TrName { get; set; }
        public decimal? QtyDr { get; set; }
        public decimal? QtyCr { get; set; }
        public decimal Rate { get; set; }
        public long Sno { get; set; }
        public decimal? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Status { get; set; }
        public decimal FinalQty { get; set; }
    }
}
