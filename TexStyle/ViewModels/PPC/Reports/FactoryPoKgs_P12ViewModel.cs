using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class FactoryPoKgs_P12ViewModel
    {

        public FactoryPoKgs_P12ViewModel()
        {
            FactoryPoKgsDetail_P12ViewModel = new List<FactoryPoKgsDetail_P12ViewModel>();
        }

        public string Buyer { get; set; }
        public string Color { get; set; }
        public long Po { get; set; }
  

        public List<FactoryPoKgsDetail_P12ViewModel> FactoryPoKgsDetail_P12ViewModel { get; set; }

    }
}
