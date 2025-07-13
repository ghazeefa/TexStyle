using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.CS
{
  public  class InHouseIssue: DefaultEntity
    {
      
        public long Id { get; set; }
        public decimal FairPrice { get; set; }
        public Decimal RecipieNo { get; set; }
        public DateTime? IssueDate { get; set; }

    }
}
