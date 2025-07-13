using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.CS {
    public class LocalPurchaseInTrViewModel {
        public long? Id { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime? TrDate { get; set; }
        [Required]
        [DisplayName("Fair Price")]
        public decimal? FairPrice { get; set; }
        [DisplayName("Confirm?")]
        public Boolean IsConfirm { get; set; }
        [DisplayName("Invoice No")]
        public long? InvoiceNo { get; set; }
        [DisplayName("Invoice Date")]
        public DateTime? InvoiceDate { get; set; }
        [Required]
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }
        [Required]
        [DisplayName("Party")]
        public long? PartyId { get; set; }
        public Party Party { get; set; }

    }
}
