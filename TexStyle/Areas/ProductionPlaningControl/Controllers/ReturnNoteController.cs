using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class ReturnNoteController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/ReturnNote";

        private readonly IReportFilterService _reportFilterService;
        private readonly IReturnNoteService _returnNoteService;
        private readonly IReturnNoteDetailService _returnNoteDetailService;
        private readonly IBuyerService _buyerService;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;


        public ReturnNoteController(IMapper mapper,
            IReturnNoteService returnNoteService,
            IReturnNoteDetailService returnNoteDetailService,
            IReportFilterService reportFilterService,
            IBuyerService buyerService
            ) {
            _mapper = mapper;
            _tempData = new TempDataViewModel();

            _returnNoteService = returnNoteService;
            _returnNoteDetailService = returnNoteDetailService;
            _reportFilterService = reportFilterService;
            _buyerService = buyerService;
        }

        public async Task<IActionResult> Index() {
           // if (id == 0)
             //   id = IssueTypes.ReturnToWinding.Id;

            //return View(_returnNoteService.GetAll().Where(i => i.ReturnTypeId == id));
            return View();
        }

        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_PRODUCTION_RECEIVED_VIEW)]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startDate;
                options.ed = endDate;
            }
         
            ViewBag.filter = filter;
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            if(filter.IsYarn == true)
            {
                return View(await _returnNoteService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
            }
            else
            {
                return View(await _returnNoteService.GetBetweenDateRangeFabric(options.sd.Value, options.ed.Value));
            }
            
        }

        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_PRODUCTION_RECEIVED_CREATE_AND_UPDATE)]
        public async Task<IActionResult> AddOrUpdate(long? id, int typeId) {
            var vm = new ReturnNoteViewModel();

            if (id.HasValue) {
                vm = _mapper.Map<ReturnNoteViewModel>(await _returnNoteService.GetById(id.Value));
            }
            else
            {
                var item = (await _returnNoteService.GetAll()).Count();
                vm.Id = item == 0 ? 1 : ((await _returnNoteService.GetAll()).LastOrDefault().Id + 1);
            }

            vm.ReturnTypeId = typeId;
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var buyerTypeList = (await _buyerService.GetAll()).ToSelectList(); ;
            ViewBag.buyerTypeList = buyerTypeList;
            ViewBag.filter = filter;
            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, ReturnNoteViewModel vm) {
            //if (ModelState.IsValid)
            //{
            try {
                var filter =  (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
                vm.Id = null;
                var m = _mapper.Map<ReturnNote>(vm);
                if (!id.HasValue) {
                    //m.IGPDetailId = null;
                    // create

                    if(filter.IsYarn == true)
                    {
                        await _returnNoteService.Create(m);
                    }
                    else
                    {
                        await _returnNoteService.CreateFabric(m);
                    }
                    
                    _tempData.MSG = "Successfully Created";
                } else {
                    //update
                    m.Id = id.Value;
                    if(filter.IsYarn==true)
                    {
                        await _returnNoteService.Update(m);
                    }
                    else
                    {
                        await _returnNoteService.UpdateFabric(m);
                    }
                    _tempData.MSG = "Successfully Updated";
                }
                return RedirectToAction(nameof(ReturnNoteController.Details), "ReturnNote", new { id = m.Id });

            }
            catch (Exception ex) {
                _tempData.Error = ex.Message;
                throw ex;
            }
            //}
            return RedirectToAction(nameof(Index), new { id = vm.ReturnTypeId });
        }
     //   [Authorize(Policy = AccountClaimKeys.PRODUCTION_RECEIVED_VIEW_DETAIL)]
        public async Task<IActionResult> Details(long id) {
            var filter =  (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var m = await _returnNoteService.GetById(id);
            ViewBag.filter = filter;
            return View(m);
        }
    }
}