using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Common;

namespace TexStyle.Areas.Admin.Controllers {
    [Area(AreaConstants.ADMIN.Name)]
    public class ManageAccountClaimController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}