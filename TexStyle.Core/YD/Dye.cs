using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.YD {
    public class Dye : DefaultEntity{
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public bool? Selected { get; set; }
    }
}
