using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.YD
{
    public class RecipeIndexViewModelRepository
    {

        public long? Id { get; set; }
        public long?  LPS { get; set; }
        public decimal? No { get; set; }
        public DateTime? Date { get; set; } 
        public string Party { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public long? RecipeFormatId { get; set; }
        public string MachineType { get; set; }
        public string BuyerColor { get; set; }
        public decimal? LiquorRate { get; set; }
        public decimal? Weight { get; set; }
        public bool IsWithoutLPS { get; set; }
        public bool IsReprocessed { get; set; }
        public bool IsYarn        { get; set; }
        public bool IsConfirmed        { get; set; }
        public int? LotNo { get; set; }
        public int? Cones { get; set; }
        public bool? IsTimeAdded { get; set; }
        public bool? IsGarmentPrinting { get; set; }
        public bool? IsGarmentDyeing { get; set; }
        public decimal? Pcs { get; set; }




    }
}
