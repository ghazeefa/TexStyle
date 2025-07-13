using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DyeChemicalLoanReturnOut_RemainBlncViewModel
    {
        public long headerId { get; set; }
        public long TrType { get; set; }
        public decimal BalanceQtyDr { get; set; }
        public long LRO_TrType { get; set; }
        public long Sno { get; set; }
        public long DetailId { get; set; }
        public decimal Rate { get; set; }
    }
}
