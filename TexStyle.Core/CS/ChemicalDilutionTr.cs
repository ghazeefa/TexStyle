using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TexStyle.Core.CS
{
   public class ChemicalDilutionTr:DefaultEntity
    {
        public ChemicalDilutionTr()
        {
            ChemicalDilutionTrDetails = new List<ChemicalDilutionTrDetail>();
        }
        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }

        public ICollection<ChemicalDilutionTrDetail> ChemicalDilutionTrDetails { get; set; }
    }
}
