using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.YD
{
    public class MachineType : DefaultEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal? Capacity { get; set; }
    }
}
