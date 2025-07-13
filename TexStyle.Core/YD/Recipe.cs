using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TexStyle.Core;
using TexStyle.Core.PPC;
using TexStyle.Core.Premisis;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.YD {
    public class Recipe : DefaultEntity {
        public Recipe() {
            RecipeLPs = new List<RecipeLPS>();
            RecipeDetails = new List<RecipeDetail>();
        }

        public long Id { get; set; }
        public decimal No { get; set; }
        public DateTime Date { get; set; }
        public decimal LiquorRate { get; set; }

        [DisplayName("Confirmed?")]
        public bool IsConfirmed { get; set; }
        public bool IsReprocessed { get; set; }

        //public bool IsWithoutLPS { get; set; }


        // private bool _IsWithoutLPS = true;
        //public bool IsWithoutLPS
        //        { 
        //    get
        //    {
        //        return _IsWithoutLPS;
        //    }
        //    set
        //    {
        //                _IsWithoutLPS = value;
        //    }
        //}
        public int? LotNo { get; set; }
        public decimal XRefRecipe { get; set; }
        public long? MachineTypeId { get; set; }
        public string Color { get; set; }

        [ForeignKey(nameof(MachineTypeId))]
        public virtual MachineType MachineType { get; set; }

        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
         public long? BuyerColorId { get; set; }
        [ForeignKey(nameof(BuyerColorId))]
        public virtual BuyerColor BuyerColor { get; set; }
       public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual  Account User { get; set; }
 

        public long? RecipeFormatId { get; set; }
        [ForeignKey(nameof(RecipeFormatId))]
        public virtual RecipeFormatHeader RecipeFormat { get; set; }

        public ICollection<RecipeLPS> RecipeLPs { get; set; }
        public ICollection<RecipeDetail> RecipeDetails { get; set; }

        // [NotMapped]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //    public int Weight { get; set; } = if(Weight  if (IsReprocessed)
        //                return RecipeLPs.Where(x => !x.IsDeleted).Sum(x => x.Reprocess.Kgs);

        //            return RecipeLPs.Where(x => !x.IsDeleted).Sum(x => x.LPS.Kgs);
        //} 
        //            else
        //            { return Weight; }

        public decimal Weight {


            get; set;


            //get
            //    {
            //        if(Weight == 0) {   if (IsReprocessed)
            //            return RecipeLPs.Where(x => !x.IsDeleted).Sum(x => x.Reprocess.Kgs);

            //        return RecipeLPs.Where(x => !x.IsDeleted).Sum(x => x.LPS.Kgs);} 
            //        else
            //        { return Weight; }

            //    }


        }

        //[NotMapped]
        public int Cones
        {
            get; set;
            //get
            //{
            //    if (IsReprocessed)
            //        return RecipeLPs.Where(x => !x.IsDeleted).Sum(x => x.Reprocess.Cones);

            //    return RecipeLPs.Where(x => !x.IsDeleted).Sum(x => x.LPS.Cones);
            //}

        }

         public bool? IsYarn { get; set; }
         public bool? IsWithoutLPS { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

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
        public bool? IsTimeAdded { get; set; }
        public decimal? Pcs { get; set; }
        public bool IsGarmentPrinting { get; set; }
        public bool IsFabricPrinting { get; set; }
        public bool IsGarmentDyeing { get; set; }

    }
}
