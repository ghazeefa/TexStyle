using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.YD
{
   public class RecipeFormatHeader: DefaultEntity
    {
        public RecipeFormatHeader()
        {
            RecipeFormatDetails = new List<RecipeFormatDetail>();
        }
        
        public long Id { get; set; }
        public bool IsYarn { get; set; }
        public string Name { get; set; }
        public decimal LiquorRate { get; set; }

        public long? ProcessTypeId { get; set; }
        [ForeignKey(nameof(ProcessTypeId))  ]
        public virtual ProcessType ProcessType { get; set; }
        public ICollection<RecipeFormatDetail> RecipeFormatDetails { get; set; }
    }
}
