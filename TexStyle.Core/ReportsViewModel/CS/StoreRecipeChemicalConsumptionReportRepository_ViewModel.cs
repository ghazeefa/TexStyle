using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
  public  class StoreRecipeChemicalConsumptionReportRepository_ViewModel
    {
        public string Name { get; set; }

        public Decimal? Qty { get; set; }
        public Decimal? CKL6Qty { get; set; }
        public Decimal? Rate { get; set; }
        public Decimal? CKL6Rate { get; set; }
        public int Status { get; set; }
    }
}
