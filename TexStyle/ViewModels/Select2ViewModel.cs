using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels
{
    public class Select2ViewModel
    {
        public string PlaceHolder { get; set; }
        public List<SelectListItem> SelectList { get; set; }
    }
}
