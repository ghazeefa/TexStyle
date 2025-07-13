using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DetailStockOutReportC_3ViewModel:DefaultViewModel
    {
        public DetailStockOutReportC_3ViewModel()
        {
            InterUnitOutDetailListViewModels = new List<InterUnitOutDetailListViewModel>();
            LoanTakenOutDetailListViewModels = new List<LoanTakenOutDetailListViewModel>();
            ChemicalIssuanceDetailListViewModels = new List<ChemicalIssuanceDetailListViewModel>();
            LoanPartyGivenOutDetailListViewModels = new List<LoanPartyGivenOutDetailListViewModel>();
            StoreReturnNoteDetailListViewModels = new List<StoreReturnNoteDetailListViewModel>();
            ChemicalDilutionDetailListViewModel = new List<ChemicalDilutionDetailListViewModel>();
            GeneralConsumptionDetailListViewModel = new List<GeneralConsumptionDetailListViewModel>();
            ChemicalIssuanceCKL6DetailListViewModel = new List<ChemicalIssuanceCKL6DetailListViewModel>();

        }
        public string Item { get; set; }
        public int rowcount { get; set; }
        public int c1 { get; set; }
        public int c2 { get; set; }
        public int c3 { get; set; }
        public int c4 { get; set; }
        public int c5 { get; set; }
        public int c6 { get; set; }
        public int c7 { get; set; }
        public int c8 { get; set; }


        public List<InterUnitOutDetailListViewModel> InterUnitOutDetailListViewModels { get; set; }
        public List<LoanTakenOutDetailListViewModel> LoanTakenOutDetailListViewModels { get; set; }
        public List<ChemicalIssuanceDetailListViewModel> ChemicalIssuanceDetailListViewModels { get; set; }
        public List<LoanPartyGivenOutDetailListViewModel> LoanPartyGivenOutDetailListViewModels { get; set; }
        public List<StoreReturnNoteDetailListViewModel> StoreReturnNoteDetailListViewModels { get; set; }
        public List<ChemicalDilutionDetailListViewModel> ChemicalDilutionDetailListViewModel { get; set; }
        public List<GeneralConsumptionDetailListViewModel> GeneralConsumptionDetailListViewModel { get; set; }
        public List<ChemicalIssuanceCKL6DetailListViewModel> ChemicalIssuanceCKL6DetailListViewModel { get; set; }



    }
}
