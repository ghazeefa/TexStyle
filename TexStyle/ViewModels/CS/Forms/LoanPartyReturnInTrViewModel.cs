using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.CS {
    public class LoanPartyReturnInTrViewModel {

        public long? Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal? FairPrice { get; set; }

        public long? GateTrId { get; set; }
        public long? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public Boolean IsConfirm { get; set; }
        public long? PartyId { get; set; }
        public Party Party { get; set; }

    }
}
