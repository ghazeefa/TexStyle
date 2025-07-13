using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
  public  class CKL1POViewModelRepository
    {
        public string BuyerName { get; set; }
        public string BrandName { get; set; }
        public string ArticleName { get; set; }
        public string StyleCode { get; set; }
        public string CustomerPO { get; set; }
        public string FactoryPO { get; set; }
        public int? DyeingUnitCode { get; set; }
        public string BranchName { get; set; }
        public string Fabric { get; set; }
        public string FabricDetail { get; set; }
        public string MainColor { get; set; }
        public string FabricColor { get; set; }
        public string GSM { get; set; }
        public int Dia { get; set; }
        public double POQty { get; set; }
        public double Consumption { get; set; }
        public double NetKgs { get; set; }
        public double ExPercentage { get; set; }
        public double ExKgs { get; set; }
        public double GrossKgs { get; set; }
        public double Mtrs { get; set; }
        public double Pcs { get; set; }
        public double AvailableKgs { get; set; }
        public int DyeingWOID  { get; set; }
        public int DyeingWODetailID { get; set; }

    }
}
