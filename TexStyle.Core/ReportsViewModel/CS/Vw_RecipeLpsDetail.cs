using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
    public class Vw_RecipeLpsDetail
    {
        public long? RecipeId { get; set; }
        public long? LPSId { get; set; }
        public string FactoryPo { get; set; }
        public long? FabricTypeId { get; set; }
        public long? YarnTypeId { get; set; }
        public string FabricType { get; set; }
        public string YarnType { get; set; }
        public decimal? Kgs { get; set; }
        public int? Pcs { get; set; }
        public bool IsYarn { get; set; }
        public decimal? RecipeNo { get; set; }
        public string KnittingParty { get; set; }
        public string YarnManufacturer { get; set; }

    }
}
