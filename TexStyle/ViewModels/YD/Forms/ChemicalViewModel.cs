using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD {
    public class ChemicalViewModel {
        [DisplayName("No")]
        public long? Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
