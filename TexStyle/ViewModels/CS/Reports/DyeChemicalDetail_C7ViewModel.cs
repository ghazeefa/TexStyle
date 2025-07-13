using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class DyeChemicalDetail_C7ViewModel:DefaultViewModel
    {
        public DyeChemicalDetail_C7ViewModel()
        {
            DyeChemicalDetail1_C7ViewModels = new List<DyeChemicalDetail1_C7ViewModel>();
        }
        public string Name { get; set; }
        public decimal DrSum { get; set; }
        public decimal CrSum { get; set; }
        public decimal FAmountSum { get; set; }
        public decimal FQtySum { get; set; }

        public List<DyeChemicalDetail1_C7ViewModel> DyeChemicalDetail1_C7ViewModels { get; set; }
    }
}
