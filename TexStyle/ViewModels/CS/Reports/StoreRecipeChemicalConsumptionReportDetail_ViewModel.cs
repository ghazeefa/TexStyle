using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StoreRecipeChemicalConsumptionReportDetail_ViewModel
    {

        public string Name { get; set; }

        public Decimal Qty { get; set; }

        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal CKL6Amount { get; set; }
        public Decimal CKL6Rate { get; set; }
        public Decimal CKL6Qty { get; set; }


    }
}
