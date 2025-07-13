
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StockOutSummaryDetailViewModel : DefaultViewModel
    {
        public StockOutSummaryDetailViewModel()
        {

        

            //StockOutSummaryViewModel StockOutSummaryViewModel = new List<StockOutSummaryViewModel>();

        }
 
        public decimal Sum { get; set; }






        public decimal DRate { get; set; }
        public decimal DAmount { get; set; }

        public decimal ICRate { get; set; }
        public decimal ICAmount { get; set; }
        public decimal RRate { get; set; }
        public decimal RAmount { get; set; }
        public decimal IUORate { get; set; }
        public decimal IUOAmount { get; set; }
        public decimal LGCRate { get; set; }
        public decimal LGCAmount { get; set; }
        public decimal GCRate { get; set; }
        public decimal GCAmount { get; set; }
        public decimal LRCRate { get; set; }
        public decimal LRCAmount { get; set; }

        public string Item { get; set; }

        public decimal Issuance { get; set; }
        public decimal IssuanceSum { get; set; }
        public decimal IssuanceRate { get; set; }
        public decimal IssuanceAmount { get; set; }

        public decimal Rejection { get; set; }
        public decimal RejectionSum { get; set; }
        public decimal RejectionRate { get; set; }
        public decimal RejectionAmount { get; set; }

        public decimal InterUnitOut { get; set; }
        public decimal InterUnitOutSum { get; set; }
        public decimal InterUnitOutRate { get; set; }
        public decimal InterUnitOutAmount { get; set; }

        public decimal Diluation { get; set; }
        public decimal DiluationSum { get; set; }
        public decimal DiluationRate { get; set; }
        public decimal DiluationAmount { get; set; }

        public decimal LoanGiven { get; set; }
        public decimal LoanGivenSum { get; set; }
        public decimal LoanGivenRate { get; set; }
        public decimal LoanGivenAmount { get; set; }

        public decimal LoanReturn { get; set; }
        public decimal LoanReturnSum { get; set; }
        public decimal LoanReturnRate { get; set; }
        public decimal LoanReturnAmount { get; set; }

        public decimal GeneralConsumption { get; set; }
        public decimal GeneralConsumptionSum { get; set; }
        public decimal GeneralConsumptionRate { get; set; }
        public decimal GeneralConsumptionAmount { get; set; }

        public decimal Purchase { get; set; }
        public decimal PurchaseSum { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal PurchaseAmount { get; set; }

        public decimal LoanTaken { get; set; }
        public decimal LoanTakenSum { get; set; }
        public decimal LoanTakenRate { get; set; }
        public decimal LoanTakenAmount { get; set; }

        public decimal LoanReturnIn { get; set; }
        public decimal LoanReturnInRate { get; set; }
        public decimal LoanReturnInAmount { get; set; }

        public decimal Opening { get; set; }
        public decimal OpeningSum { get; set; }
        public decimal OpeningRate { get; set; }
        public decimal OpeningAmount { get; set; }

        public decimal Closing { get; set; }
        public decimal ClosingSum { get; set; }
        public decimal ClosingRate { get; set; }
        public decimal ClosingAmount { get; set; }

        public decimal DilutionIn { get; set; }
        public decimal DilutionInSum { get; set; }
        public decimal DilutionInRate { get; set; }
        public decimal DiluationInAmount { get; set; }


        public decimal IssunaceCKL6 { get; set; }
        public decimal IssunaceCKL6Rate { get; set; }
        public decimal IssunaceCKL6Sum { get; set; }
        public decimal IssunaceCKL6Amount { get; set; }








    }

}






