using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.GATE;

namespace TexStyle.Areas.Gate.Controllers {
    [Area(AreaConstants.GATE.Name)]
    public class OGPGateController : Controller {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public OGPGateController(IUnitOfWork uow, IMapper mapper) {
            _uow = uow;
            _mapper = mapper;
        }

    //    // GET: /<controller>/
    //    public IActionResult Index() {
    //        return View(_uow.GateOgpService.GetAll());
    //    }

    //    [HttpGet]
    //    public IActionResult Index([FromQuery] FilterOptions options) {
    //        var today = DateTime.Now;
    //        var startDate = new DateTime(today.Year, today.Month, 1);
    //        var endDate = startDate.AddMonths(1).AddDays(-1);
    //        if (!options.sd.HasValue || !options.ed.HasValue) {
    //            options.sd = startDate;
    //            options.ed = endDate;
    //        }
    //        ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
    //        return View(_uow.GateOgpService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
    //    }

    //    [HttpGet]
    //    public IActionResult AddOrUpdate(long? id) {
    //        var OgpTypeList = _uow.GateActivityTypeService.GetAll().ToSelectList();
    //        var YarnTypeList = _uow.YarnTypeService.GetAll().ToSelectList();

    //        var vm = new GateOgpViewModel();

    //        if (id.HasValue) {
    //            vm = _mapper.Map<GateOgpViewModel>(_uow.GateOgpService.GetById(id.Value));

    //            try {
    //                OgpTypeList.Find(x => Convert.ToInt64(x.Value) == vm.GateActivityTypeId).Selected = true;
    //                YarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
    //            }
    //            catch (Exception) {
    //            }
    //        }

    //        ViewBag.OgpTypeList = OgpTypeList;
    //        ViewBag.YarnTypeList = YarnTypeList;

    //        return PartialView(vm);
    //    }

    //    [HttpPost]
    //    public IActionResult AddOrUpdate([FromRoute]long? id, [FromForm]GateOgpViewModel vm) {
    //        try {
    //            var m = _mapper.Map<GateOgp>(vm);
    //            var ogp = new GateOgp();
    //            if (id.HasValue) {
    //                ogp = _uow.GateOgpService.Update(m);

    //                var o = _uow.GateOgpService.GetById(ogp.Id);

    //                o.GateOgpDetails.ToList().ForEach(de => {
    //                    _uow.GateOgpDetailService.Delete(de);
    //                });
    //            } else {
    //                ogp = _uow.GateOgpService.Create(m);
    //                vm.Id = ogp.Id;
    //            }

    //            dynamic d = 1;

    //            if (vm.GateActivityTypeId == 2)
    //                d = _uow.OGPService.GetById(m.Xref).OutwardGatePassDetails;
    //            else if (vm.GateActivityTypeId == 7)
    //                d = _uow.InterUnitOutTrService.GetById(m.Xref).InterUnitOutTrDetails;
    //            else if (vm.GateActivityTypeId == 8)
    //                d = _uow.LoanPartyGivenOutTrService.GetById(m.Xref).LoanPartyGivenOutTrDetails;
    //            else if (vm.GateActivityTypeId == 9)
    //                d = _uow.LoanTakenReturnOutTrService.GetById(m.Xref).LoanTakenReturnOutTrDetails;

    //            var details = new List<GateOgpDetail>();

    //            foreach (var i in d) {
    //                var newD = new GateOgpDetail();

    //                if (vm.GateActivityTypeId == 2) {
    //                    newD = new GateOgpDetail {
    //                        QtyCr = i.Kgs,
    //                        YarnTypeId = i.YarnTypeId,
    //                        GateOgpId = ogp.Id
    //                    };
    //                } else {
    //                    newD = new GateOgpDetail {
    //                        QtyCr = i.QtyCr,
    //                        QtyDr = i.QtyDr,
    //                        DyeId = i.DyeId,
    //                        ChemicalId = i.ChemicalId,
    //                        GateOgpId = ogp.Id
    //                    };
    //                }

    //                _uow.GateOgpDetailService.Create(newD);
    //            }
    //        }
    //        catch (Exception ex) {
    //            throw ex;
    //        }

    //        return RedirectToAction(nameof(Details), new { id = vm.Id.Value });
    //    }

    //    public IActionResult GetXrefSelect(long id) {
    //        var selectList = new List<SelectListItem>();

    //        if (id == 2)
    //            selectList = _uow.OGPService.GetAll().ToSelectList();
    //        else if (id == 7)
    //            selectList = _uow.InterUnitOutTrService.GetAll().ToSelectList();
    //        else if (id == 8)
    //            selectList = _uow.LoanPartyGivenOutTrService.GetAll().ToSelectList();
    //        else if (id == 9)
    //            selectList = _uow.LoanTakenReturnOutTrService.GetAll().ToSelectList();

    //        var model = new SelectListItemViewModel { Name = "Xref", SelectList = selectList, PlaceHolder = "Select an item" };

    //        return PartialView(@"\Views\Shared\_SelectList.cshtml", model);
    //    }

    //    public IActionResult GetParty(long id, long entity) {
    //        var p = new Party();

    //        if (entity == 2)
    //            p = _uow.OGPService.GetById(id).Party;
    //        else if (entity == 7)
    //            p = _uow.InterUnitOutTrService.GetById(id).Party;
    //        else if (entity == 8)
    //            p = _uow.LoanPartyGivenOutTrService.GetById(id).Party;
    //        else if (entity == 9)
    //            p = _uow.LoanTakenReturnOutTrService.GetById(id).Party;

    //        return Json(p);
    //    }

    //    public IActionResult Details(long id) {
    //        var m = _uow.GateOgpService.GetById(id);
    //        var partyList = _uow.PartyService.GetAll().ToSelectList();
    //        var YarnTypeList = _uow.YarnTypeService.GetAll().ToSelectList();
    //        var gateIGPTypeList = _uow.GateActivityTypeService.GetAll().ToSelectList();

    //        if (m != null) {
    //            if (m.PartyId.HasValue)
    //                partyList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;

    //            if (m.YarnTypeId.HasValue)
    //                YarnTypeList.Find(x => Convert.ToInt64(x.Value) == m.YarnTypeId).Selected = true;

    //            gateIGPTypeList.Find(x => Convert.ToInt64(x.Value) == m.GateActivityTypeId).Selected = true;
    //        }

    //        ViewBag.partyList = partyList;
    //        ViewBag.YarnTypeList = YarnTypeList;
    //        ViewBag.gateIGPTypeList = gateIGPTypeList;
    //        return View(m);
    //    }

    //    [HttpGet]
    //    public IActionResult AddOrUpdateDetail(long id) {
    //        var ogpd = _uow.GateOgpDetailService.GetById(id);
    //        return PartialView(new GateOgpDetailViewModel { Id = ogpd.Id, Description = ogpd.Description, parentId = ogpd.GateOgpId.Value });
    //    }

    //    [HttpPost]
    //    public IActionResult AddOrUpdateDetail(long id, GateOgpDetailViewModel vm) {
    //        var ogpd = _uow.GateOgpDetailService.GetById(id);
    //        ogpd.Description = vm.Description;
    //        _uow.GateOgpDetailService.Update(ogpd);

    //        return RedirectToAction(nameof(Details), new { id = vm.parentId });
    //    }
    }
}
