using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
  public  class EcruYarnReceiveRepository_P1ViewModel
    {
        public DateTime IgpDate { get; set; }
        public string Name { get; set; }
        public long IGPNo { get; set; }
        public decimal Bags { get; set; }
        public string YarnManufacturer { get; set; }
        public string Yarntype { get; set; }
        public string YarnQuality { get; set; }
        public decimal TearWeightInKg { get; set; }
        public decimal  NetWeightInKg { get; set; }
    }
}
