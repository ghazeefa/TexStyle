using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.PPC;

namespace TexStyle.Core.YD
{
    public class RecipeLPS : DefaultEntity
    {
        public long Id { get; set; }
     

        public long RecipeId { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public virtual Recipe Recipe { get; set; }

        public long? LPSId { get; set; }
        [ForeignKey(nameof(LPSId))]
        public virtual PPCPlanning LPS { get; set; }

        public long? ReprocessId { get; set; }
        [ForeignKey(nameof(ReprocessId))]
        public virtual Reprocess Reprocess { get; set; }
    }
}
