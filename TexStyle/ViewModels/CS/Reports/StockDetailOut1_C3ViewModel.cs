using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StockDetailOut1_C3ViewModel
    {
        public DateTime? LoanTakenOutDate { get; set; }
        public long? LoanTakenOutNo { get; set; }
        public decimal? LoanTakenOutQty { get; set; }
        public DateTime? StoreReturnNoteDate { get; set; }
        public long? StoreReturnNoteNo { get; set; }
        public decimal? StoreReturnNoteQty { get; set; }
        public DateTime? InterUnitDate { get; set; }
        public long? InterUnitNo { get; set; }
        public decimal? InterUnitQty { get; set; }
        public DateTime? LoanPartyDate { get; set; }
        public long? LoanPartyNo { get; set; }
        public decimal? LoanPartyQty { get; set; }
        public DateTime? RecipieIssuanceDate { get; set; }
        public decimal? RecipieIssuanceQty { get; set; }
        public long? RecipieIssuanceNo { get; set; }
    }
}
