using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.CS
{
    public class LoanTakenReturnOutTrViewModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("IGP No")]
        public long? IgpOgpNo { get; set; }
        public GateTr GateTr { get; set; }
        [Required]
        [DisplayName("Loan Taken No")]
        public long? LoanTakenInTrId { get; set; }
        public long? LoanTakenInTrDetailId { get; set; }
        [DisplayName("Fare Price")]
        public decimal? FairPrice { get; set; }
        [Required]
        [DisplayName("Party")]
        public long PartyId { get; set; }
        public Party Party { get; set; }
        [DisplayName("Confirm?")]
        public Boolean IsConfirm { get; set; }
     
    }
}
