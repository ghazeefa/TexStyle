using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD.Reports
{
    public class RecipeHeaderViewModel
    {
        public long Id { get; set; }
        public decimal No { get; set; }
        public DateTime Date { get; set; }
        public decimal LiquorRate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReprocessed { get; set; }
        public int? LotNo { get; set; }
        public decimal XRefRecipe { get; set; }
        public string MachineType { get; set; }
        public string Color { get; set; }
        public long? PartyId { get; set; }
        public int? UserId { get; set; }
        public string RecipeFormat { get; set; }
       public decimal Weight
        {

            get; set;

        }

        public int Cones
        {
            get; set;
        }

        public bool? IsYarn { get; set; }
        public bool? IsWithoutLPS { get; set; }

        public int? BranchId { get; set; }




    }
}
