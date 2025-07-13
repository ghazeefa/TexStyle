using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS
{
    public class InterUnitOutTrViewModel
    {
        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        //[DisplayName("IGP No")]
        //public long IgpOgpNo { get; set; }
        [DisplayName("Confirm?")]
        public bool IsConfirm { get; set; }
        [DisplayName("Fair Price")]
        public decimal? FairPrice { get; set; }
        // TODO need to update views
        [DisplayName("Party")]
        public long PartyId { get; set; }
    }
}
