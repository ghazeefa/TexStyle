using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
 public  class UpdateDr
    {
        public long id { get; set; }
        public decimal QtyDr { get; set; }
        public long? GateHeaderId { get; set; }
        public decimal? FairPrice { get; set; }
        public long? IgpRefNo { get; set; }
        public long? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public long? Dtreno { get; set; }
        public long DetailId { get; set; }
        public long ide { get; set; }

    }
}
