using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StoreRecipeSortReport_ViewModel: DefaultViewModel
    {

   
        public StoreRecipeSortReport_ViewModel()
        {
            this.StoreRecipeSortReportDetail_ViewModel = new List<StoreRecipeSortReportDetail_ViewModel>();
        }


        public DateTime Date { get; set; }
       
        public List<StoreRecipeSortReportDetail_ViewModel> StoreRecipeSortReportDetail_ViewModel { get; set; }


    }
}
