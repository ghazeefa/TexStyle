using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD
{
    public class RecipeLPSViewModel
    {
        [DisplayName("Recipe")]
        public long RecipeId { get; set; }
        [DisplayName("LPS")]
        public long? LPSId { get; set; }
        [DisplayName("Reprocess")]
        public long? ReprocessId { get; set; }
        // only used in Views
        public decimal Weight { get; set; }
        public int Cones { get; set; }
        public int NoOfRolls { get; set; }

        public long? PartyId { get; set; }
        public long? BuyerColorId { get; set; }
        public int? LotNo { get; set; }
    }
}
