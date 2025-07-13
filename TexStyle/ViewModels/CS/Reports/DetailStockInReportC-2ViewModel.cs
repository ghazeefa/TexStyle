using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DetailStockInReportC_2ViewModel: DefaultViewModel
    {
        public DetailStockInReportC_2ViewModel()
        {
            OpeningDetailListViewModel = new List<OpeningDetailListViewModel>();
            LCImportDetailListViewModel = new List<LCImportDetailListViewModel>();
            LocalPurchaseDetailListViewModel = new List<LocalPurchaseDetailListViewModel>();
            LoanTakenInDetailListViewModel = new List<LoanTakenInDetailListViewModel>();
            LoanPartyReturnInDetailListViewModel = new List<LoanPartyReturnInDetailListViewModel>();
            ChemicalDilutionDetailListViewModel = new List<ChemicalDilutionDetailListViewModel>();
        }
        public string Item { get; set; }
        public int rowcount { get; set; }
        public int c1 { get; set; }
        public int c2 { get; set; }
        public int c3 { get; set; }
        public int c4 { get; set; }  
        public int c5 { get; set; }
        public int c6 { get; set; }

        public List<OpeningDetailListViewModel> OpeningDetailListViewModel { get; set; }
        public List<LCImportDetailListViewModel> LCImportDetailListViewModel { get; set; }
        public List<LocalPurchaseDetailListViewModel > LocalPurchaseDetailListViewModel { get; set; }
        public List<LoanTakenInDetailListViewModel > LoanTakenInDetailListViewModel { get; set; }
        public List<LoanPartyReturnInDetailListViewModel> LoanPartyReturnInDetailListViewModel { get; set; }
        public List<ChemicalDilutionDetailListViewModel> ChemicalDilutionDetailListViewModel { get; set; }
        

    }
}
