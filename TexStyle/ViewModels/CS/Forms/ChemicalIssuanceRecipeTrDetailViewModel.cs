using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS
{
    public class ChemicalIssuanceRecipeTrDetailViewModel
    {
        public long Id { get; set; }
        public long ChemicalIssuanceRecipeTrId { get; set; }
        public long? RecipeDetailId { get; set; }
        public long? ChemicalId { get; set; }
        public long? DyeId { get; set; }
        public decimal Weight { get; set; }
        public bool IsIssued { get; set; }

 
    }
}
