using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
    public class DyeChemicalDetailRepository_C7ViewModel
    {
        public string Name { get; set; }
        public long Sno { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TypeName { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? QtyDr { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? QtyCr { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? FinalQty { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? FinalAmount { get; set; }
    }
}

