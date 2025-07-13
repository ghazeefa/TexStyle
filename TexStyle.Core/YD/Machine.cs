using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.YD
{
    public class Machine : DefaultEntity
    {
        public long Id { get; set; }
        public int Chambers { get; set; }
        public int ReelSpeed { get; set; }

        public long MachineTypeId { get; set; }
        [ForeignKey(nameof(MachineTypeId))]
        public virtual MachineType MachineType { get; set; }
    }
}
