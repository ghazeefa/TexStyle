using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;
using TexStyle.ViewModels.PPC.Forms;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class FactoryPoController : Controller
    {

        private IUnitOfWork _uow;
        private IFactoryPoService _FactoryPoService;

        [ViewData]

        public string AreaName { get; set; }
        private readonly TempDataViewModel _tempData;
        private readonly IReportFilterService _reportFilterService;
        private readonly IMapper _mapper;
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(FactoryPo)}";

        public FactoryPoController(
            IFactoryPoService factoryPoService,
            IUnitOfWork uow, IMapper mapper,
            IReportFilterService ReportFilterService)
        {
            AreaName = "PPC Planing";
            _uow = uow;
            _mapper = mapper;
            _reportFilterService = ReportFilterService;

            _FactoryPoService = factoryPoService;
        }


        public async Task<IActionResult> Index()
        {
            return View((await _uow.FactoryPoService.GetAll()).ToList());
        }
  
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
      
            var filter =(await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
         
            var buyerlist =(await _uow.BuyerService.GetAll()).ToSelectList();
            var buyercolorlist =(await _uow.BuyerColorService.GetAll()).ToSelectList();
            

            FactoryPoViewModel m = new FactoryPoViewModel();
            if (id != null)
            {
               // FactoryPo m = new FactoryPo();
                var vm =await _uow.FactoryPoService.GetById(id.Value);
                m.Id = vm.Id;
                m.BuyerId = vm.BuyerId;
                m.Description = vm.Description;
                m.GSM = vm.GSM;
                m.IsCancel = vm.IsCancel;
                m.Po = vm.Po;
                m.Date = vm.Date;
                m.BuyerColorId = vm.BuyerColorId;
                //edit
              //  vm = _mapper.Map<FactoryPoViewModel>();

                if (vm.BuyerId != 0) buyerlist.Find(x => Convert.ToInt64(x.Value) == vm.BuyerId).Selected = true;
            }


            ViewBag.buyerlist = buyerlist;
            ViewBag.buyercolorlist = buyercolorlist;

            ViewBag.filter = filter;
      

            return PartialView(m);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, FactoryPoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FactoryPo m = new FactoryPo();
                  //  m.Id = vm.Id.Value;
                    m.BuyerId = vm.BuyerId;
                    m.Description = vm.Description;
                    m.GSM = vm.GSM;
                    m.IsCancel = vm.IsCancel;
                    m.Po = vm.Po;
                    m.Date = vm.Date;
                    m.BuyerColorId = vm.BuyerColorId;
                    if (!id.HasValue)
                    {
                        // create
                        await _uow.FactoryPoService.Create(m);
                        //    _tempData.MSG = "Successfully Created";

                    }
                    else
                    {
                        //update
                        await _uow.FactoryPoService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex)
                {
              //      _tempData.Error = ex.Message;
                }
            }


            return RedirectToAction(nameof(FactoryPoController.Details), "FactoryPo", new { id = vm.Id });

        }

        public async Task<IActionResult> Details(long id)

        {

            var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();



            var m =await _uow.FactoryPoService.GetById(id);

            if (m == null) return RedirectToAction(nameof(Index));

            var buyerlist =(await _uow.BuyerService.GetAll()).ToSelectList();


            ViewBag.buyerlist = buyerlist;
            ViewBag.filter = filter;

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col)
        {
            try
            {
                if (id.HasValue)
                {
                    await _uow.FactoryPoService.Delete(await _uow.FactoryPoService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }

        [HttpGet]
        public async Task<IActionResult> FactoryPoDetailReport(long id)
        {
            if (id == 0) return BadRequest();
            var m =await _uow.FactoryPoService.GetById(id);
        
            return View(m);
        }

        public async Task<IActionResult> Select(long id)

        {
            var chemical =await _uow.FactoryPoService.GetById(id);
            chemical.IsChecked = true;

            await _uow.FactoryPoService.Update(chemical);

            return PartialView(chemical);
        }


        public async Task<IActionResult> UnSelect(long id)

        {
            var chemical =await _uow.FactoryPoService.GetById(id);
            chemical.IsChecked = false;

            await _uow.FactoryPoService.Update(chemical);

            return PartialView(chemical);
        }
    }
}