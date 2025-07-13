using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
   public class ReturnedIdViewModel
    {
        public long headerId { get; set; }
        public DateTime? TransactionDate{ get; set; }
        public long Sno { get; set; }
        public long TrType { get; set; }
        public long? InvoiceNo{ get; set; }
        public DateTime? InvoiceDate{ get; set; }
        public decimal? FairPrice{ get; set; }
        public string DTRENo { get; set; }
        public bool IsCancel { get; set; }
        public long detailId{ get; set; }
        public decimal QtyDr  { get; set; }
        public decimal QtyCr  { get; set; }
        public decimal Rate { get; set; }
        public decimal? Packet { get; set; }
        public long? ChemicalId { get; set; }
        public long? DyeId { get; set; }
        public long  PartyId { get; set; }
        public bool IsConfirm { get; set; }
    }
}
