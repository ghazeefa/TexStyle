using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;

namespace TexStyle.Core.CS
{
    public class StoreReturnNote : DefaultEntity
    {
        public StoreReturnNote()
        {
            StoreReturnNoteDetails = new List<StoreReturnNoteDetail>();
        }

        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Invoice No")]
        public long? InvoiceNo { get; set; }
        [DisplayName("Invoice Date")]
        public DateTime? InvoiceDate { get; set; }

        private Boolean _isConfirm = false;
        [DisplayName("Confirm?")]
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
        [DisplayName("Party")]
        public long PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
        [DisplayName("Local Purchase No")]
        public long? LocalPurchaseInTrId { get; set; }
        [ForeignKey(nameof(LocalPurchaseInTrId))]
        public virtual LocalPurchaseInTr LocalPurchaseInTr { get; set; }
        public ICollection<StoreReturnNoteDetail> StoreReturnNoteDetails { get; set; }

    }
}
