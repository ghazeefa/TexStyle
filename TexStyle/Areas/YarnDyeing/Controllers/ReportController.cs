using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels.CS.Reports;
using TexStyle.ViewModels.YD.Reports;
using TexStyle.Core.ReportsViewModel.YD;
using AutoMapper;
using TexStyle.ViewModels.PPC.Reports;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System.IO;

namespace TexStyle.Areas.YarnDyeing.Controllers
{  
    [Area("YarnDyeing")]
    public class ReportController : Controller
    {
     
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IOptions<CompanySettings> _companySettings;

        public ReportController(IUnitOfWork uow, IMapper mapper, IOptions<CompanySettings> companySettings)
        {
            this._uow = uow;
            _mapper = mapper;
            _companySettings = companySettings;
        }

        public async Task<IActionResult> Index()
        {
            return (IActionResult)this.View();
        }

        public async Task<IActionResult> DyeingConsupmtionSummary_D1Report()
        {
            ReportFilter filter = (await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
            if (filter == null)
                return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");
            List<DyeingConsupmtionSummary_D1ViewModel> res = new List<DyeingConsupmtionSummary_D1ViewModel>();
            var d =(await _uow.RecipeService.DyeingConsupmtionSummaryRepository_D1(Convert.ToInt64(User.Identity.GetUserId()))).ToList();

            d.ToList().GroupBy(y => y.Status).ToList().ForEach(
                x =>
            {
                DyeingConsupmtionSummary_D1ViewModel r = new DyeingConsupmtionSummary_D1ViewModel();
                r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                r.DateTo = filter.DateTo.Value.ToShortDateString();
                r.Status = x.FirstOrDefault().Status;

                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;

                x.ToList().ForEach(b =>
                {
                    DyeingConsumptionSummaryDetail_D1ViewModel e = new DyeingConsumptionSummaryDetail_D1ViewModel();

                    e.Name = b.Name;
                    e.Qty = b.Qty;
                    e.Rate = b.Rate;
                    r.DyeingConsumptionSummaryDetail_D1ViewModels.Add(e);
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
        
        public async Task<IActionResult> DyeingConsupmtionDetail_D5Report()
            {
                ReportFilter filter =(await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
                if (filter == null)
                    return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");
                List<DyeingDetailConsumption_ViewModel> res = new List<DyeingDetailConsumption_ViewModel>();
                var d =(await _uow.RecipeService.DyeingDetailConsumption_D5(Convert.ToInt64(User.Identity.GetUserId()))).ToList();

            d.ToList().GroupBy(y => y.Status).ToList().ForEach(
              x =>
             {
                 DyeingDetailConsumption_ViewModel r = new DyeingDetailConsumption_ViewModel();
                 r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                 r.DateTo = filter.DateTo.Value.ToShortDateString();
                 r.Status = x.FirstOrDefault().Status.Value;

                 r.CompanyName = _companySettings.Value.CompanyName;
                 r.CompanyBranch = filter.BranchName;

                 x.ToList().ForEach(b =>
                 {
                     DyeingDetailConsumptionDetail_ViewModel e = new DyeingDetailConsumptionDetail_ViewModel();

                     e.Name = b.Name;
                     
                         e.CKL5 = b.CKL5.Value;

                   
                         e.CKL6 = b.CKL6.Value;

                     


                 
                     r.DyeingDetailConsumptionDetail_ViewModel.Add(e);
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
        public async Task<IActionResult> RecipeIssued_D3Report()
            {
                ReportFilter filter =(await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
                if (filter == null)
                    return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");
                List<RecipeIssued_D3ViewModel> res = new List<RecipeIssued_D3ViewModel>();
                var d =(await _uow.RecipeService.RecipeIssuedRepository_D3(Convert.ToInt64(User.Identity.GetUserId()))).ToList();

               d.ToList().GroupBy(y => y.Date).ToList().ForEach(
                 x =>
                {
                    RecipeIssued_D3ViewModel r = new RecipeIssued_D3ViewModel();
                    r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                    r.DateTo = filter.DateTo.Value.ToShortDateString();                    
                    r.Date = x.FirstOrDefault().Date;

                    r.CompanyName = _companySettings.Value.CompanyName;
                    r.CompanyBranch = filter.BranchName;

                    x.ToList().ForEach(b =>
                    {
                        RecipeIssuedDetail_D3ViewModel e = new RecipeIssuedDetail_D3ViewModel();
                        if (b.LPSId != null) { e.LPSId = b.LPSId.Value; }
                        if (b.No != null)
                        { e.No = b.No.Value; }
                        r.RecipeIssuedDetail_D3ViewModel.Add(e);
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



        public async Task<IActionResult> DyeingEfficencyReport(PagingOptions options)
        {
            var filter =(await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var res = new List<StockOutSummaryViewModel>();
            var title = "Dyeing Efficency Report";
            var d =(await _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
            var d1 = (await _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel1(Convert.ToInt64(User.Identity.GetUserId()))).ToList();
            var d2 = (await _uow.DyeChemicalTrService.StockOutSummaryReportRepository_ViewModel2(Convert.ToInt64(User.Identity.GetUserId()))).ToList();
            var d3 = (await _uow.DyeChemicalTrService.DyeChemicalDetailsTrTypeWise(Convert.ToInt64(User.Identity.GetUserId()))).ToList();
            var r = new StockOutSummaryViewModel();


            r.DateFrom = filter.DateFrom.Value.ToShortDateString();
            r.DateTo = filter.DateTo.Value.ToShortDateString();
            r.CompanyName = "COMFORT KNITWEARS PVT LTD(YARN DYEING DIV)";


            //d.ToList().ForEach(y =>
            //{

            //    var sd = new StockOutSummaryViewModel();
            //    sd.DateFrom = filter.DateFrom.Value.ToShortDateString();
            //    sd.DateTo = filter.DateTo.Value.ToShortDateString();
            //    sd.CompanyName = "COMFORT KNITWEARS PVT LTD(YARN DYEING DIV)";
            //    sd.Item = y.Item;

            //    if (y.Diluation != null)
            //    {
            //        sd.DAmount = y.DAmount.Value;
            //        sd.DRate = y.DRate.Value;
            //        sd.Diluation = y.Diluation.Value;
            //    }
            //    if (y.Issuance != null)
            //    {
            //        sd.ICRate = y.ICRate.Value;
            //        sd.ICAmount = y.ICAmount.Value;
            //        sd.Issuance = y.Issuance.Value;
            //    }
            //    if (y.LoanReturn != null)
            //    {
            //        sd.LRCRate = y.LRCRate.Value;
            //        sd.LRCAmount = y.LRCAmount.Value;
            //        sd.LoanReturn = y.LoanReturn.Value;
            //    }
            //    if (y.Rejection != null)
            //    {
            //        sd.RRate = y.RRate.Value;
            //        sd.RAmount = y.RAmount.Value;
            //        sd.Rejection = y.Rejection.Value;
            //    }
            //    if (y.LoanGiven != null)
            //    {
            //        sd.LGCRate = y.LGCAmount.Value;
            //        sd.LGCAmount = y.LGCAmount.Value;
            //        sd.LoanGiven = y.LoanGiven.Value;
            //    }
            //    if (y.InterUnitOut != null)
            //    {
            //        sd.IUORate = y.IUORate.Value;
            //        sd.IUOAmount = y.IUOAmount.Value;
            //        sd.InterUnitOut = y.InterUnitOut.Value;
            //    }





            //    sd.Sum = sd.Rejection + sd.LoanReturn + sd.LoanGiven + sd.Issuance + sd.Diluation + sd.InterUnitOut;
            //    res.Add(sd);
            //});

            StockSummary2ViewModel ress = new StockSummary2ViewModel()
            {
                StockOutSummaryViewModel = res,
                StockOutSummaryViewModel1 = d1,
                StockOutSummaryViewModel2 = d2,
                DyeChemicalDetailsTrTypeWise = d3
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
            }
                ;
            return View();

        }



        //public IActionResult DyeRecipeConsumption_D4Report()
        //    {
        //        ReportFilter filter = this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId())).FirstOrDefault<ReportFilter>();
        //        if (filter == null)
        //            return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");
        //        List<DyeRecipeConsumption_D4ViewModel> res = new List<DyeRecipeConsumption_D4ViewModel>();
        //        var d = _uow.RecipeService.`Convert.ToInt64(User.Identity.GetUserId())).ToList();

        //       d.ToList().GroupBy(y => y.Status).ToList().ForEach(
        //         x =>
        //        {
        //            DyeRecipeConsumption_D4ViewModel r = new DyeRecipeConsumption_D4ViewModel();
        //            r.DateFrom = filter.DateFrom.Value.ToShortDateString();
        //            r.DateTo = filter.DateTo.Value.ToShortDateString();
        //            r.CompanyName = "COMFORT KNITWEARS PVT LTD(YARN DYEING DIV) ";
        //            r.Status = x.FirstOrDefault().Status;

        //            x.ToList().ForEach(b =>
        //            {
        //                DyeRecipeConsumptionDetail_D4ViewModel e = new DyeRecipeConsumptionDetail_D4ViewModel();

        //               e.RecipeNumber= b.
        //                r.RecipeIssuedDetail_D3ViewModel.Add(e);
        //            });
        //            res.Add(r);
        //        });
        //    string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
        //     " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
        //    ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
        //    return new ViewAsPdf(res)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 4 },
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        CustomSwitches = footer
        //    }
        //        ;

        //}


        //public async Task<IActionResult> FabricDyeingRecipeReprocess()
        //{
        //    ReportFilter filter =(await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
        //    if (filter == null)
        //        return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");
        //    List<DyeingRecipeReprocessReport_ViewModel> res = (List<DyeingRecipeReprocessReport_ViewModel>)(await _uow.RecipeService.GetDyeingRecipeReprocesses(Convert.ToInt32(User.Identity.GetUserId()), isyarn: false)).ToList();

        //    string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
        //     " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";

        //    return new ViewAsPdf(res)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 4 },
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        CustomSwitches = footer
        //    };

        //}
        //public async Task<IActionResult> YarnDyeingRecipeReprocess()
        //{
        //    ReportFilter filter =(await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
        //    if (filter == null)
        //        return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");
        //    List<DyeingRecipeReprocessReport_ViewModel> res = (List<DyeingRecipeReprocessReport_ViewModel>)(await _uow.RecipeService.GetDyeingRecipeReprocesses(Convert.ToInt32(User.Identity.GetUserId()), isyarn: true)).ToList();

        //    string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
        //     " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";

        //    return new ViewAsPdf(res)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 4 },
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        CustomSwitches = footer
        //    };

        //}

        public async Task<IActionResult> DyeingRecipeReprocess()
        {
            ReportFilter filter = (await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
            if (filter == null)
                return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");
            var data = (await _uow.RecipeService.GetDyeingRecipeReprocesses(Convert.ToInt32(User.Identity.GetUserId()), filter.IsYarn.Value)).ToList();
            var res = new List<DyeingRecipeReprocessReport_ViewModel>();

            foreach (var item in data)
            {
                var d = new DyeingRecipeReprocessReport_ViewModel();

                d.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                d.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                d.CompanyName = _companySettings.Value.CompanyName;
                d.CompanyBranch = filter.BranchName;

                d.BuyerColor = item.BuyerColor;
                d.LotNo = item.LotNo;
                d.BuyerName = item.BuyerName;
                d.RecipeNo = item.RecipeNo;
                d.RecipeDate = item.RecipeDate;
                d.Weight = item.Weight;
                d.MachineName = item.MachineName;
                d.ShiftName = item.ShiftName;

                if (filter.IsYarn == true)
                {
                    d.ReportName = "Yarn Reprocess";
                }
                else
                {
                    d.ReportName = "Fabric Reprocess";
                }

                res.Add(d);
            }

            

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
             " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";

            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }

        public async Task<IActionResult> ChemicalIssuanceDetail()
        {
            ReportFilter filter = (await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
            if (filter == null)
                return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");

            var data = (await _uow.RecipeService.ChemicalIssuanceDetail(Convert.ToInt32(User.Identity.GetUserId()))).ToList();

            if (data == null || !data.Any())
            {
                return NotFound("No data available to export.");
            }

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("ChemicalIssuance");

                    // Add the headers
                    worksheet.Cells[1, 1].Value = "Chemical/Dyes";
                    worksheet.Cells[1, 2].Value = "RecipeNo";
                    worksheet.Cells[1, 3].Value = "RecipeDate";
                    worksheet.Cells[1, 4].Value = "ItemName";
                    worksheet.Cells[1, 5].Value = "Weight";

                    // Load the data into the worksheet, starting from the second row
                    var exportData = data.Select(d => new
                    {
                        d.ChemicalORDyes,
                        d.RecipeNo,
                        d.RecipeDate,
                        d.ItemName,
                        d.Weight
                    });

                    worksheet.Cells["A2"].LoadFromCollection(exportData, false);

                    // Auto-fit columns for all the data
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Save the package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    var fileName = $"ChemicalIssuanceDetail_{DateTime.Now:yyyyMMdd}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while generating the Excel file: {ex.Message}");
            }
        }

        public async Task<IActionResult> FabricDyeingWeighDetail()
        {
            ReportFilter filter = (await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
            if (filter == null)
                return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");

            var data = (await _uow.RecipeService.FabricDyeingWeighDetail(Convert.ToInt32(User.Identity.GetUserId()))).ToList();

            if (data == null || !data.Any())
            {
                return NotFound("No data available to export.");
            }

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("FabricKgs");

                    // Add the headers
                    worksheet.Cells[1, 1].Value = "FactoryPo";
                    worksheet.Cells[1, 2].Value = "RecipeNo";
                    worksheet.Cells[1, 3].Value = "RecipeDate";
                    worksheet.Cells[1, 4].Value = "Buyer";
                    worksheet.Cells[1, 5].Value = "FabricType";
                    worksheet.Cells[1, 6].Value = "BuyerColor";
                    worksheet.Cells[1, 7].Value = "ProcessName";
                    worksheet.Cells[1, 8].Value = "PPCPlanedKgs";

                    // Create the export data with the right properties mapped
                    var exportData = data.Select(d => new {
                        d.FactoryPo,
                        d.RecipeNo,
                        d.RecipeDate,
                        d.Buyer,
                        d.FabricType,
                        d.BuyerColor,
                        d.ProcessName,
                        d.PPCPlanedKgs
                    });

                    // Load the data into the worksheet, starting from the second row
                    worksheet.Cells["A2"].LoadFromCollection(exportData, false);

                    // Auto-fit columns for all the data
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Save the package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    var fileName = $"FabricDyeingWeighDetail_{DateTime.Now:yyyyMMdd}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while generating the Excel file: {ex.Message}");
            }
        }

        public async Task<IActionResult> TotalRecipesPoBuyerColor()
        {
            ReportFilter filter = (await this._uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(this.User.Identity.GetUserId()))).FirstOrDefault<ReportFilter>();
            if (filter == null)
                return (IActionResult)this.NotFound((object)"Report filter doesn't have any record.");

            var data = (await _uow.RecipeService.TotalRecipes_ViewModel(Convert.ToInt32(User.Identity.GetUserId()))).ToList();
            var res = new List<TotalRecipesReport_ViewModel>();

            foreach (var item in data)
            {
                var d = new TotalRecipesReport_ViewModel();

                d.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                d.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                d.CompanyName = _companySettings.Value.CompanyName;
                d.CompanyBranch = filter.BranchName;

                d.FactoryPo = item.FactoryPo;
                d.RecipeNo = item.RecipeNo;
                d.RecipeDate = item.RecipeDate;
                d.Buyer = item.Buyer;
                d.BuyerId = item.BuyerId;
                d.BuyerColor = item.BuyerColor;
                d.BuyerColorId = item.BuyerColorId;
                d.PPCPlanedKgs = item.PPCPlanedKgs;
                d.Pcs = item.Pcs;

                res.Add(d);
            }

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
             " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";

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