using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Extensions;

namespace TexStyle.Areas.ChemicalStore.Controllers
{

        [Area("ChemicalStore")]
        public class UpdateRateController : Controller
        {
            private readonly IUnitOfWork _uow;

            public UpdateRateController(IUnitOfWork uow)
            {
                this._uow = uow;
            }

            public async Task<IActionResult> Index()
            {
                await _uow.DyeChemicalTrService.UpdateRateService((Convert.ToInt32(User.Identity.GetUserId())));
                return View();
            }
        }
    
}