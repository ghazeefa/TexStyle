using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels {
    public class SelectListItemViewModel {
        public string Name { get; set; }
        public string PlaceHolder { get; set; }
        public bool UseDefault { get; set; }
        public bool? IsReadonly { get; set; }
        public bool? Disabled { get; set; }
        public bool? Reqired { get; set; }
        public List<SelectListItem> SelectList { get; set; }
    }
}
