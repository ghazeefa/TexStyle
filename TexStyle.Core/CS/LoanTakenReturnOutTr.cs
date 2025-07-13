using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;

namespace TexStyle.Core.CS
{
    public class LoanTakenReturnOutTr : DefaultEntity
    {
        public LoanTakenReturnOutTr()
        {
            LoanTakenReturnOutTrDetails = new List<LoanTakenReturnOutTrDetail>();
        }

        public long Id { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("IGP No")]
        public long? IgpOgpNo { get; set; }
        [Required]
        [DisplayName("Fare Price")]
        public decimal? FairPrice { get; set; }
        [Required]
        [DisplayName("Loan Taken No")]
        public long? LoanTakenInTrId { get; set; }
        [ForeignKey(nameof(LoanTakenInTrId))]
        public virtual LoanTakenInTr LoanTakenInTr { get; set; }
        [DisplayName("Confirm?")]
        private Boolean _isConfirm = false;
        public Boolean IsConfirm
        {
            get
            {
                return _isConfirm;
            }
            set
            {
                _isConfirm = value;
            }
        }

        // TODO need to update views
        [Required]
        [DisplayName("Party")]
        public long PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
        public ICollection<LoanTakenReturnOutTrDetail> LoanTakenReturnOutTrDetails { get; set; }
    }
}
