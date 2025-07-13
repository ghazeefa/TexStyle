using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.Core.YD;

namespace TexStyle.ViewModels.YD
{
    public class RecipeViewModel
    {
        public long? Id { get; set; }
        public decimal No { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Liquor Rate")]
        public decimal LiquorRate { get; set; }
        [DisplayName("Reprocess?")]
        public bool IsReprocessed { get; set; }
        [DisplayName("Machine Type")]
        public long MachineTypeId { get; set; }
        [DisplayName("Recipe Format")]
        public long RecipeFormatId { get; set; }
        public decimal XRefRecipe { get; set; }
        public decimal Weight { get; set; }
        public int Cones { get; set; }
        public string Color { get; set; }
        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
        public long? BuyerColorId { get; set; }
        [ForeignKey(nameof(BuyerColorId))]
        public virtual BuyerColor BuyerColor { get; set; }
        [DisplayName("Confirmed?")]
        public bool IsConfirmed { get; set; }

        public int LotNo { get; set; }
        [DisplayName("YarnRecipe?")]
        public bool IsYarn { get; set; }
               
        [DisplayName("WithoutLPS?")]
        public bool IsWithoutLPS { get; set; }

        [DisplayName("FabricRecipe?")]
        public bool IsFabric { get; set; }
   

        [ForeignKey(nameof(RecipeFormatId))]
        public virtual RecipeFormatHeader RecipeFormat { get; set; }


        [ForeignKey(nameof(MachineTypeId))]
        public virtual MachineType MachineType { get; set; }

        public ICollection<RecipeLPS> RecipeLPs { get; set; }

        [DisplayName("Shift Name")]
        public string ShiftName { get; set; }
        [DisplayName("Shift Incharge Name")]
        public string ShiftInchargeName { get; set; }
        [DisplayName("Shift Incharge Code")]
        public long? ShiftInchargeCode { get; set; }
        [DisplayName("Machine Operator Name")]
        public string MachineOperatorName { get; set; }
        [DisplayName("Machine Operator Code")]
        public long? MachineOperatorCode { get; set; }
        [DisplayName("Helper Name")]
        public string HelperName { get; set; }
        [DisplayName("Helper Code")]
        public long? HelperCode { get; set; }
        [DisplayName("CStore Operator Name")]
        public string CStoreOperatorName { get; set; }
        [DisplayName("CStore Operator Code")]
        public long? CStoreOperatorCode { get; set; }

        [DisplayName("Machine Start Time")]
        public DateTime? MachineStartTime { get; set; }
        [DisplayName("Machine Unload Time")]
        public DateTime? MachineUnloadTime { get; set; }
        [DisplayName("Soaping Drain Time")]
        public DateTime? SoapingDrainTime { get; set; }
        public bool? IsTimeAdded { get; set; }
        [DisplayName("Printing Pcs")]
        public decimal Pcs { get; set; }
        [DisplayName("Garment Printing Recipe?")]
        public bool IsGarmentPrinting { get; set; }
        [DisplayName("Garment Dyeing Recipe?")]
        public bool IsGarmentDyeing { get; set; }
        [DisplayName("Fabric Printing Recipe?")]
        public bool IsFabricPrinting { get; set; }

    }
}
