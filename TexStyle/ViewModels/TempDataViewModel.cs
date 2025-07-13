using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels
{
    public class TempDataViewModel
    {
        [TempData]
        public string MSG { get; set; }
        [TempData]
        public string Error { get; set; }
    }
}
