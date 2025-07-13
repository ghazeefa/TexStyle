using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.YD;
using TexStyle.ViewModels;
using TexStyle.ViewModels.YD;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

using Microsoft.AspNetCore.Routing;

using TexStyle.ApplicationServices.Interfaces.IPPC;

using TexStyle.Core.PPC;

using TexStyle.Extensions;

using System.Reflection;
using TexStyle.ViewModels.YD.Reports;
using Rotativa.AspNetCore;
namespace TexStyle.Areas.YarnDyeing.Controllers
{
    [Area(AreaConstants.YARN_DYEING.Name)]
    public class StickerController : Controller
    {
        private readonly TempDataViewModel _tempData;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
    
        public StickerController(IUnitOfWork uow, IMapper mapper)
        {
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View((await _uow.StickerService.GetAll()).ToList());
        }
    
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            //var LPSList = _uow.RecipeLPSService.GetAll().ToList();
            StickerViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<StickerViewModel>(await _uow.StickerService.GetById(id.Value));
            }

            //ViewBag.LPSList = LPSList;
            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, StickerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<Sticker>(vm);
                    if (id==null)
                    {
                        // create
                        await _uow.StickerService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        await _uow.StickerService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var o =await _uow.StickerService.GetById(id);

                if (o == null)
                    return new StatusCodeResult(StatusCodes.Status404NotFound);

                await _uow.StickerService.Delete(o);

                if (o.IsDeleted == true)
                    return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                throw ex;
            }

            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }


        public async Task<IActionResult> Sticker_D4Report(long id)
        {

            List<Sticker_D4ViewModel> res = new List<Sticker_D4ViewModel>();
            var d = (await _uow.StickerService.StickerService_D4(id)).ToList();


            

            Sticker_D4ViewModel r = new Sticker_D4ViewModel();
            if (d.Count() > 0)
            {
                r.LPSId = d.FirstOrDefault().LPSId;
                r.LotNo = d.FirstOrDefault().LotNo;
                r.Party = d.FirstOrDefault().Party;
                r.YarnQuality = d.FirstOrDefault().YarnQuality;
                r.Yarntype = d.FirstOrDefault().Yarntype;
                r.Date = d.FirstOrDefault().Date;
                r.Cons = d.FirstOrDefault().Cons;
                r.Color = d.FirstOrDefault().Color;

               
                res.Add(r);
            }

            //});
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
             " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;
        }

    }
}