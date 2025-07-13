using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels {
    public class FilterOptions {
        public long? Id { get; set; }
        public DateTime? sd { get; set; }
        public DateTime? ed { get; set; }
    }
}
