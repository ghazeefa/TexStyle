using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
    public class ChemicalDilutionTrDetail : DefaultEntity
    {
        public long Id { get; set; }
        public decimal? QtyDr { get; set; }
        public decimal? QtyCr { get; set; }
        public decimal Rate { get; set; }
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

        public long? ChemicalId { get; set; }
        public long? DyeId { get; set; }
        public long? ChemicalDilutionTrId { get; set; }

        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }
        [ForeignKey(nameof(ChemicalDilutionTrId))]
        public virtual ChemicalDilutionTr ChemicalDilutionTr { get; set; }
    }
}
