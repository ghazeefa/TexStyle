using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
    public class DyedYarnDespatchedRepository_P5ViewModel
    {
        public string Quality { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string YarnType { get; set; }
        public string Knitter { get; set; }
        public string FabricType { get; set; }
        public bool IsYarn { get; set; }
        public string Party { get; set; }
        public string Shade { get; set; }
        public string Buyer { get; set; }
      
        public DateTime OgpDate { get; set; }
        public long OGPNo { get; set; }
        public decimal Bags { get; set; }
        public decimal Kgs { get; set; }
        public long? LPSId { get; set; }
        public int LotNo { get; set; }
    }
}
