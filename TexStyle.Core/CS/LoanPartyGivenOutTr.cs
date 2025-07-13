using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.PPC;

namespace TexStyle.Core.CS
{
    public class LoanPartyGivenOutTr: DefaultEntity
    {
        public LoanPartyGivenOutTr()
        {
            LoanPartyGivenOutTrDetails = new List<LoanPartyGivenOutTrDetail>();
        }


        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Fare Price")]
        public decimal? FairPrice { get; set; }
        // TODO need to update views
        [DisplayName("Party")]
        public long PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
        public ICollection<LoanPartyGivenOutTrDetail> LoanPartyGivenOutTrDetails { get; set; }
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
    }
}
