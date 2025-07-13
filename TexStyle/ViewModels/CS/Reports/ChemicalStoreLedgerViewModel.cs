using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class ChemicalStoreLedgerViewModel: DefaultViewModel
    {
        public ChemicalStoreLedgerViewModel()
        {
            ChemicalStoreLedgerDetail_ViewModels = new List<ChemicalStoreLedgerDetail_ViewModel>();
        }
        public string ItemName { get; set; }
        

        public List<ChemicalStoreLedgerDetail_ViewModel> ChemicalStoreLedgerDetail_ViewModels { get; set; }
    }
}
