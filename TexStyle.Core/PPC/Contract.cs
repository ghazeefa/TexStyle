using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
    public class Contract:DefaultEntity
    {
        public long Id { get; set; }
        public long? PartyId { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
    }
}
