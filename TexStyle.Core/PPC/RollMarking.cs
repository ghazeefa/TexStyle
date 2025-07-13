using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Premisis;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC
{
   public class RollMarking : DefaultEntity
    {
        public long Id { get; set; } // lps id
        public decimal NoOfRolls { get; set; } // lps id
      
        public DateTime Date { get; set; }
  
         [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
        [ForeignKey(nameof(PPCPlanningId))]
        public virtual PPCPlanning PPCPlanning { get; set; }

        public ICollection<RollMarkingDetail> RollMarkingDetails { get; set; }



    }
}
