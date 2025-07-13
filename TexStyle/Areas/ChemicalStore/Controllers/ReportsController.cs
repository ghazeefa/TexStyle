using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS.Reports;
using TexStyle.ViewModels.PPC;
using TexStyle.ViewModels.PPC.Reports;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]

    public class ReportsController : Controller
    {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.CHEMICAL_STORE.Name}/Views/{nameof(ReportsController).Replace("Controller", "")}";

        private readonly IOptions<CompanySettings> _companySettings;
        private readonly TempDataViewModel _tempData;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ReportsController(IUnitOfWork uow, IMapper mapper, IOptions<CompanySettings> companySettings)
        {
            AreaName = $"{AreaConstants.CHEMICAL_STORE.NormalizedName} / {nameof(ReportsController).Replace("Controller", "")}";
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
            _companySettings = companySettings;
        }
        public async Task<IActionResult> Index()
        {
            long? id = null;
            ReportFilterViewModel vm = new ReportFilterViewModel() { };
            if (User.Identity.IsAuthenticated)
            {
                vm.UserId = Convert.ToInt64(User.Identity.GetUserId());
            }
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            id = filter.Id;
            ViewBag.filter = filter;

            if (id.HasValue)
            {
                vm = _mapper.Map<ReportFilterViewModel>(await _uow.ReportFilterService.GetById(id.Value));
            }

            List<SelectListItem> yarnTypeList = (await _uow.YarnTypeService.GetAll()).ToSelectList();
            List<SelectListItem> yarnQualityList = (await _uow.YarnQualityService.GetAll()).ToSelectList();
            List<SelectListItem> yarnPartyList = (await _uow.PartyService.GetAll()).ToSelectList();

            List<SelectListItem> FabricTypeList = (await _uow.FabricTypesService.GetAll()).ToSelectList();
            List<SelectListItem> FabricQualityList = (await _uow.FabricQualityService.GetAll()).ToSelectList();
            List<SelectListItem> BuyerList = (await _uow.BuyerService.GetAll()).ToSelectList();

            List<SelectListItem> analysistypeList = (await _uow.AnalysisTypeService.GetAll()).ToSelectList();
            
            List<SelectListItem> buyerColorList = null;

            if (vm.BuyerId.HasValue)
            {
                buyerColorList = ((await _uow.BuyerColorService.GetAll())
                    .Where(x => x.BuyerId == vm.BuyerId)
                    .ToList()).ToSelectList();
            }
            else
            {
                buyerColorList = (await _uow.BuyerColorService.GetAll()).ToSelectList();
            }            

            List<SelectListItem> factoryPOList = (await _uow.FactoryPoService.GetFactoryPoList()).ToSelectList(nameof(FactoryPo.Po));
            //var purchaseOrderList = (await _uow.PurchaseOrderService.GetCommercialPo()).ToSelectList(nameof(PurchaseOrder.Po));

            //if (id.HasValue)
            //{
            //    vm = _mapper.Map<ReportFilterViewModel>(await _uow.ReportFilterService.GetById(id.Value));

                if (vm.YarnTypeId.HasValue) yarnTypeList.Find(x => Convert.ToInt32(x.Value) == vm.YarnTypeId).Selected = true;
                if (vm.YarnQualityId.HasValue) yarnQualityList.Find(x => Convert.ToInt32(x.Value) == vm.YarnQualityId).Selected = true;
                if (vm.YarnPartyId.HasValue) yarnPartyList.Find(x => Convert.ToInt32(x.Value) == vm.YarnPartyId).Selected = true;

                if (vm.FabricTypeId.HasValue) FabricTypeList.Find(x => Convert.ToInt32(x.Value) == vm.FabricTypeId).Selected = true;
                if (vm.FabricQualityId.HasValue) FabricQualityList.Find(x => Convert.ToInt32(x.Value) == vm.FabricQualityId).Selected = true;
                if (vm.BuyerId.HasValue) BuyerList.Find(x => Convert.ToInt32(x.Value) == vm.BuyerId).Selected = true;


                if (vm.AnalysisTypeId.HasValue) analysistypeList.Find(x => Convert.ToInt32(x.Value) == vm.AnalysisTypeId).Selected = true;
                if (vm.BuyerColorId.HasValue) buyerColorList.Find(x => Convert.ToInt32(x.Value) == vm.BuyerColorId).Selected = true;
                if (vm.FactoryPO.HasValue) factoryPOList.Find(x => Convert.ToInt32(x.Text) == vm.FactoryPO).Selected = true;

            //}


            ViewBag.FactoryPOList = factoryPOList;
            ViewBag.BuyerList = BuyerList;
            ViewBag.FabricQualityList = FabricQualityList;
            ViewBag.FabricTypeList = FabricTypeList;

            ViewBag.YarnTypes = yarnTypeList;
            ViewBag.YarnQualities = yarnQualityList;
            ViewBag.YarnParties = yarnPartyList;
            ViewBag.analysistypeList = analysistypeList;
            ViewBag.BuyerColorList = buyerColorList;

            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, ReportFilterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<ReportFilter>(vm);
                    if (!id.HasValue)
                    {
                        // create
                        await _uow.ReportFilterService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update


                        await _uow.ReportFilterService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult TotalStockReport(PagingOptions options)
        {
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf()
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> DetailStockInReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<DetailStockInReportC_2ViewModel>();
            var title = "Detailed Stock Movement Report";
            var dcd = (await _uow.DyeChemicalTrService.GetAllTypesBetweenDateRange(filter.DateFrom.Value, filter.DateTo.Value)).ToList();

            ViewBag.ReportTitle = title;
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            var dye = await _uow.DyeService.GetAll();
            var chemical = await _uow.ChemicalService.GetAll();
            //var detailList = new List<DetailStockInReportC_2ViewModel>();

            foreach (var d in dye.OrderBy(x => x.Name).Where(x => x.IsDeleted != true))
            {
                var detailItem = new DetailStockInReportC_2ViewModel();
                {
                    detailItem.Item = d.Name;
                    detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    detailItem.DateTo = filter.DateTo.Value.ToShortDateString();

                    detailItem.CompanyName = _companySettings.Value.CompanyName;
                    detailItem.CompanyBranch = filter.BranchName;

                    dcd.ToList().ForEach(x =>//Header
                {
                    if (x.TrType == ChemicalTransactions.OpeningBalance)
                    {

                        x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ToList().ForEach(ds =>
                          {
                              //var detailopening = new OpeningDetailListViewModel();
                              //detailopening.Date = x.TransactionDate;
                              //detailopening.No = x.Sno;
                              //detailopening.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                              detailItem.OpeningDetailListViewModel.Add(
                                   new OpeningDetailListViewModel()
                                   {
                                       Date = x.TransactionDate,
                                       No = x.Sno,
                                       Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value,
                                       Rate=ds.Rate,
                                       Amount=ds.QtyDr.Value* ds.Rate
                                   } );  });
                    }
                    if (x.TrType == ChemicalTransactions.LCImport)
                    {

                        x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ToList().ForEach(ds =>
                        {
                            var detail = new LCImportDetailListViewModel();
                            detail.Date = x.TransactionDate;
                            detail.No = x.Sno;
                            detail.Rate = ds.Rate;
                            detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                            detail.Amount = ds.QtyDr.Value * ds.Rate;
                            detailItem.LCImportDetailListViewModel.Add(detail);

                        });

                    }
                    if (x.TrType == ChemicalTransactions.LocalPurchase)
                    {

                        x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                        {
                            var detail = new LocalPurchaseDetailListViewModel();
                            detail.Date = x.TransactionDate;
                            detail.No = x.Sno;
                            detail.Rate = ds.Rate;
                            detail.Amount = ds.QtyDr.Value * ds.Rate;
                            detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                            detailItem.LocalPurchaseDetailListViewModel.Add(detail);
                        });


                    }
                    if (x.TrType == ChemicalTransactions.LoanTakenIn)
                    {

                        x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                        {
                            var detail = new LoanTakenInDetailListViewModel();
                            detail.Date = x.TransactionDate;
                            detail.No = x.Sno;
                            detail.Rate = ds.Rate;
                            detail.Amount = ds.QtyDr.Value * ds.Rate;
                            detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                            detailItem.LoanTakenInDetailListViewModel.Add(detail);
                        });


                    }
                    if (x.TrType == ChemicalTransactions.LoanPartyReturnIn)
                    {

                        x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                        {
                            var detail = new LoanPartyReturnInDetailListViewModel();
                            detail.Date = x.TransactionDate;
                            detail.No = x.Sno;
                            detail.Rate = ds.Rate;
                            detail.Amount = ds.QtyDr.Value * ds.Rate;
                            detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                            detailItem.LoanPartyReturnInDetailListViewModel.Add(detail);
                        });


                    }

                    if (x.TrType == ChemicalTransactions.Dilution)
                    {

                        x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                        {
                            var detail = new ChemicalDilutionDetailListViewModel();
                            detail.Date = x.TransactionDate;
                            detail.No = x.Sno;
                            detail.Qty = ds.QtyDr.Value;
                            detail.Rate = ds.Rate;
                            detail.Amount = ds.QtyDr.Value * ds.Rate;
                            detailItem.ChemicalDilutionDetailListViewModel.Add(detail);
                        });


                    }

                });
                    detailItem.c1 = detailItem.OpeningDetailListViewModel.Count();
                    detailItem.c2 = detailItem.LCImportDetailListViewModel.Count();
                    detailItem.c3 = detailItem.LocalPurchaseDetailListViewModel.Count();
                    detailItem.c4 = detailItem.LoanTakenInDetailListViewModel.Count();
                    detailItem.c5 = detailItem.LoanPartyReturnInDetailListViewModel.Count();
                    detailItem.c6 = detailItem.ChemicalDilutionDetailListViewModel.Count();

                    int c = 0;
                    if (c < detailItem.c1) c = detailItem.c1;
                    if (c < detailItem.c2) c = detailItem.c2;
                    if (c < detailItem.c3) c = detailItem.c3;
                    if (c < detailItem.c4) c = detailItem.c4;
                    if (c < detailItem.c5) c = detailItem.c5;
                    if (c < detailItem.c6) c = detailItem.c6;

                    detailItem.rowcount = c;
                    res.Add(detailItem);


                }
            }

            foreach (var d in chemical.OrderBy(x => x.Name).Where(x => x.IsDeleted != true))
            {
                var detailItem = new DetailStockInReportC_2ViewModel();
                {
                    detailItem.Item = d.Name;
                    detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    detailItem.DateTo = filter.DateTo.Value.ToShortDateString();
                    dcd.ToList().ForEach(x =>//Header
                    {
                        if (x.TrType == ChemicalTransactions.OpeningBalance)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                //var detailopening = new OpeningDetailListViewModel();
                                //detailopening.Date = x.TransactionDate;
                                //detailopening.No = x.Sno;
                                //detailopening.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.OpeningDetailListViewModel.Add(
                                     new OpeningDetailListViewModel()
                                     {
                                         Date = x.TransactionDate,
                                         No = x.Sno,
                                         Rate=ds.Rate,
                                         Amount=ds.Rate*ds.QtyDr.Value,
                                         Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value,
                                         
                                       
                                     }


                                    );
                            });


                        }
                        if (x.TrType == ChemicalTransactions.LCImport)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new LCImportDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.QtyDr.Value * ds.Rate;
                                detailItem.LCImportDetailListViewModel.Add(detail);

                            });


                        }
                        if (x.TrType == ChemicalTransactions.LocalPurchase)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new LocalPurchaseDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.QtyDr.Value * ds.Rate;
                                detailItem.LocalPurchaseDetailListViewModel.Add(detail);
                            });


                        }
                        if (x.TrType == ChemicalTransactions.LoanTakenIn)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new LoanTakenInDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.QtyDr.Value * ds.Rate;
                                detailItem.LoanTakenInDetailListViewModel.Add(detail);
                            });


                        }
                        if (x.TrType == ChemicalTransactions.LoanPartyReturnIn)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new LoanPartyReturnInDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.QtyDr.Value * ds.Rate;
                                detailItem.LoanPartyReturnInDetailListViewModel.Add(detail);
                            });


                        }

                        if (x.TrType == ChemicalTransactions.Dilution)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                if (ds.QtyDr!=null)
                                {
                                    var detail = new ChemicalDilutionDetailListViewModel();

                                    detail.Date = x.TransactionDate;
                                    detail.No = x.Sno;
                                    detail.Qty = ds.QtyDr.Value;
                                    detail.Rate = ds.Rate;

                                    detail.Amount = ds.QtyDr.Value * ds.Rate;
                                    detailItem.ChemicalDilutionDetailListViewModel.Add(detail);
                                }
                                
                             
                            });


                        }

                    });
                    detailItem.c1 = detailItem.OpeningDetailListViewModel.Count();
                    detailItem.c2 = detailItem.LCImportDetailListViewModel.Count();
                    detailItem.c3 = detailItem.LocalPurchaseDetailListViewModel.Count();
                    detailItem.c4 = detailItem.LoanTakenInDetailListViewModel.Count();
                    detailItem.c5 = detailItem.LoanPartyReturnInDetailListViewModel.Count();
                    detailItem.c6 = detailItem.ChemicalDilutionDetailListViewModel.Count();

                    int c = 0;
                    if (c < detailItem.c1) c = detailItem.c1;
                    if (c < detailItem.c2) c = detailItem.c2;
                    if (c < detailItem.c3) c = detailItem.c3;
                    if (c < detailItem.c4) c = detailItem.c4;
                    if (c < detailItem.c5) c = detailItem.c5;
                    if (c < detailItem.c6) c = detailItem.c6;

                    detailItem.rowcount = c;
                    res.Add(detailItem);


                }
            }


            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                CustomSwitches = footer
            };
        }
        [HttpGet]
        public async Task<IActionResult> DetailStockOutReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<DetailStockOutReportC_3ViewModel>();
            var title = "Detailed Stock In Report";
            var dcd = (await _uow.DyeChemicalTrService.GetAllTypesBetweenDateRange(filter.DateFrom.Value, filter.DateTo.Value)).ToList();

            ViewBag.ReportTitle = title;
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            var dye = await _uow.DyeService.GetAll();
            var chemical = await _uow.ChemicalService.GetAll();
            //var detailList = new List<DetailStockInReportC_2ViewModel>();

            foreach (var d in dye.OrderBy(x => x.Name).Where(x => x.IsDeleted != true))
            {
                var detailItem = new DetailStockOutReportC_3ViewModel();
                {
                    detailItem.Item = d.Name;
                    detailItem.CompanyName= _companySettings.Value.CompanyName;
                    detailItem.CompanyBranch = filter.BranchName;
                    detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    detailItem.DateTo = filter.DateTo.Value.ToShortDateString();
                    dcd.ToList().ForEach(x =>//Header
                    {
                        if (x.TrType == ChemicalTransactions.InterUnitOut)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                //var detailopening = new OpeningDetailListViewModel();
                                //detailopening.Date = x.TransactionDate;
                                //detailopening.No = x.Sno;
                                //detailopening.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.InterUnitOutDetailListViewModels.Add(
                                     new InterUnitOutDetailListViewModel()
                                     {
                                         Date = x.TransactionDate,
                                         No = x.Sno,
                                         Rate=ds.Rate,
                                         Amount=ds.Rate*ds.QtyCr.Value,

                                         Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value
                                     }


                                    );
                            });


                        }
                        if (x.TrType == ChemicalTransactions.LoanTakenReturnOut)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new LoanTakenOutDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.LoanTakenOutDetailListViewModels.Add(detail);

                            });


                        }
                        if (x.TrType == ChemicalTransactions.ChemicalIssuanceRecipe)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new ChemicalIssuanceDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.Sno = x.Recipe.No;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.ChemicalIssuanceDetailListViewModels.Add(detail);
                            });


                        }

                        if (x.TrType == ChemicalTransactions.CKL6RecipeIssuance)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new ChemicalIssuanceCKL6DetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.Sno = x.Recipe.No;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.ChemicalIssuanceCKL6DetailListViewModel.Add(detail);
                            });


                        }


                        if (x.TrType == ChemicalTransactions.LoanPartyGivenOut)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new LoanPartyGivenOutDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.LoanPartyGivenOutDetailListViewModels.Add(detail);
                            });


                        }
                        if (x.TrType == ChemicalTransactions.StoreReturnNote)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new StoreReturnNoteDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.StoreReturnNoteDetailListViewModels.Add(detail);
                            });


                        }
                        if (x.TrType == ChemicalTransactions.Dilution)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new ChemicalDilutionDetailListViewModel();
                                if (ds.QtyCr != null)
                                {
                                    detail.Date = x.TransactionDate;
                                    detail.No = x.Sno;
                                    detail.Rate = ds.Rate;
                                    detail.Amount = ds.Rate * ds.QtyCr.Value;
                                    detail.Qty = ds.QtyCr.Value;
                                    detailItem.ChemicalDilutionDetailListViewModel.Add(detail);
                                }

                            });


                        }


                        if (x.TrType == ChemicalTransactions.GeneralConsumption)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new GeneralConsumptionDetailListViewModel();
                                if (ds.QtyCr != null)
                                {
                                    detail.Date = x.TransactionDate;
                                    detail.No = x.Sno;
                                    detail.Rate = ds.Rate;
                                    detail.Amount = ds.Rate * ds.QtyCr.Value;
                                    detail.Qty = ds.QtyCr.Value;
                                    detailItem.GeneralConsumptionDetailListViewModel.Add(detail);
                                }

                            });


                        }


                    });
                    detailItem.c1 = detailItem.InterUnitOutDetailListViewModels.Count();
                    detailItem.c2 = detailItem.LoanTakenOutDetailListViewModels.Count();
                    detailItem.c3 = detailItem.ChemicalIssuanceDetailListViewModels.Count();
                    detailItem.c4 = detailItem.LoanPartyGivenOutDetailListViewModels.Count();
                    detailItem.c5 = detailItem.StoreReturnNoteDetailListViewModels.Count();
                    detailItem.c6 = detailItem.ChemicalDilutionDetailListViewModel.Count();
                    detailItem.c7 = detailItem.GeneralConsumptionDetailListViewModel.Count();



                    int c = 0;
                    if (c < detailItem.c1) c = detailItem.c1;
                    if (c < detailItem.c2) c = detailItem.c2;
                    if (c < detailItem.c3) c = detailItem.c3;
                    if (c < detailItem.c4) c = detailItem.c4;
                    if (c < detailItem.c5) c = detailItem.c5;
                    if (c < detailItem.c6) c = detailItem.c6;
                    if (c < detailItem.c7) c = detailItem.c7;

                    detailItem.rowcount = c;
                    res.Add(detailItem);


                }
            }



            foreach (var d in chemical.OrderBy(x => x.Name).Where(x => x.IsDeleted != true))
            {
                var detailItem = new DetailStockOutReportC_3ViewModel();
                {
                    detailItem.Item = d.Name;
                    detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    detailItem.DateTo = filter.DateTo.Value.ToShortDateString();
                    dcd.ToList().ForEach(x =>//Header
                    {
                        if (x.TrType == ChemicalTransactions.InterUnitOut)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                //var detailopening = new OpeningDetailListViewModel();
                                //detailopening.Date = x.TransactionDate;
                                //detailopening.No = x.Sno;
                                //detailopening.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.InterUnitOutDetailListViewModels.Add(
                                     new InterUnitOutDetailListViewModel()
                                     {
                                         Date = x.TransactionDate,
                                         No = x.Sno,
                                         Rate = ds.Rate,
                                         Amount = ds.Rate * ds.QtyCr.Value,
                                         Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value
                                     }


                                    );
                            });


                        }
                        if (x.TrType == ChemicalTransactions.LoanTakenReturnOut)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new LoanTakenOutDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.LoanTakenOutDetailListViewModels.Add(detail);

                            });


                        }
                        if (x.TrType == ChemicalTransactions.LoanPartyGivenOut)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new LoanPartyGivenOutDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.LoanPartyGivenOutDetailListViewModels.Add(detail);
                            });


                        }
                        if (x.TrType == ChemicalTransactions.ChemicalIssuanceRecipe)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new ChemicalIssuanceDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.ChemicalIssuanceDetailListViewModels.Add(detail);
                            });


                        }                   
                        if (x.TrType == ChemicalTransactions.CKL6RecipeIssuance)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new ChemicalIssuanceCKL6DetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.ChemicalIssuanceCKL6DetailListViewModel.Add(detail);
                            });


                        }
                        if (x.TrType == ChemicalTransactions.StoreReturnNote)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new StoreReturnNoteDetailListViewModel();
                                detail.Date = x.TransactionDate;
                                detail.No = x.Sno;
                                detail.Rate = ds.Rate;
                                detail.Amount = ds.Rate * ds.QtyCr.Value;
                                detail.Qty = ds.QtyDr != null ? ds.QtyDr.Value : ds.QtyCr.Value;
                                detailItem.StoreReturnNoteDetailListViewModels.Add(detail);
                            });


                        }
                        if (x.TrType == ChemicalTransactions.Dilution)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().ForEach(ds =>
                            {
                                var detail = new ChemicalDilutionDetailListViewModel();
                                if (ds.QtyCr != null)
                                {
                                    detail.Date = x.TransactionDate;
                                    detail.No = x.Sno;
                                    detail.Rate = ds.Rate;
                                    detail.Amount = ds.Rate * ds.QtyCr.Value;
                                    detail.Qty = ds.QtyCr.Value;
                                    detailItem.ChemicalDilutionDetailListViewModel.Add(detail);
                                }

                            });


                        }
                        if (x.TrType == ChemicalTransactions.GeneralConsumption)
                        {

                            x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().ForEach(ds =>
                            {
                                var detail = new GeneralConsumptionDetailListViewModel();
                                if (ds.QtyCr != null)
                                {
                                    detail.Date = x.TransactionDate;
                                    detail.No = x.Sno;
                                    detail.Rate = ds.Rate;
                                    detail.Amount = ds.Rate * ds.QtyCr.Value;
                                    detail.Qty = ds.QtyCr.Value;
                                    detailItem.GeneralConsumptionDetailListViewModel.Add(detail);
                                }

                            });


                        }

                    });
                    detailItem.c1 = detailItem.InterUnitOutDetailListViewModels.Count();
                    detailItem.c2 = detailItem.LoanTakenOutDetailListViewModels.Count();
                    detailItem.c3 = detailItem.ChemicalIssuanceDetailListViewModels.Count();
                    detailItem.c4 = detailItem.LoanPartyGivenOutDetailListViewModels.Count();
                    detailItem.c5 = detailItem.StoreReturnNoteDetailListViewModels.Count();
                    detailItem.c6 = detailItem.ChemicalDilutionDetailListViewModel.Count();
                    detailItem.c7 = detailItem.GeneralConsumptionDetailListViewModel.Count();
                    detailItem.c8 = detailItem.ChemicalIssuanceCKL6DetailListViewModel.Count();

                    int c = 0;
                    if (c < detailItem.c1) c = detailItem.c1;
                    if (c < detailItem.c2) c = detailItem.c2;
                    if (c < detailItem.c3) c = detailItem.c3;
                    if (c < detailItem.c4) c = detailItem.c4;
                    if (c < detailItem.c5) c = detailItem.c5;
                    if (c < detailItem.c6) c = detailItem.c6;
                    if (c < detailItem.c7) c = detailItem.c7;
                    if (c < detailItem.c8) c = detailItem.c8;


                    detailItem.rowcount = c;
                    res.Add(detailItem);


                }
            }


            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                CustomSwitches = footer
            }
                ;
        }




        [HttpGet]
        //public IActionResult StockOutSummary(PagingOptions options)
        //{
        //    var filter = _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();
        //    if (filter == null) return NotFound("Report filter doesn't have any record.");
        //    var res = new List<StockOutSummaryViewModel>();
        //    var title = "Detailed Stock In Report";
        //    var dcd = _uow.DyeChemicalTrService.GetAllTypesBetweenDateRange(filter.DateFrom.Value, filter.DateTo.Value).ToList();

        //    ViewBag.ReportTitle = title;
        //    ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
        //    var dye = _uow.DyeService.GetAll();
        //    var chemical = _uow.ChemicalService.GetAll();
        //    //var detailList = new List<DetailStockInReportC_2ViewModel>();

        //    foreach (var d in dye.OrderBy(x => x.Name).Where(x => x.IsDeleted != true))
        //    {
        //        var detailItem = new StockOutSummaryViewModel();
        //        {
        //            detailItem.Item = d.Name;
        //            detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
        //            detailItem.DateTo = filter.DateTo.Value.ToShortDateString();
        //            detailItem.CompanyName = "COMFORT KNITWEARS PVT LTD";
        //            dcd.ToList().ForEach(x =>//Header
        //            {
        //                if (x.TrType == ChemicalTransactions.InterUnitOut)
        //                {

        //                    detailItem.InterUnitOut = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ToList().Sum(z => z.QtyDr).Value;


        //                }
        //                if (x.TrType == ChemicalTransactions.LoanTakenReturnOut)
        //                {

        //                    detailItem.LoanTakenReturnOut = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ToList().Sum(z => z.QtyDr).Value;


        //                }
        //                if (x.TrType == ChemicalTransactions.ChemicalIssuanceRecipe)
        //                {

        //                    detailItem.ChemicalIssuanceRecipe = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().Sum(z => z.QtyDr).Value;

        //                }
        //                if (x.TrType == ChemicalTransactions.LoanPartyGivenOut)
        //                {

        //                    detailItem.LoanPartyGivenOut = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().Sum(z => z.QtyDr).Value;

        //                }
        //                if (x.TrType == ChemicalTransactions.StoreReturnNote)
        //                {

        //                    detailItem.StoreReturnNote = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().Sum(z => z.QtyDr).Value;

        //                }
        //                if (x.TrType == ChemicalTransactions.Dilution)
        //                {

        //                    detailItem.Dilution = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true).OrderBy(y => y.DyeId).ThenBy(y => y.DyeId).ToList().Sum(z => z.QtyDr).Value;

        //                }

        //                detailItem.Sum = (detailItem.InterUnitOut + detailItem.LoanTakenReturnOut + detailItem.LoanPartyGivenOut + detailItem.ChemicalIssuanceRecipe + detailItem.StoreReturnNote + detailItem.Dilution);

        //            });
        //            res.Add(detailItem);


        //        }
        //    }



        //    foreach (var d in chemical.OrderBy(x => x.Name).Where(x => x.IsDeleted != true))
        //    {
        //        var detailItem = new StockOutSummaryViewModel();
        //        {
        //            detailItem.Item = d.Name;
        //            detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
        //            detailItem.DateTo = filter.DateTo.Value.ToShortDateString();
        //            detailItem.CompanyName = "COMFORT KNITWEARS PVT LTD";
        //            dcd.ToList().ForEach(x =>//Header
        //            {
        //                if (x.TrType == ChemicalTransactions.InterUnitOut)
        //                {

        //                    detailItem.InterUnitOut = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().Sum(z => z.QtyDr).Value;


        //                }
        //                if (x.TrType == ChemicalTransactions.LoanTakenReturnOut)
        //                {

        //                    detailItem.LoanTakenReturnOut = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().Sum(z => z.QtyDr).Value;

        //                }
        //                if (x.TrType == ChemicalTransactions.LoanPartyGivenOut)
        //                {

        //                    detailItem.LoanPartyGivenOut = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().Sum(z => z.QtyDr).Value;


        //                }
        //                if (x.TrType == ChemicalTransactions.ChemicalIssuanceRecipe)
        //                {

        //                    detailItem.ChemicalIssuanceRecipe = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().Sum(z => z.QtyDr).Value;


        //                }
        //                if (x.TrType == ChemicalTransactions.StoreReturnNote)
        //                {

        //                    detailItem.StoreReturnNote = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().Sum(z => z.QtyDr).Value;


        //                }
        //                if (x.TrType == ChemicalTransactions.Dilution)
        //                {

        //                    detailItem.Dilution = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true).OrderBy(y => y.ChemicalId).ToList().Sum(z => z.QtyDr).Value;


        //                }

        //                detailItem.Sum = (detailItem.InterUnitOut + detailItem.LoanTakenReturnOut + detailItem.LoanPartyGivenOut + detailItem.ChemicalIssuanceRecipe + detailItem.StoreReturnNote + detailItem.Dilution);

        //            });

        //            res.Add(detailItem);


        //        }
        //    }


        //    string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
        //        " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
        //    ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");

        //    ViewBag.summaryobject = res;

        //    return new ViewAsPdf(res)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 4 },
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
        //        CustomSwitches = footer
        //    }
        //        ;
        //    return View();

        //}

        [HttpGet]
        public async Task<IActionResult> LoanTakenInReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<LoanTakenInOutC_4ViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.LoanTakenInOutReportServiceC4(Convert.ToInt32(User.Identity.GetUserId()))).ToList();

            var gb = d.GroupBy(x => x.Name).ToList();

            gb.ForEach(x =>
            {

                var r = new LoanTakenInOutC_4ViewModel();
                r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                r.DateTo = filter.DateTo.Value.ToShortDateString();
                r.CompanyName= _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;
                x.GroupBy(y => y.IGPNo).ToList().ForEach(b =>
                {
                    r.Item = b.FirstOrDefault().Name;
                    r.IGPNo = b.FirstOrDefault().IGPNo.Value;
                    r.Date = b.FirstOrDefault().IGPDate.Value;
                    r.Qty = b.FirstOrDefault().IGPQty.Value;

                });

                x.ToList().ForEach(b =>
                {
                    var detailItem = new OGP_IGPDetailListViewModel();
                    if (b.OGPQty != null)
                    {
                        detailItem.Date = b.OGPDate.Value;
                        detailItem.Qty = b.OGPQty.Value;
                    }

                    if (b.OGPNo != null)
                    {
                        detailItem.No = b.OGPNo.Value;
                    }


                    //  detailItem.Qty = x.FirstOrDefault().OGPQty.Value;

                    r.OGP_IGPDetailListViewModel.Add(detailItem);

                });

                res.Add(r);

            });




            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> DetailOpenStockReportC11()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            List<DetailOpenStockReportC11_ViewModel> res = new List<DetailOpenStockReportC11_ViewModel>();
            var rec = await _uow.DyeChemicalTrService.DetailOpenStockReportRepository_C11ViewModels(Convert.ToInt32(User.Identity.GetUserId()));

            var gb = rec.GroupBy(x => x.Status).ToList();
            gb.ToList().ForEach(x =>
            {
                
                var r = new DetailOpenStockReportC11_ViewModel();
                r.Status = x.Key.ToString();
                r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                r.DateTo = filter.DateTo.Value.ToShortDateString();
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;

                if (x.FirstOrDefault().Status == 0)
                {
                    r.Status = "Chemical";
                }
                else
                {
                    r.Status = "Dye";

                }


                x.ToList().ForEach(y =>
                {
                    var detailItem = new DetailOpenStockDetailReportC11_ViewModel();


                    detailItem.Name = y.Name;
                    detailItem.QtyDr = y.QtyDr;
                    detailItem.Rate = y.Rate;
                   
                    r.DetailOpenStockDetailReportC11_ViewModel.Add(detailItem);
                });
                res.Add(r);
            });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        [HttpGet]
        public async Task<IActionResult> LoanPartyGiveInOutReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<LoanPartyInOutC5ViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.LoanPartyInOutReportServiceC5(Convert.ToInt32(User.Identity.GetUserId()))).ToList();

            var gb = d.GroupBy(x => x.Name).ToList();
            gb.ForEach(x =>
            {
                var r = new LoanPartyInOutC5ViewModel();
                x.GroupBy(y => y.OGPNo).ToList().ForEach(b =>
                {
                    r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    r.DateTo = filter.DateTo.Value.ToShortDateString();
                    r.CompanyName = _companySettings.Value.CompanyName;
                    r.CompanyBranch = filter.BranchName;
                    r.Item = b.FirstOrDefault().Name;
                    r.Item = b.FirstOrDefault().Name;
                    if(b.FirstOrDefault().OGPNo!= null)
                    { r.OGPNo = b.FirstOrDefault().OGPNo;}
                    else
                    {
                        r.OGPNo = null;
                    }
                    
                    r.Date = b.FirstOrDefault().OGPDate.Value;
                    r.Qty = b.FirstOrDefault().OGPQty.Value;

                });

                x.ToList().ForEach(b =>
                {
                    var detailItem = new OGP_IGPDetailListViewModel();
                    if (b.IGPQty != null)
                    {
                        detailItem.Date = b.IGPDate.Value;
                        detailItem.Qty = b.IGPQty.Value;
                    }

                    if (b.IGPNo != null)
                    {
                        detailItem.No = b.IGPNo.Value;
                    }


                    //  detailItem.Qty = x.FirstOrDefault().OGPQty.Value;

                    r.OGP_IGPDetailListViewModel.Add(detailItem);

                });

                res.Add(r);

            });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }
        [HttpGet]
        public IActionResult LoanPartyOutReport()
        {
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf()
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        [HttpGet]
        public async Task<IActionResult> DetailedStockOutReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<StockDetailOut_C3ViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.StockDetailOutReportServiceC3()).ToList();

            var gb = d.GroupBy(x => x.Name).ToList();
            gb.ForEach(x =>
            {

                x.GroupBy(y => y.Name).ToList().ForEach(b =>
                {
                    var r = new StockDetailOut_C3ViewModel();
                    r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    r.DateTo = filter.DateTo.Value.ToShortDateString();
                    r.CompanyName = "COMFORT KNITWEARS PVT LTD(YARN DYEING DIV) ";
                    r.Name = b.FirstOrDefault().Name;
                    b.ToList().ForEach(y =>
                    {
                        var sd = new StockDetailOut1_C3ViewModel();
                        sd.InterUnitDate = y.InterUnitDate;
                        sd.InterUnitNo = y.InterUnitNo;
                        sd.InterUnitQty = y.InterUnitQty;
                        sd.LoanTakenOutDate = y.LoanTakenOutDate;
                        sd.LoanTakenOutNo = y.LoanTakenOutNo;
                        sd.LoanTakenOutQty = y.LoanTakenOutQty;
                        sd.StoreReturnNoteDate = y.StoreReturnNoteDate;
                        sd.StoreReturnNoteNo = y.StoreReturnNoteNo;
                        sd.StoreReturnNoteQty = y.StoreReturnNoteQty;
                        sd.LoanPartyDate = y.LoanPartyDate;
                        sd.LoanPartyNo = y.LoanPartyNo;
                        sd.LoanPartyQty = y.LoanPartyQty;
                        sd.RecipieIssuanceDate = y.RecipieIssuanceDate;
                        sd.RecipieIssuanceNo = y.RecipieIssuanceNo;
                        sd.RecipieIssuanceQty = y.RecipieIssuanceQty;
                        r.StockDetailOut1_C3ViewModels.Add(sd);
                    });
                    res.Add(r);
                });
            });
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }   
        public async Task<IActionResult> DetailedStockInReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<StockDetailIn_C2ViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.StockDetailInReportServiceC2()).ToList();

            var gb = d.GroupBy(x => x.Name).ToList();
            gb.ForEach(x =>
            {

                x.GroupBy(y => y.Name).ToList().ForEach(b =>
                {
                    var r = new StockDetailIn_C2ViewModel();
                    r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    r.DateTo = filter.DateTo.Value.ToShortDateString();
                    r.CompanyName = "COMFORT KNITWEARS PVT LTD(YARN DYEING DIV)";
                    r.Name = b.FirstOrDefault().Name;
                    b.ToList().ForEach(y =>
                    {
                        var sd = new StockDetailIn1_C2ViewModel();
                        //if (res.Any(person => person.StockDetailIn1_C2ViewModels. > 21))
                        //{
                        //    // ...
                        //}
                        sd.OpeningDate = y.OpeningDate;
                        sd.OpeningNo = y.OpeningNo;
                        sd.OpeningQty = y.OpeningQty;
                        sd.LCImportDate = y.LCImportDate;
                        sd.LCImportNo = y.LCImportNo;
                        sd.LCImportQty = y.LCImportQty;
                        sd.LocalPurchaseDate = y.LocalPurchaseDate;
                        sd.LocalPurchaseNo = y.LocalPurchaseNo;
                        sd.LocalPurchaseQty = y.LocalPurchaseQty;

                        sd.LoanTakenInDate = y.LoanTakenInDate;
                        sd.LoanTakenInQty = y.LoanTakenInQty;
                        sd.LoanTakenInQty = y.LoanTakenInNo;   
                    
                       sd.LoanPartyInDate = y.LoanPartyInDate;
                        sd.LoanPartyInQty = y.LoanPartyInQty;
                        sd.LoanPartyInNo = y.LoanPartyInNo;

                        sd.DilutionNo = y.DilutionNo;
                        sd.DilutionQty = y.DilutionQty;
                        sd.DilutionDate = y.DilutionDate;


                        sd.DilutionRate = y.DilutionRate;
                        sd.LoanTakenRate = y.LoanTakenRate;
                        sd.LoanReturnRate = y.LoanReturnRate;
                        sd.LCRate = y.LCRate;
                        sd.LocalPurchRate = y.LocalPurchRate;

                        r.StockDetailIn1_C2ViewModels.Add(sd);


    });
                    res.Add(r);
                });
            });
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        //public IActionResult UpdateRateReport()
        //{
        //    if (_uow.DyeChemicalTrService.UpdateRateService())
        //    {
        //        return View();
        //    }
        //    return View();
         
        //}

        public async Task<IActionResult> C1DyedStockSummaryReport()
        {
           var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
           List<DyeSummary_C1ViewModel> res = new List<DyeSummary_C1ViewModel>();
            var rec = await _uow.DyeChemicalTrService.DyeSummaryReportServiceC1(Convert.ToInt32(User.Identity.GetUserId()));

            var gb = rec.GroupBy(x => x.Status).ToList();
            gb.ToList().ForEach(x =>
            {
                var r = new DyeSummary_C1ViewModel();
                r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                r.DateTo = filter.DateTo.Value.ToShortDateString();
                r.Status= x.FirstOrDefault().Status==1 ? "Dye" :"Chemical";
                r.KgSum = (decimal)x.Sum(y => y.Balance);

                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;

                x.ToList().ForEach(y =>
                {
                    var detailItem = new DyeSummaryDetail_C1ViewModel
                    {
                        Name = y.Name,
                        Balance = y.Balance
                    };
                    r.SummaryDetail.Add(detailItem);
                    
                 });
                //r.KgSum += r.KgSum;
                res.Add(r);
              });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }
 
       public async Task<IActionResult> DyedChemicalRateDetailC7Report()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
          //  if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<DyeChemicalDetail_C7ViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.DyeChemicalRateDetailServiceC7(Convert.ToInt64(User.Identity.GetUserId()))).ToList();

            var gb = d.GroupBy(x => x.Name).ToList();
            gb.ForEach(x =>
            {

                x.GroupBy(y => y.Name).ToList().ForEach(b =>
                {
                    var r = new DyeChemicalDetail_C7ViewModel();
                    r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    r.DateTo = filter.DateTo.Value.ToShortDateString();
                    r.CompanyName = _companySettings.Value.CompanyName;
                    r.CompanyBranch = filter.BranchName;

                    r.Name = b.FirstOrDefault().Name;

                    r.DrSum= (decimal)x.Sum(y => (y.QtyDr));
                    r.CrSum= (decimal)x.Sum(y => (y.QtyCr));   
                    r.FAmountSum= (decimal)x.Sum(y => (y.FinalAmount));
                    r.FQtySum= (decimal)x.Sum(y => (y.FinalQty));

                    b.ToList().ForEach(y =>
                    {
                        var sd = new DyeChemicalDetail1_C7ViewModel();
                        sd.TypeName = y.TypeName;
                        sd.TransactionDate = y.TransactionDate;
                        sd.Sno = y.Sno;
                        if(y.QtyCr!= null)
                        {
                          sd.QtyCr = y.QtyCr.Value;
                        }
                        if (y.QtyDr != null)
                        {
                            sd.QtyDr = y.QtyDr.Value;
                        }
                        if (y.Rate != null)
                        {
                            sd.Rate = y.Rate.Value;
                        }
                        if (y.FinalAmount != null)
                        {
                            sd.FinalAmount = y.FinalAmount.Value;
                        }
                        if (y.FinalQty != null)
                        {
                            sd.FinalQty = y.FinalQty.Value;
                        }
                        r.DyeChemicalDetail1_C7ViewModels.Add(sd);
                    });
                   res.Add(r);
                });
            });
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
            //return View();

        }

        public async Task<IActionResult> DyedChemicalDebitRate()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            //  if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<DyeChemicalDetail_C7ViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.DyeChemicalRateDetailServiceC7(Convert.ToInt64(User.Identity.GetUserId()))).ToList();

            var gb = d.GroupBy(x => x.Name).ToList();
            gb.ForEach(x =>
            {

                x.GroupBy(y => y.Name).ToList().ForEach(b =>
                {
                    var r = new DyeChemicalDetail_C7ViewModel();
                    r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    r.DateTo = filter.DateTo.Value.ToShortDateString();
                    r.CompanyName = _companySettings.Value.CompanyName;
                    r.CompanyBranch = filter.BranchName;

                    r.Name = b.FirstOrDefault().Name;

                    b.ToList().ForEach(y =>
                    {
                        var sd = new DyeChemicalDetail1_C7ViewModel();
                        sd.TypeName = y.TypeName;
                        sd.TransactionDate = y.TransactionDate;
                        sd.Sno = y.Sno;
                        if (y.QtyCr != null)
                        {
                            sd.QtyCr = y.QtyCr.Value;
                        }
                        if (y.QtyDr != null)
                        {
                            sd.QtyDr = y.QtyDr.Value;
                        }
                        if (y.Rate != null)
                        {
                            sd.Rate = y.Rate.Value;
                        }
                        if (y.FinalAmount != null)
                        {
                            sd.FinalAmount = y.FinalAmount.Value;
                        }
                        if (y.FinalQty != null)
                        {
                            sd.FinalQty = y.FinalQty.Value;
                        }
                        r.DyeChemicalDetail1_C7ViewModels.Add(sd);
                    });
                    res.Add(r);
                });
            });
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
            //return View();

        }

        public async Task<IActionResult> ChemicalStoreLedger_Report()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            //  if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<ChemicalStoreLedgerViewModel>();
            var title = "Chemical Store Ledger Report";
            var d = (await _uow.DyeChemicalTrService.ChemicalStoreLedgerRepository_ViewModels(Convert.ToInt64(User.Identity.GetUserId()))).ToList(); 

            var gb = d.GroupBy(x => x.ItemName).ToList();
            gb.ForEach(x =>
            {

                x.GroupBy(y => y.TransactionDate).ToList().ForEach(b =>
                {
                    var r = new ChemicalStoreLedgerViewModel();
                    r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    r.DateTo = filter.DateTo.Value.ToShortDateString();
                    r.CompanyName = _companySettings.Value.CompanyName;
                    r.CompanyBranch = filter.BranchName;

                    r.ItemName = x.Key;

                    b.ToList().ForEach(y =>
                    {
                        var sd = new ChemicalStoreLedgerDetail_ViewModel();
                        sd.TrName = y.TrName;
                        sd.TransactionDate = y.TransactionDate;
                        sd.Sno = y.Sno;
                        sd.Status = y.Status;
                        
                        if (y.QtyCr != null)
                        {
                            sd.QtyCr = y.QtyCr.Value;
                        }
                        if (y.QtyDr != null)
                        {
                            sd.QtyDr = y.QtyDr.Value;
                        }
                        if (y.Rate != 0)
                        {
                            sd.Rate = y.Rate;
                        }
                        if (y.Amount != null)
                        {
                            sd.Amount = y.Amount.Value;
                        }
                        if(y.TrName != null)
                        {
                            sd.TrName = y.TrName;
                        }
                        if (y.FinalQty != null)
                        {
                            sd.FinalQty = y.FinalQty.Value;
                        }
                        //r.ChemicalStoreLedgerDetail_ViewModel.Add(sd);
                        r.ChemicalStoreLedgerDetail_ViewModels.Add(sd);
                    });
                    res.Add(r);
                });
            });
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
            //return View(res);

        }

        public async Task<IActionResult> StoreRecipeSortReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null)
                return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");



            List<StoreRecipeSortReport_ViewModel> res = new List<StoreRecipeSortReport_ViewModel>();
            
                     
            
            var d = (await _uow.DyeChemicalTrService.StoreRecipeSortReportReporistory_ViewModel(Convert.ToInt64(User.Identity.GetUserId()))).ToList();

            d.ToList().GroupBy(y => y.Date.Value.Date).ToList().ForEach(
              x =>
              {
                  StoreRecipeSortReport_ViewModel r = new StoreRecipeSortReport_ViewModel ();
                  r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                  r.DateTo = filter.DateTo.Value.ToShortDateString();
                  r.CompanyName = _companySettings.Value.CompanyName;
                  r.CompanyBranch = filter.BranchName;
                  r.Date = x.FirstOrDefault().Date.Value;
                  x.ToList().ForEach(b =>
                  {
                      StoreRecipeSortReportDetail_ViewModel e = new StoreRecipeSortReportDetail_ViewModel();
                      if (b.LPSId != null) { e.LPSId = b.LPSId.Value; }
                      if (b.No != null)
                      { e.No = b.No.Value; } 
                      if (b.IssuanceDate != null)
                      { e.IssuanceDate = b.IssuanceDate.Value; }
                      r.StoreRecipeSortReportDetail_ViewModel.Add(e);
                         //e.LPSId = b.LPSId.Value;
                         //e.No = e.No.;

                     });
                  res.Add(r);
              });
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
        
        public async Task<IActionResult> StoreRecipeChemicalConsumptionReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null)
                return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");



            List<StoreRecipeChemicalConsumptionReport_ViewModel> res = new List<StoreRecipeChemicalConsumptionReport_ViewModel>();

            //var count;  
            
            var d = (await _uow.DyeChemicalTrService.StoreRecipeChemicalConsumptionReportRepository_ViewModel(Convert.ToInt64(User.Identity.GetUserId()))).ToList();

            d.ToList().GroupBy(y => y.Status).ToList().ForEach(
              x =>
              {
                  //count = 1;
                  StoreRecipeChemicalConsumptionReport_ViewModel r = new StoreRecipeChemicalConsumptionReport_ViewModel();
                  r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                  r.DateTo = filter.DateTo.Value.ToShortDateString();
                  r.CompanyName = _companySettings.Value.CompanyName;
                  r.CompanyBranch = filter.BranchName;
                  if (x.FirstOrDefault().Status==0)
                  {
                      r.Status = "Chemical";
                  }
                  if (x.FirstOrDefault().Status==1)
                  {
                      r.Status = "Dyes";

                  }
                  
                  r.KgsSum = (decimal)d.Sum(y => y.Qty);
                  r.CKL6KgsSum = (decimal)d.Sum(y => y.CKL6Qty);

                  x.ToList().ForEach(b =>
                  {
                      StoreRecipeChemicalConsumptionReportDetail_ViewModel e = new StoreRecipeChemicalConsumptionReportDetail_ViewModel();
                 
                      e.Name = b.Name;


                      if (b.Qty != null)
                      {
                          e.Qty = b.Qty.Value;

                      }
                          if (b.Rate != null)
                      {

                          e.Rate = b.Rate.Value;
                          e.Amount = e.Qty * e.Rate;
                      }

                       if (b.CKL6Qty != null)
                      {
                          e.CKL6Qty = b.CKL6Qty.Value;

                      }
                          if (b.CKL6Rate != null && b.CKL6Rate !=0)
                      {

                       
                          e.CKL6Amount = e.CKL6Qty * e.CKL6Rate;
                      }
                      //else
                      //{

                      //    e.CKL6Amount = 0;
                      //}
                    
                   
                      r.StoreRecipeChemicalConsumptionReportDetail_ViewModel.Add(e);
                      //e.LPSId = b.LPSId.Value;
                      //e.No = e.No.
                      r.AmountSum += e.Amount;
                      r.CKL6AmountSum += e.Amount;

                     
                });
                  //count++;
                  res.Add(r);
              });
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

        public async Task<IActionResult> StockOutSummary(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<StockOutSummaryViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
            //var d1 = _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel1(Convert.ToInt64(User.Identity.GetUserId())).ToList();
            //var d2 = _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel2(Convert.ToInt64(User.Identity.GetUserId())).ToList();
            //var d3 = _uow.DyeChemicalTrService.DyeChemicalDetailsTrTypeWise(Convert.ToInt64(User.Identity.GetUserId())).ToList();

            var r = new StockOutSummaryViewModel();


            r.DateFrom = filter.DateFrom.Value.ToShortDateString();
            r.DateTo = filter.DateTo.Value.ToShortDateString();
            r.CompanyName = _companySettings.Value.CompanyName;
            r.CompanyBranch = filter.BranchName;


            r.IssuanceSum = (decimal)d.Sum(y => y.Issuance);
            r.InterUnitOutSum = (decimal)d.Sum(y => y.InterUnitOut);
            r.DiluationSum = (decimal)d.Sum(y => y.Diluation);
            r.LoanReturnSum = (decimal)d.Sum(y => y.LoanReturn);
            r.LoanGivenSum = (decimal)d.Sum(y => y.LoanGiven);
            r.GeneralConsumptionSum = (decimal)d.Sum(y => y.GeneralConsumption);
            r.RejectionSum = (decimal)d.Sum(y => y.Rejection);
            r.IssunaceCKL6Sum = (decimal)d.Sum(y => y.IssunaceCKL6);


            d.ToList().ForEach(y =>
                    {

                    var sd = new StockOutSummaryDetailViewModel();
                    sd.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    sd.DateTo = filter.DateTo.Value.ToShortDateString();
                        sd.Item = y.Item;
                    //sd.IssuanceSum = (decimal)y.Sum(m => m.Issuance);
                    //sd.InterUnitOut = (decimal)y.Sum(m => m.InterUnitOut);
                    //sd.DiluationSum = (decimal)y.Sum(m => m.Diluation);
                    //sd.LoanReturnSum = (decimal)y.Sum(m => m.LoanReturn);
                    //sd.LoanGivenSum = (decimal)y.Sum(m => m.LoanGiven);
                    //sd.GeneralConsumptionSum = (decimal)y.Sum(m => m.GeneralConsumption);
                    //sd.RejectionSum = (decimal)y.Sum(m => m.Rejection);
                    if (y.Diluation != null)
                    {
                        //sd.DRate = y.DRate.Value;
                        //sd.DAmount = y.DAmount.Value;
                        sd.Diluation = y.Diluation.Value;
                    } if (y.Issuance != null)
                    {
                        //sd.ICAmount = y.ICAmount.Value;
                        //sd.ICRate = y.ICRate.Value;
                        sd.Issuance = y.Issuance.Value;
                    } if (y.LoanReturn != null)
                        {
                            //sd.LRCRate = y.LRCRate.Value;
                            //sd.LRCAmount = y.LRCAmount.Value;
                            sd.LoanReturn = y.LoanReturn.Value;
                        }
                        if (y.IssunaceCKL6 != null)
                        {
                            //sd.LRCRate = y.LRCRate.Value;
                            //sd.LRCAmount = y.LRCAmount.Value;
                            sd.IssunaceCKL6 = y.IssunaceCKL6.Value;
                        }
                        if (y.Rejection != null)
                        {
                            //sd.RRate = y.RRate.Value;
                            //sd.RAmount = y.RAmount.Value;
                            sd.Rejection = y.Rejection.Value;
                        }
                        if (y.LoanGiven != null)
                        {
                            //sd.LGCRate = y.LGCRate.Value;
                            //sd.LGCAmount = y.LGCAmount.Value;
                            sd.LoanGiven = y.LoanGiven.Value;
                        }
                        if (y.InterUnitOut != null)
                        {
                            //sd.IUORate = y.IUORate.Value;
                            //sd.IUOAmount = y.IUOAmount.Value;
                            sd.InterUnitOut = y.InterUnitOut.Value;
                        }

                        if (y.GeneralConsumption != null)
                        {
                            //sd.GCRate = y.GCRate.Value;
                            //sd.GCAmount = y.GCAmount.Value;
                            sd.GeneralConsumption = y.GeneralConsumption.Value;
                        }

                        sd.Sum = sd.Rejection + sd.LoanReturn + sd.LoanGiven + sd.Issuance + sd.Diluation + sd.InterUnitOut + sd.GeneralConsumption +sd.IssunaceCKL6;

                        r.SummaryDetail.Add(sd);
                        r.Sum += sd.Sum;


                    });
        

            res.Add(r);
            StockSummary2ViewModel ress = new StockSummary2ViewModel()
            {
                StockOutSummaryViewModel = res,
                //StockOutSummaryViewModel1 = d1,
                //StockOutSummaryViewModel2 = d2,
                //DyeChemicalDetailsTrTypeWise = d3
            };


            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(ress)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> StockSummaryAccountReport_C13(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<StockOutSummaryViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
            //var d1 = _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel1(Convert.ToInt64(User.Identity.GetUserId())).ToList();
            //var d2 = _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel2(Convert.ToInt64(User.Identity.GetUserId())).ToList();
            //var d3 = _uow.DyeChemicalTrService.DyeChemicalDetailsTrTypeWise(Convert.ToInt64(User.Identity.GetUserId())).ToList();

            var r = new StockOutSummaryViewModel();


            r.DateFrom = filter.DateFrom.Value.ToShortDateString();
            r.DateTo = filter.DateTo.Value.ToShortDateString();
            r.CompanyName = _companySettings.Value.CompanyName;
            r.CompanyBranch = filter.BranchName;
            r.IssuanceSum = (decimal)d.Sum(y => y.Issuance);
            r.IssunaceCKL6Sum = (decimal)d.Sum(y => y.IssunaceCKL6);
          
            r.InterUnitOutSum = (decimal)d.Sum(y => y.InterUnitOut);
            r.DiluationSum = (decimal)d.Sum(y => y.Diluation);
            r.LoanReturnSum = (decimal)d.Sum(y => y.LoanReturn);
            r.LoanGivenSum = (decimal)d.Sum(y => y.LoanGiven);
            r.GeneralConsumptionSum = (decimal)d.Sum(y => y.GeneralConsumption);
            r.RejectionSum = (decimal)d.Sum(y => y.Rejection);
            r.OpeningSum = (decimal)d.Sum(y => y.Opening);
            r.PurchaseSum = (decimal)d.Sum(y => y.Purchase);
            r.LoanTakenSum = (decimal)d.Sum(y => y.LoanTaken);
            r.LoanReturnSum = (decimal)d.Sum(y => y.LoanReturn);
            r.DilutionInSum = (decimal)d.Sum(y => y.DilutionIn);
            r.LoanReturnInSum = (decimal)d.Sum(y => y.LoanReturnIn);




            d.ToList().ForEach(y =>
            {

                var sd = new StockOutSummaryDetailViewModel();
                sd.DateFrom = filter.DateFrom.Value.ToShortDateString();
                sd.DateTo = filter.DateTo.Value.ToShortDateString();
                sd.CompanyName = _companySettings.Value.CompanyName;
                sd.Item = y.Item;
                if (y.Diluation != null)
                {
          
                    sd.Diluation = y.Diluation.Value;
                    sd.DiluationRate = y.DiluationRate.Value;
                    sd.DiluationAmount = y.DiluationRate.Value * y.Diluation.Value;

                }
                if (y.Issuance != null)
                {
                  
                    sd.Issuance = y.Issuance.Value;
                    sd.IssuanceRate = y.IssuanceRate.Value;
                    sd.IssuanceAmount = y.IssuanceRate.Value * y.Issuance.Value;

                }

                if (y.IssunaceCKL6 != null)
                {

                    sd.IssunaceCKL6 = y.IssunaceCKL6.Value;
                    sd.IssunaceCKL6Rate = y.IssunaceCKL6Rate.Value;
                    sd.IssuanceAmount = y.IssunaceCKL6Rate.Value * y.IssunaceCKL6.Value;

                }

                if (y.LoanReturn != null)
                {
                    
                    sd.LoanReturn = y.LoanReturn.Value;
                    sd.LoanReturnRate = y.LoanReturnRate.Value;
                    sd.LoanReturnAmount = y.LoanReturnRate.Value * y.LoanReturn.Value;
                }
                if (y.Rejection != null)
                {
                   
                    sd.Rejection = y.Rejection.Value;
                    sd.RejectionRate = y.RejectionRate.Value;
                    sd.RejectionAmount = y.DiluationRate.Value * y.Diluation.Value;
                }
                if (y.LoanGiven != null)
                {
                  
                    sd.LoanGiven = y.LoanGiven.Value;
                    sd.LoanGivenRate = y.LoanGivenRate.Value;
                    sd.LoanGivenAmount = y.LoanGivenRate.Value * y.LoanGiven.Value;
                }
                if (y.InterUnitOut != null)
                {
                   
                    sd.InterUnitOut = y.InterUnitOut.Value;
                    sd.InterUnitOutRate = y.InterUnitOutRate.Value;
                    sd.InterUnitOutAmount = y.InterUnitOutRate.Value * y.InterUnitOut.Value;
                }

                if (y.GeneralConsumption != null)
                {
                
                    sd.GeneralConsumption = y.GeneralConsumption.Value;
                    sd.GeneralConsumptionRate = y.GeneralConsumptionRate.Value;
                    sd.GeneralConsumptionAmount = y.GeneralConsumptionRate.Value * y.GeneralConsumption.Value;
                }

                if (y.Purchase != null)
                {
                 
                    sd.Purchase = y.Purchase.Value;
                    //sd.PurchaseRate = y.PurchaseRate.Value;
                    sd.PurchaseAmount = y.PurchaseRate.Value;
                }
                
                if (y.LoanReturnIn != null)
                {
                  
                    sd.LoanReturnIn = y.LoanReturnIn.Value;
                    sd.LoanReturnInRate = y.LoanReturnInRate.Value;
                    sd.LoanReturnInAmount = y.LoanReturnInRate.Value * y.LoanReturnIn.Value;
                }
                if (y.LoanTaken != null)
                {
                   
                    sd.LoanTaken = y.LoanTaken.Value;
                    sd.LoanTakenRate = y.LoanTakenRate.Value;
                    sd.LoanTakenAmount = y.LoanTakenRate.Value * y.LoanTaken.Value;
                }
                if (y.Opening != null)
                {

                    sd.Opening = y.Opening.Value;
                    if (y.OpeningRate!=null)
                    {
                        sd.OpeningRate = y.OpeningRate.Value;
                        sd.OpeningAmount = y.OpeningRate.Value * y.Opening.Value;

                    }
                    else
                    {
                        sd.OpeningRate =0;
                        sd.OpeningAmount = sd.OpeningRate * y.Opening.Value;

                    }
                  
                   
                }
                if (y.DilutionIn != null)
                {

                    sd.DilutionIn = y.DilutionIn.Value;
                    sd.DiluationRate = y.DilutionInRate.Value;
                    sd.DiluationInAmount = y.DilutionIn.Value * y.DilutionInRate.Value;
                }
                if (y.Closing != null)
                {
                    if (y.Closing != 0)
                    {
                        sd.Closing = y.Closing.Value;
                        if (y.ClosingRate != null)
                        {
                            sd.ClosingRate = y.ClosingRate.Value;
                            sd.ClosingAmount = y.Closing.Value * y.ClosingRate.Value;
                        }
                        else
                        {
                            sd.ClosingRate = 0;
                            sd.ClosingAmount = 0;

                        }
                    }
                    else
                {


                        sd.Closing = 0;
                    sd.ClosingRate = 0;
                    sd.ClosingAmount = 0;
                }
                   
                }

                var credit = sd.LoanReturn + sd.LoanGiven + sd.Issuance + sd.Diluation + sd.InterUnitOut +sd.GeneralConsumption + sd.IssunaceCKL6;
                var debit = sd.Purchase + sd.LoanReturnIn + sd.LoanTaken+sd.Opening+sd.DilutionIn;

               //var creditammount= sd.LoanReturnAmount + sd.LoanGivenAmount + sd.IssuanceAmount + sd.DiluationAmount + sd.InterUnitOutAmount +sd.GeneralConsumptionAmount;

               // var debitammount= sd.PurchaseAmount + sd.LoanReturnInAmount + sd.LoanGivenAmount + sd.OpeningAmount;
                //var totalamount = debitammount - creditammount;

                //sd.Closing = debit - credit;
                //if (sd.Closing !=0)
                //{
                //    //sd.ClosingRate = totalamount / sd.Closing;
                //    //sd.ClosingAmount = sd.Closing * sd.ClosingRate;

                //}
                //else
                //{
                //    sd.ClosingRate = 0;
                //    sd.ClosingAmount = 0;

                //}


                r.SummaryDetail.Add(sd);
                r.PurchaseAmountSum += sd.PurchaseAmount;
                r.OpeningAmountSum += sd.OpeningAmount;
                r.IssuanceAmountSum += sd.IssuanceAmount;
                r.ClosingAmountSum += sd.IssunaceCKL6Amount;
                r.RejectionAmountSum += sd.RejectionAmount;
                r.GeneralConsumptionAmountSum += sd.GeneralConsumptionAmount;

                r.LoanGivenAmountSum += sd.LoanGivenAmount;
                r.LoanTakenAmountSum += sd.LoanTakenAmount;
                r.LoanReturnAmountSum += sd.LoanReturnAmount;
                r.LoanReturnInAmountSum += sd.LoanReturnInAmount;

                r.DiluationAmountSum += sd.DiluationAmount;
                r.DilutionInAmountSum += sd.DiluationInAmount;
                r.GeneralConsumptionAmountSum += sd.GeneralConsumptionAmount;


              
                r.InterUnitOutAmountSum += sd.InterUnitOutAmount;
                r.ClosingSum += sd.Closing;
                r.ClosingAmountSum += sd.ClosingAmount;
                res.Add(r);
            });

     
            res.Add(r);

            StockSummary2ViewModel ress = new StockSummary2ViewModel()
            {
                StockOutSummaryViewModel = res,
                //StockOutSummaryViewModel1 = d1,
                //StockOutSummaryViewModel2 = d2,
                //DyeChemicalDetailsTrTypeWise = d3
            };


            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(ress)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> AccountStockReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<StockOutSummaryViewModel>();
            var d = (await _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
            
            

            d.ToList().ForEach(y =>
            {
                var r = new StockOutSummaryViewModel();

                r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                r.DateTo = filter.DateTo.Value.ToShortDateString();
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;

                r.Item = y.Item;
                r.Closing = y.Closing ?? 0;
                r.ClosingRate = y.ClosingRate ?? 0;

                if (y.Closing.HasValue && y.ClosingRate.HasValue)
                {
                    var closing = y.Closing;
                    var closingRate = y.ClosingRate;
                    r.ClosingAmount = (decimal)(closing * closingRate);
                }

                res.Add(r);
            });


            //res.Add(r);



            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
            " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> PurchaseSummaryReport_C15(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<StockOutSummaryViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
            //var d1 = _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel1(Convert.ToInt64(User.Identity.GetUserId())).ToList();
            //var d2 = _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel2(Convert.ToInt64(User.Identity.GetUserId())).ToList();
            //var d3 = _uow.DyeChemicalTrService.DyeChemicalDetailsTrTypeWise(Convert.ToInt64(User.Identity.GetUserId())).ToList();

            var r = new StockOutSummaryViewModel();


            r.DateFrom = filter.DateFrom.Value.ToShortDateString();
            r.DateTo = filter.DateTo.Value.ToShortDateString();
            r.CompanyName = _companySettings.Value.CompanyName;


            r.PurchaseSum = (decimal)d.Sum(y => y.Purchase);
      




            d.ToList().ForEach(y =>
            {

                var sd = new StockOutSummaryDetailViewModel();
                sd.DateFrom = filter.DateFrom.Value.ToShortDateString();
                sd.DateTo = filter.DateTo.Value.ToShortDateString();
                sd.CompanyName = _companySettings.Value.CompanyName;
                sd.Item = y.Item;
              
             
              
           

                

                if (y.Purchase != null)
                {

                    sd.Purchase = y.Purchase.Value;
                    sd.PurchaseRate = y.PurchaseRate.Value;
                    sd.PurchaseAmount = y.PurchaseRate.Value * y.Purchase.Value;
                }

               
           

                var credit = sd.LoanReturn + sd.LoanGiven + sd.Issuance + sd.Diluation + sd.InterUnitOut;
                var debit = sd.Purchase + sd.LoanReturnIn + sd.LoanTaken + sd.Opening + sd.DilutionIn;

                var creditammount = sd.LoanReturnAmount + sd.LoanGivenAmount + sd.IssuanceAmount + sd.DiluationAmount + sd.InterUnitOutAmount;

                var debitammount = sd.PurchaseAmount + sd.LoanReturnInAmount + sd.LoanGivenAmount + sd.OpeningAmount;
                var totalamount = debitammount - creditammount;

                sd.Closing = debit - credit;
                if (sd.Closing != 0)
                {
                    sd.ClosingRate = totalamount / sd.Closing;
                    sd.ClosingAmount = sd.Closing * sd.ClosingRate;

                }
                else
                {
                    sd.ClosingRate = 0;
            sd.ClosingAmount = 0;


                }        


                r.SummaryDetail.Add(sd);
                r.PurchaseAmountSum += sd.PurchaseAmount;
               
                res.Add(r);
            });


            res.Add(r);

            StockSummary2ViewModel ress = new StockSummary2ViewModel()
            {
                StockOutSummaryViewModel = res,
                //StockOutSummaryViewModel1 = d1,
                //StockOutSummaryViewModel2 = d2,
                //DyeChemicalDetailsTrTypeWise = d3
            };


            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(ress)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                CustomSwitches = footer
            };

        }




        public async Task<IActionResult> LeatestRateDyesandChemicals_C14(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<LatestRateReportC14_ViewModel>();
            var title = "Detailed Stock In Report";
            var d = (await _uow.DyeChemicalTrService.LeatestRateDyesandChemicals_C14(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
 


                LatestRateReportC14_ViewModel r = new LatestRateReportC14_ViewModel();
                r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                r.DateTo = filter.DateTo.Value.ToShortDateString();
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyName = filter.BranchName;
            r.Date = DateTime.Now.ToString("dd-MMM-yyyy");
                d.ToList().ForEach(b =>
                {
                    LatestRateReportDetailC14_ViewModel sd = new LatestRateReportDetailC14_ViewModel();

                     sd.Item = b.Item;
                if (b.Rate != null)
                    {
                        sd.Rate = b.Rate.Value;
                        if (b.TransactionDate != null)
                        {
                            sd.TransactionDate = b.TransactionDate.Value;
                        }
                    }
                    r.LatestRateReportDetailC14_ViewModel.Add(sd);

                  });
                res.Add(r);
           
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };
        }




        //    //var filter = _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();
        //    //if (filter == null) return NotFound("Report filter doesn't have any record.");
        //    //var res = new List<StockOutSummaryViewModel>();
        //    //var title = "Detailed Stock In Report";
        //    //var dcd = _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel().ToList();

        //    //ViewBag.ReportTitle = title;
        //    //ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
        //    //var dye = _uow.DyeService.GetAll();
        //    //var chemical = _uow.ChemicalService.GetAll();
        //    ////var detailList = new List<DetailStockInReportC_2ViewModel>();

        //    //foreach (var d in dye.OrderBy(x => x.item).Where(x => x.IsDeleted != true))
        //    //{
        //    //    var detailItem = new StockOutSummaryViewModel();
        //    //    {
        //    //        detailItem.Item = d.Name;
        //    //        detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
        //    //        detailItem.DateTo = filter.DateTo.Value.ToShortDateString();
        //    //        detailItem.CompanyName = "COMFORT KNITWEARS PVT LTD";
        //    //        dcd.ToList().ForEach(x =>//Header
        //    //        {

        //    //            detailItem.InterUnitOut=x.












        //                if (x.TrType == ChemicalTransactions.InterUnitOut)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.InterUnitOut += a.Sum(z => z.QtyCr).Value;
        //                    }




        //                }
        //                if (x.TrType == ChemicalTransactions.LoanTakenReturnOut)

        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.LoanTakenReturnOut += a.Sum(z => z.QtyCr).Value;
        //                    }


        //                }
        //                if (x.TrType == ChemicalTransactions.ChemicalIssuanceRecipe)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.ChemicalIssuanceRecipe += a.Sum(z => z.QtyCr).Value;
        //                    }


        //                }
        //                if (x.TrType == ChemicalTransactions.LoanPartyGivenOut)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.LoanPartyGivenOut += a.Sum(z => z.QtyCr).Value;
        //                    }

        //                }
        //                if (x.TrType == ChemicalTransactions.StoreReturnNote)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.StoreReturnNote += a.Sum(z => z.QtyCr).Value;
        //                    }

        //                }
        //                if (x.TrType == ChemicalTransactions.Dilution)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.DyeId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.Dilution += a.Sum(z => z.QtyCr).Value;
        //                    }


        //                }

        //                detailItem.Sum += (detailItem.InterUnitOut + detailItem.LoanTakenReturnOut + detailItem.LoanPartyGivenOut + detailItem.ChemicalIssuanceRecipe + detailItem.StoreReturnNote + detailItem.Dilution);




        //            });
        //            res.Add(detailItem);

        //        }
        //    }



        //    foreach (var d in chemical.OrderBy(x => x.Name).Where(x => x.IsDeleted != true))
        //    {
        //        var detailItem = new StockOutSummaryViewModel();
        //        {
        //            detailItem.Item = d.Name;
        //            detailItem.DateFrom = filter.DateFrom.Value.ToShortDateString();
        //            detailItem.DateTo = filter.DateTo.Value.ToShortDateString();
        //            detailItem.CompanyName = "COMFORT KNITWEARS PVT LTD";
        //            dcd.ToList().ForEach(x =>//Header
        //            {
        //                if (x.TrType == ChemicalTransactions.InterUnitOut)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true);
        //                   if (a.Any())
        //                    {
        //                          detailItem.InterUnitOut += a.Sum(z => z.QtyCr).Value;
        //                    }

        //                }
        //                if (x.TrType == ChemicalTransactions.LoanTakenReturnOut)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.LoanTakenReturnOut += a.Sum(z => z.QtyCr).Value;
        //                    }

        //                                           }
        //                if (x.TrType == ChemicalTransactions.LoanPartyGivenOut)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.LoanPartyGivenOut += a.Sum(z => z.QtyCr).Value;
        //                    }




        //                }
        //                if (x.TrType == ChemicalTransactions.ChemicalIssuanceRecipe)
        //                {
        //                    var a = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.ChemicalIssuanceRecipe += a.Sum(z => z.QtyCr).Value;
        //                    }




        //                }
        //                if (x.TrType == ChemicalTransactions.StoreReturnNote)
        //                {

        //                    var a = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.StoreReturnNote += a.Sum(z => z.QtyCr).Value;
        //                    }




        //                }
        //                if (x.TrType == ChemicalTransactions.Dilution)
        //                {


        //                    var a = x.DyeChemicalTrDetails.Where(y => y.ChemicalId == d.Id && y.IsDeleted != true);
        //                    if (a.Any())
        //                    {
        //                        detailItem.Dilution += a.Sum(z => z.QtyCr).Value;
        //                    }


        //                }

        //                detailItem.Sum = (detailItem.InterUnitOut + detailItem.LoanTakenReturnOut + detailItem.LoanPartyGivenOut + detailItem.ChemicalIssuanceRecipe + detailItem.StoreReturnNote + detailItem.Dilution);

        //            });

        //            res.Add(detailItem);


        //        }
        //    }


        //    string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
        //        " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
        //    ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");

        //    ViewBag.summaryobject = res;

        //    return new ViewAsPdf(res)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 4 },
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        CustomSwitches = footer
        //    }
        //        ;
        //    return View();

        //}   //[HttpGet]
        //public IActionResult TotalStockReport(PagingOptions options)
        //{
        //    // date wise

        //    var filter = _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();
        //    if (filter == null) return NotFound("Report filter doesn't have any record.");
        //    var dyelist = _uow.DyeService.GetAll();
        //    var chemlist = _uow.ChemicalService.GetAll();
        //    var res = new List<StockSummaryC1ViewModel>();
        //    var title = "Store Balances (For Physical Inventory)";
        //   var rec =_uow.TrLinkerMasterService.GetbetweenDates(filter.DateFrom.Value , filter.DateTo.Value);
        //foreach(var d in dyelist)
        //    {
        //        rec.
        //    }


        //    var gb = rec.GroupBy(x
        //        => x.DyeId).ToList();
        //    gb.ForEach(x =>
        //    {

        //        x.ToList().ForEach(b =>
        //        {
        //            var detailItem = new StockSummaryC1ViewModel
        //            {
        //                   Name = b.Dye?.Name,
        //                    Qty = b.,
        //                    Id = b.Id,

        //            b.ToList().ForEach(ds =>
        //            {

        //                    Name = b.IgpDate,
        //                    Qty = b.Party?.Name,
        //                    Id = b.Id,
        //                    Kgs = ds.GrossWeightInKg,
        //                    NetKgs = ds.NetWeightInKg,
        //                    YarnManufacturer = ds.YarnManufacturer?.Name,
        //                    YarnType = ds.YarnType?.Name,
        //                    YarnQuality = ds.YarnQuality?.Name,
        //                    Po = ds.BuyerPO,
        //                    No = ds.InwardGatePassId.Value,

        //                    IsDateWise = true
        //                };

        //                var lps = _uow.PPCPlanningService.GetListbyIgpDetailId(ds.Id);
        //                if (lps.Count > 0)
        //                {
        //                    lps.ForEach(lp =>
        //                    {
        //                        detailItem.PPCPlaningList.Add(new YarnReceivedReportPPCPlaningModel
        //                        {
        //                            Kgs = lp.Kgs,
        //                            LPSNo = lp.Id,
        //                            IssueId = _uow.IssueNoteDetailService.GetByIGPNO(lp.Id).IssueNoteId,
        //                            IssueDate = _uow.IssueNoteDetailService.GetByIGPNO(lp.Id).IssueNote.IssueDate
        //                        });
        //                        //var issue = _uow.IssueNoteDetailService.GetByIGPNO(lp.Id);
        //                        //if(issue.Id!= 0)
        //                        //{
        //                        //    detailItem.IssueList.Add(new YarnReceivedReportIssueNoteModel
        //                        //    {
        //                        //        Id=issue.IssueNoteId,
        //                        //        IssueDate=issue.IssueNote.IssueDate

        //                        //    });
        //                        //}
        //                    }
        //                    );
        //                    detailItem.BalanceKgs = (ds.NetWeightInKg - lps.Sum(lpskg => lpskg.Kgs));

        //                }
        //                r.Items.Add(detailItem);
        //            });
        //        });
        //        res.Add(r);
        //    });




        //    string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
        //        " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
        //    ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
        //    return new ViewAsPdf(res)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 4 },
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        CustomSwitches = footer
        //    }
        //        ;
        //    return View();
        //}


        public async Task<IActionResult> DyeingProductionAndCostingReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var r = (await _uow.DyeChemicalTrService.DyeingProductionAndCosting(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
            var res = new List<DyeingProductionAndCostingReport_ViewModel>();

            r.ToList().ForEach(d =>
            {
                var sd = new DyeingProductionAndCostingReport_ViewModel();

                sd.DateFrom = filter.DateFrom.Value.ToShortDateString();
                sd.DateTo = filter.DateTo.Value.ToShortDateString();
                sd.CompanyName = _companySettings.Value.CompanyName;
                sd.CompanyBranch = filter.BranchName;

                sd.ProcessName = d.ProcessName;
                sd.TotalKgs = d.TotalKgs;
                sd.ChemicalAmount = d.ChemicalAmount;
                sd.DyeAmount = d.DyeAmount;
                sd.ChemicalCostPerKgs = d.ChemicalCostPerKgs;
                sd.DyeCostPerKgs = d.DyeCostPerKgs;
                sd.TotalCostPerKg = d.TotalCostPerKg;

                res.Add(sd);
            });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
            " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> DyesAndChemicalConsumptionYarn(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var data = (await _uow.DyeChemicalTrService.DyesAndChemicalConsumption(Convert.ToInt32(User.Identity.GetUserId()), true)).ToList();
            var res = new List<DyesAndChemicalConsumption_ReportViewModel>();

            data.ToList().ForEach(d =>
            {
                var sd = new DyesAndChemicalConsumption_ReportViewModel();

                sd.DateFrom = filter.DateFrom?.ToString("dd-MM-yyyy");
                sd.DateTo = filter.DateTo?.ToString("dd-MM-yyyy");
                sd.CompanyName = _companySettings.Value.CompanyName;
                sd.CompanyBranch = filter.BranchName;

                sd.ItemName = d.ItemName;
                sd.IsChemical = d.IsChemical;
                sd.WeightUsed = d.WeightUsed;
                sd.RatePerKg = d.RatePerKg;
                sd.Cost = d.Cost;
                sd.FabricKGs = d.FabricKGs;

                res.Add(sd);
            });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
            " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> DyesAndChemicalConsumptionFabric(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var data = (await _uow.DyeChemicalTrService.DyesAndChemicalConsumption(Convert.ToInt32(User.Identity.GetUserId()), false)).ToList();
            var res = new List<DyesAndChemicalConsumption_ReportViewModel>();

            data.ToList().ForEach(d =>
            {
                var sd = new DyesAndChemicalConsumption_ReportViewModel();

                sd.DateFrom = filter.DateFrom?.ToString("dd-MM-yyyy");
                sd.DateTo = filter.DateTo?.ToString("dd-MM-yyyy");
                sd.CompanyName = _companySettings.Value.CompanyName;
                sd.CompanyBranch = filter.BranchName;

                sd.ItemName = d.ItemName;
                sd.IsChemical = d.IsChemical;
                sd.WeightUsed = d.WeightUsed;
                sd.RatePerKg = d.RatePerKg;
                sd.Cost = d.Cost;
                sd.FabricKGs = d.FabricKGs;

                res.Add(sd);
            });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
            " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> DyesChemicalAndEnergyConsumptionYarn(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var data = (await _uow.DyeChemicalTrService.DyesChemicalAndEnergyConsumption_ViewModel(Convert.ToInt32(User.Identity.GetUserId()), true)).ToList();
            if (data.Count == 0) return NotFound("Report doesn't have any record.");
            var res = new List<DyesChemicalAndEnergyConsumption_ReportViewModel>();

            data.ToList().ForEach(d =>
            {
                var sd = new DyesChemicalAndEnergyConsumption_ReportViewModel();

                sd.DateFrom = filter.DateFrom?.ToString("dd-MM-yyyy");
                sd.DateTo = filter.DateTo?.ToString("dd-MM-yyyy");
                sd.CompanyName = _companySettings.Value.CompanyName;
                sd.CompanyBranch = filter.BranchName;
                

                sd.ItemName = d.ItemName;
                sd.IsChemical = d.IsChemical;
                sd.WeightUsed = d.WeightUsed;
                sd.RatePerKg = d.RatePerKg;
                sd.Cost = d.Cost;
                sd.FabricKGs = d.FabricKGs;

                sd.EnergyEntryDate = d.EnergyEntryDate;
                sd.ElectricityCost = d.ElectricityCost;
                sd.GassCost = d.GassCost;
                sd.CoalCost = d.CoalCost;
                sd.SalaryCost = d.SalaryCost;
                sd.DispatchedKgs = d.DispatchedKgs;
                sd.DispatchedAmount = d.DispatchedAmount;

                res.Add(sd);
            });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
            " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> DyesChemicalAndEnergyConsumptionFabric(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var data = (await _uow.DyeChemicalTrService.DyesChemicalAndEnergyConsumption_ViewModel(Convert.ToInt32(User.Identity.GetUserId()), false)).ToList();
            if (data.Count == 0) return NotFound("Report doesn't have any record.");
            var res = new List<DyesChemicalAndEnergyConsumption_ReportViewModel>();

            data.ToList().ForEach(d =>
            {
                var sd = new DyesChemicalAndEnergyConsumption_ReportViewModel();

                sd.DateFrom = filter.DateFrom?.ToString("dd-MM-yyyy");
                sd.DateTo = filter.DateTo?.ToString("dd-MM-yyyy");
                sd.CompanyName = _companySettings.Value.CompanyName;
                sd.CompanyBranch = filter.BranchName;


                sd.ItemName = d.ItemName;
                sd.IsChemical = d.IsChemical;
                sd.WeightUsed = d.WeightUsed;
                sd.RatePerKg = d.RatePerKg;
                sd.Cost = d.Cost;
                sd.FabricKGs = d.FabricKGs;

                sd.EnergyEntryDate = d.EnergyEntryDate;
                sd.ElectricityCost = d.ElectricityCost;
                sd.GassCost = d.GassCost;
                sd.CoalCost = d.CoalCost;
                sd.SalaryCost = d.SalaryCost;
                sd.DispatchedKgs = d.DispatchedKgs;
                sd.DispatchedAmount = d.DispatchedAmount;

                res.Add(sd);
            });

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
            " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }
    }
}
