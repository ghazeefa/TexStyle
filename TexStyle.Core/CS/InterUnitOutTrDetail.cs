using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
    public class InterUnitOutTrDetail:DefaultEntity
    {
        public long Id { get; set; }
        [DisplayName("Qty Dr")]
        public decimal? QtyDr { get; set; }
        [DisplayName("Qty Cr")]
        public decimal? QtyCr { get; set; }
        [DisplayName("Rate")]
        public decimal Rate { get; set; }
        [DisplayName("Packet")]
        public decimal? Packet { get; set; }
        [NotMapped]
        public decimal Amount
        {
            get
            {
                if (QtyDr.HasValue)
                {
                    return QtyDr.Value * Rate;
                }
                else if (QtyCr.HasValue)
                {
                    return QtyCr.Value * Rate;
                }

                return 0;
            }
        }
        [DisplayName("Chemical")]
        public long? ChemicalId { get; set; }
        [DisplayName("Dye")]
        public long? DyeId { get; set; }
        public long? InterUnitOutTrId { get; set; }

        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }
        [ForeignKey(nameof(InterUnitOutTrId))]
        public virtual InterUnitOutTr InterUnitOutTr { get; set; }
    }
}
