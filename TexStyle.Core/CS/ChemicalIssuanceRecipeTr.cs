using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
   public class ChemicalIssuanceRecipeTr:DefaultEntity
    {
        public ChemicalIssuanceRecipeTr()
        {
            ChemicalIssuanceRecipeTrDetail =  new List<ChemicalIssuanceRecipeTrDetail>();
        }

        public long Id { get; set; }
        public long? RecipeId { get; set; }
        public DateTime IssuanceDate { get; set; }
        public bool IsCompleteIssued { get; set; }


        [ForeignKey(nameof(RecipeId))]
        public virtual Recipe Recipe { get; set; }
        public ICollection<ChemicalIssuanceRecipeTrDetail> ChemicalIssuanceRecipeTrDetail { get; set; }
    }
}
