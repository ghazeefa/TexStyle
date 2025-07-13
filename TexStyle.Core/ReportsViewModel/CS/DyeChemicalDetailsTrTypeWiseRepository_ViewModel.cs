using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
   public class DyeChemicalDetailsTrTypeWiseRepository_ViewModel
    {
        public DateTime? TransactionDate { get; set; }
        public string PartyName { get; set; }
        public long? IGPNO { get; set; }
        public long? TrType { get; set; }
        public decimal? QtyDr { get; set; }
        public decimal? QtyCr { get; set; }
        public int? Status { get; set; }
        public long? Item { get; set; }
        public string DyeChemicalName { get; set; }
    }
}
