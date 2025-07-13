using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Extensions;
using TexStyle.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class TrLinkerMasterController : Controller
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TrLinkerMasterController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var filter = _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            try
            {
                if (filter.DateFrom.HasValue)
                {
                    _uow.TrLinkerMasterService.UpdateRate(filter.DateFrom.Value);
                    return PartialView();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);


            //var dyelist = _uow.DyeService.GetAll();
            //var chemicallist = _uow.ChemicalService.GetAll();
            //foreach( var d in dyelist)
            //{//get all data related to dyeng Id
            // //var linkerlist=  _uow.TrLinkerMasterService.GetQtybyItem(d.Id, null);
            //    decimal qty = 0;
            //    decimal amount = 0;
            //    linkerlist.ForEach(l=>
            //    {

            //        if (l.TrStatus == LinkerMasterTrStatus.Debit)//if debit then get debit transactions and update qty
            //        {
            //            if (l.LCImportInTrDetailId != null) //LC import
            //            {
            //                l.Qty = l.LCImportInTrDetail.QtyDr.Value;
            //                l.Amount = l.LCImportInTrDetail.QtyDr.Value * l.LCImportInTrDetail.Rate;
            //                qty += l.Qty;
            //                amount += l.Amount;
            //            }
            //            if (l.LocalPurchaseInTrDetailId != null) //local purchase
            //            {
            //                l.Qty = l.LocalPurchaseInTrDetail.QtyDr.Value;
            //                l.Amount = l.LocalPurchaseInTrDetail.QtyDr.Value * l.LocalPurchaseInTrDetail.Rate;
            //                qty += l.Qty;
            //                amount += l.Amount;
            //            }
            //            if (l.LoanTakenInTrDetailId != null) //
            //            {
            //                l.Qty = l.LoanTakenInTrDetail.QtyDr.Value;
            //                l.Amount = l.LoanTakenInTrDetail.QtyDr.Value * l.LoanTakenInTrDetail.Rate;
            //                qty += l.Qty;
            //                amount += l.Amount;
            //            }
            //            if (l.LoanPartyReturnInTrDetailId != null) //
            //            {
            //                l.Qty = l.LoanPartyReturnInTrDetail.QtyDr.Value;
            //                l.Amount = l.LoanPartyReturnInTrDetail.QtyDr.Value * l.LoanPartyReturnInTrDetail.Rate;
            //                qty += l.Qty;
            //                amount += l.Amount;
            //            }
            //            if (l.ChemicalDilutionTrDetailId != null) //
            //            {
            //                l.Qty = l.ChemicalDilutionTrDetail.QtyDr.Value;
            //                l.Amount = l.ChemicalDilutionTrDetail.QtyDr.Value * l.ChemicalDilutionTrDetail.Rate;
            //                qty += l.Qty;
            //                amount += l.Amount;
            //            }
            //        }
            //        else
            //        {//else credit then get credit transactions and update qty
            //            if (l.StoreReturnNoteDetailId != null) //
            //            {
            //                l.Qty = l.StoreReturnNoteDetail.QtyCr.Value;
            //                l.Amount = l.StoreReturnNoteDetail.QtyCr.Value * l.StoreReturnNoteDetail.Rate;
            //                qty -= l.Qty;
            //                amount -= l.Amount;
            //            }
            //            if (l.ChemicalIssuanceRecipeTrDetailId != null) //
            //            {
            //                l.Qty = l.ChemicalIssuanceRecipeTrDetail.Weight;
            //                l.Amount = l.ChemicalIssuanceRecipeTrDetail.Weight * l.ChemicalIssuanceRecipeTrDetail.Rate;
            //                qty -= l.Qty;
            //                amount -= l.Amount;
            //            }
            //            if (l.InterUnitOutTrDetailId != null) //
            //            {
            //                l.Qty = l.InterUnitOutTrDetail.QtyCr.Value;
            //                l.Amount = l.InterUnitOutTrDetail.QtyCr.Value * l.InterUnitOutTrDetail.Rate;
            //                qty -= l.Qty;
            //                amount -= l.Amount;
            //            }
            //            if (l.LoanTakenReturnOutTrDetailId != null) //
            //            {
            //                l.Qty = l.LoanTakenReturnOutTrDetail.QtyCr.Value;
            //                l.Amount = l.LoanTakenReturnOutTrDetail.QtyCr.Value * l.LoanTakenReturnOutTrDetail.Rate;
            //                qty -= l.Qty;
            //                amount -= l.Amount;
            //            }
            //            if (l.LoanPartyGivenOutTrDetailId != null) //
            //            {
            //                l.Qty = l.LoanPartyGivenOutTrDetail.QtyCr.Value;
            //                l.Amount = l.LoanPartyGivenOutTrDetail.QtyCr.Value * l.LoanPartyGivenOutTrDetail.Rate;
            //                qty -= l.Qty;
            //                amount -= l.Amount;
            //            }

            //        }
            //        _uow.TrLinkerMasterService.Update(l);
            //    }
            //        );
            //    foreach (var l in linkerlist)
            //    {

            //    }

            //}
            return View();
        }
    }
}
