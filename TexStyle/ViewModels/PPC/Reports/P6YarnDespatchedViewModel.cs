using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class P6YarnDespatchedViewModel:DefaultViewModel
    {
        public P6YarnDespatchedViewModel()
        {
            YarnDespatchedDetail = new List<P6YarnDespatchedDetailViewModel>();
        }
     
        public string Date { get; set; }
         public decimal Total { get; set; }
    public    List<P6YarnDespatchedDetailViewModel> YarnDespatchedDetail { get; set; }

    }
}
