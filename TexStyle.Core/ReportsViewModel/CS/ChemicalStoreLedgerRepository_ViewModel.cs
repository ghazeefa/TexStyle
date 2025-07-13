using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
   public  class ChemicalStoreLedgerRepository_ViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string TrName { get; set; }
        public decimal? QtyDr { get; set; }
        public decimal? QtyCr { get; set; }
        public decimal Rate { get; set; }
        public long Sno { get; set; }
        public decimal? Amount { get; set; }
        public string ItemName { get; set; }
        public int Status { get; set; }
        public decimal? FinalQty { get; set; }
    }
}
