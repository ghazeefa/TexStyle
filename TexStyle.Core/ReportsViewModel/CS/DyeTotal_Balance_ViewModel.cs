using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
    public class DyeTotal_Balance_ViewModel
    {
        //public long ChemicalId { get; set; }
        public long DyeId { get; set; }
        public string Name { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Balance { get; set; }
        //public DateTime TransactionDate { get; set; }
        //  public bool IdSelection { get; set; }
    }
}
