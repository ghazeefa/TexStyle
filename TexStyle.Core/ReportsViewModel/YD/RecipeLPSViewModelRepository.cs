using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.YD
{
    public class RecipeLPSViewModelRepository
    {

        public long LPSId { get; set; }
        public long Id { get; set; }
        public decimal No { get; set; }
        public decimal? Kgs { get; set; }
        public int? Pcs { get; set; }
               

        public int? LotNo { get; set; }

        public string FabricType { get; set; }
        public string Party { get; set; }
        public string YarnType { get; set; }



    }
}
