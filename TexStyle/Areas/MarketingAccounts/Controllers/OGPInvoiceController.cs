using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.MarketingAccounts
{
    [Area(AreaConstants.MarketingAccounts.Name)]
    public class OGPInvoiceController : Controller
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        public OGPInvoiceController(IUnitOfWork uow, 
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.OGPService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) { 
            var vm = new OGPViewModel();

            if (id.HasValue)
            {
                vm = _mapper.Map<OGPViewModel>(await _uow.OGPService.GetById(id.Value));

            }



            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, [FromForm]OGPViewModel vm)
        {
            try
            {
                var m = _mapper.Map<OutwardGatePass>(vm);
                if (id.HasValue)
                {
                    await _uow.OGPService.Update(m);
                }
                else
                {
                    await _uow.OGPService.Create(m);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}