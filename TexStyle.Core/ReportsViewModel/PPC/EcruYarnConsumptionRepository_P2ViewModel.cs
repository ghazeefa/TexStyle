using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
   public class EcruYarnConsumptionRepository_P2ViewModel
    {
        public DateTime IgpDate { get; set; }
        public string Name { get; set; }
        public long IGPNo { get; set; }
        public decimal Bags { get; set; }
        public string YarnManufacturer { get; set; }
        public string Yarntype { get; set; }
        public string YarnQuality { get; set; }
        public decimal TearWeightInKg { get; set; }
        public decimal NetWeightInKg { get; set; }
        public decimal Kgs { get; set; }
        public int Cones { get; set; }
        public long LPSId { get; set; }
        public long IGPDetailId { get; set; }
    }
}
