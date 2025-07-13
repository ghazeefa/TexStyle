using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace TexStyle.Core.PPC
{
   public class RollMarkingDetail : DefaultEntity
    {
        public long Id { get; set; }
        public decimal RollNo { get; set; }
        public long EcruKgs { get; set; }
        public long DyedKgs { get; set; }


        public string Status { get; set; }

        public long? RollMarkingId { get; set; }
        [ForeignKey(nameof(RollMarkingId))]
        public virtual RollMarking RollMarking { get; set; }




    }
}
