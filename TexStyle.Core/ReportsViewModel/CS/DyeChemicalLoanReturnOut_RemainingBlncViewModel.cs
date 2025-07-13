using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
    public class DyeChemicalLoanReturnOut_RemainingBlncViewModel
    {
        public long headerId { get; set; }
        public long TrType { get; set; }
        public decimal BalanceQtyDr { get; set; }
        public long? LRO_TrType { get; set; }
        public long Sno { get; set; }
        public long DetailId { get; set; }
        public decimal? Rate { get; set; }
        public string Name { get; set; }

    }
}
