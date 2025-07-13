using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StockOutSummary1ViewModel
    {



        //public string Item { get; set; }
        public decimal Issuance { get; set; }
        public decimal Rejection { get; set; }
        public decimal InterUnitOut { get; set; }
        public decimal Diluation { get; set; }
        public decimal LoanGiven { get; set; }
        public decimal LoanReturn { get; set; }


        public decimal Sum { get; set; }











    }
}
