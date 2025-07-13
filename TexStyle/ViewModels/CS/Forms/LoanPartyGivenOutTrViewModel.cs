using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS
{
    public class LoanPartyGivenOutTrViewModel
    {
        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Party")]
        public long PartyId { get; set; }
        [DisplayName("Confirm?")]
        public Boolean IsConfirm { get; set; }
        [Required]
        [DisplayName("Fare Price")]
        public decimal? FairPrice { get; set; }
        [DisplayName("IGP Reff No")]
        public long? IGPReffNo { get; set; }

        [DisplayName("Cancel?")]
        public bool IsCancel { get; set; }
    }
}
