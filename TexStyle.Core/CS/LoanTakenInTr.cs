using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;

namespace TexStyle.Core.CS {
    public class LoanTakenInTr : DefaultEntity {
        public LoanTakenInTr() {
            LoanTakenInTrDetails = new List<LoanTakenInTrDetail>();
        }

        public long Id { get; set; }

        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("Fair Price")]
        public decimal? FairPrice { get; set; }
        //public long? InvoiceNo { get; set; }
        //public DateTime? InvoiceDate { get; set; }

        //public long IgpOgpNo { get; set; }
        //public long SupplierId { get; set; }
        //[ForeignKey(nameof(SupplierId))]
        //public virtual Supplier Supplier { get; set; }
        [DisplayName("Comfirm?")]
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
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTr { get; set; }
        [DisplayName("Party")]
        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }

        public ICollection<LoanTakenInTrDetail> LoanTakenInTrDetails { get; set; }
    }
}
