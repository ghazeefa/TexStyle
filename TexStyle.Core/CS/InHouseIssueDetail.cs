using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
    public class InHouseIssueDetail: DefaultEntity
    {
        public long Id { get; set; }
        public decimal? QtyCr { get; set; }
        public decimal? QtyDr { get; set; }
        public long? ChemicalId { get; set; }
        public long? DyeId { get; set; }
        public long? InHouseIssueId { get; set; }
        public bool? IsIssued { get; set; }


        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }
        [ForeignKey(nameof(InHouseIssueId))]
        public virtual InHouseIssue InHouseIssue { get; set; }
    }
}
