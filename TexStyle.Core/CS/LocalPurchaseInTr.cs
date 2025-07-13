using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;

namespace TexStyle.Core.CS
{
    public class LocalPurchaseInTr : DefaultEntity
    {
        public LocalPurchaseInTr()
        {
            localPurchaseInTrDetails = new List<LocalPurchaseInTrDetail>();
        }

        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TrDate { get; set; }
        public decimal? FairPrice { get; set; }
        [DisplayName("Invoice No")]
        public long? InvoiceNo { get; set; }
        [DisplayName("Invoice Date")]
        public DateTime? InvoiceDate { get; set; }
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
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTr { get; set; }
        [DisplayName("Party")]
        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }

        public ICollection<LocalPurchaseInTrDetail> localPurchaseInTrDetails { get; set; }
    }
}
