using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS
{
    public class ChemicalIssuanceRecipeTrViewModel
    {
        public long Id { get; set; }
        public long? RecipeId { get; set; }
        public DateTime IssuanceDate { get; set; }
        public bool? IsCompleteIssued { get; set; }
    }
}
