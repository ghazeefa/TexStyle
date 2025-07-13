using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
  public  class FactoryPoKgRepository_P12ViewModel
    {
        public string FabricType { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public long Po { get; set; }
        public decimal Kgs { get; set; }
        public decimal TotalKgs { get; set; }
        public decimal NetKg { get; set; }
        public decimal OgpKgs { get; set; }
    }
}
