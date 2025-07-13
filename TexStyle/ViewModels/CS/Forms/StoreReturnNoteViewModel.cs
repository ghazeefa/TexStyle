using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.CS
{
    public class StoreReturnNoteViewModel
    {
        public long? Id { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [Required]
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }
        [DisplayName("Invoice No")]
        public long? InvoiceNo { get; set; }
        [DisplayName("Invoice Date")]
        public DateTime? InvoiceDate { get; set; }
        [DisplayName("Confirm?")]
        public Boolean IsConfirm { get; set; }
        [DisplayName("Local Purchase")]
        public long? LocalPurchaseInTrId { get; set; }
        [Required]
        [DisplayName("Party")]
        public long? PartyId { get; set; }
        public Party Party { get; set; }

        [DisplayName("Local Purchase")]
        public LocalPurchaseInTr LocalPurchaseInTr { get; set; }

    }
}
