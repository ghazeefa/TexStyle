using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StoreRecipeChemicalConsumptionReport_ViewModel :  DefaultViewModel
    {
        public StoreRecipeChemicalConsumptionReport_ViewModel()
        {
            this.StoreRecipeChemicalConsumptionReportDetail_ViewModel = new List<StoreRecipeChemicalConsumptionReportDetail_ViewModel>();
        }


        public string Status { get; set; }
        public decimal KgsSum { get; set; }
        public decimal CKL6KgsSum { get; set; }
        public decimal AmountSum { get; set; }
        public decimal CKL6AmountSum { get; set; }

        public List<StoreRecipeChemicalConsumptionReportDetail_ViewModel> StoreRecipeChemicalConsumptionReportDetail_ViewModel { get; set; }
    }
    
    }

