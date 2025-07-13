using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels {
    public class TempViewDataViewModel {
        [ViewData]
        public static string AreaName { get; set; }
    }
}
