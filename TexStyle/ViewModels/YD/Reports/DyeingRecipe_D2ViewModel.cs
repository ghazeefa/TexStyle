using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.YD.Reports
{
    public class DyeingRecipe_D2ViewModel : DefaultViewModel
    {
        public DyeingRecipe_D2ViewModel()
        {
            DyeingRecipeDetail_D2ViewModels = new List<DyeingRecipeDetail_D2ViewModel>();
            Vw_RecipeLpsDetail = new List<Vw_RecipeLpsDetail>();
        }
        public string LPS { get; set; }
        public int LotNo { get; set; }
        public string MachineType { get; set; }
        public string Format { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public string RecipeName { get; set; }
        public decimal LiquorRate { get; set; }
        public DateTime Date { get; set; }
        public decimal No { get; set; }
        public decimal Total { get; set; }
        public decimal? Cost { get; set; }
        public decimal? CostPerKgDye { get; set; }
        public decimal? CostPerKgChemical { get; set; }        
        public decimal? Kgs { get; set; }
        public bool? IsReprocessed { get; set; }
        public string Party { get; set; }
        public bool IsYarn { get; set; }
        public string YarnType { get; set; }
        public string FabricType { get; set; }
        public bool IsWithoutLPS { get; set; }
        public string Buyer { get; set; }

        public string ShiftName { get; set; }
        public string ShiftInchargeName { get; set; }
        public long? ShiftInchargeCode { get; set; }
        public string MachineOperatorName { get; set; }
        public long? MachineOperatorCode { get; set; }
        public string HelperName { get; set; }
        public long? HelperCode { get; set; }
        public string CStoreOperatorName { get; set; }
        public long? CStoreOperatorCode { get; set; }

        public DateTime? MachineStartTime { get; set; }
        public DateTime? MachineUnloadTime { get; set; }
        public DateTime? SoapingDrainTime { get; set; }
        public bool? IsGarmentPrinting { get; set; }
        public bool? IsGarmentDyeing { get; set; }
        public bool? IsFabricPrinting { get; set; }
        public decimal Pcs { get; set; }

        public List<DyeingRecipeDetail_D2ViewModel> DyeingRecipeDetail_D2ViewModels { get; set; }
        public List<Vw_RecipeLpsDetail> Vw_RecipeLpsDetail { get; set; }       


    }
}
