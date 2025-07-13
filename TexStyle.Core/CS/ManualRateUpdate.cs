using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.CS
{
   public  class ManualRateUpdate
    {

        public DateTime TransactionDate { get; set; }
        public string Name { get; set; }
        public long? Sno { get; set; } 
        public long? Id { get; set; } 
        public long? TrType { get; set; } 
        public decimal? QtyDr { get; set; }
        public decimal? TakenRate { get; set; }
    }
}
