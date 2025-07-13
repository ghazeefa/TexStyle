using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;

namespace TexStyle.Core.CS {
    public class LoanPartyReturnInTr : DefaultEntity {
        public LoanPartyReturnInTr() {
            LoanPartyReturnInTrDetails = new List<LoanPartyReturnInTrDetail>();
        }

        public long Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal? FairPrice { get; set; }
        //public long? InvoiceNo { get; set; }
        //public DateTime? InvoiceDate { get; set; }

        //public long IgpOgpNo { get; set; }
        //public long SupplierId { get; set; }
        //[ForeignKey(nameof(SupplierId))]
        //public virtual Supplier Supplier { get; set; }

        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTr { get; set; }

        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }

        public ICollection<LoanPartyReturnInTrDetail> LoanPartyReturnInTrDetails { get; set; }
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
