using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels.PPC;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class ReportsController : Controller {

        [ViewData]
        public string AreaName { get; set; }
        public decimal RemainingKgs { get; private set; }

        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(ReportsController).Replace("Controller", "")}";

        private readonly IUnitOfWork _uow;
        private readonly IOptions<CompanySettings> _companySettings;
        public ReportsController(IUnitOfWork uow , IOptions<CompanySettings> companySettings) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(ReportsController).Replace("Controller", "")}";
            _uow = uow;
            _companySettings = companySettings;
        }

        [HttpGet]
        public async Task<IActionResult> OgpReport(PagingOptions options) {
            var report = await _uow.OGPService.GetOgpReport(options);
            var res = report.Select(x => new OGPReportViewModel {
                Id = x.Id,
                Description = x.Description,
                Kgs = x.Kgs,
                //LotNo = x.LotNo,
                //    Lps = x.Xref,
                Amount = 0,
                RateKg = 0
            }).ToList();
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> IgpReport(PagingOptions options) {
            try {

                var report = await _uow.IGPService.GetIgpReport(options);
                var res = report.Select(x => new IGPReportViewModel {
                    Bags = x.Bags,
                    Description = x.Description,
                    IssueDate = x.InwardGatePass?.IgpDate,
                    NetKgs = x.NetWeightInKg,
                    Sno = x.Sno
                }).ToList();

                return View(res);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> YarnReceivedReport(PagingOptions options) {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.IGPService.GetIgpRepYarnRecievedReport(filter);

            var res = new List<YarnReceivedReportViewModel>();
            var title = "Yarn Received Report";

            if (!string.IsNullOrEmpty(options.GroupBy))
            {

                // date wise
                if (options.GroupBy.Equals(ReportingConsts.YARN_RECEIEVED_DATE_WISE))
                {
                    //  title += " (Date Wise)";

                    var gb = rec.GroupBy(x => x.IgpDate.Date).ToList();

                    await Task.WhenAll(gb.Select(async x =>
                    {
                        var r = new YarnReceivedReportViewModel
                        {
                            Descriminator = x.Key.ToShortDateString(),
                        };

                        await Task.WhenAll(x.Select(async b =>
                        {
                            await Task.WhenAll(b.InwardGatePassDetails.Select(async ds =>
                            {
                                var detailItem = new YarnReceivedReportItemViewModel
                                {
                                    Date = b.IgpDate,
                                    Party = b.Party?.Name,
                                    Bags = ds.Bags,
                                    Kgs = ds.GrossWeightInKg,
                                    NetKgs = ds.NetWeightInKg,
                                    YarnManufacturer = ds.YarnManufacturer?.Name,
                                    YarnType = ds.YarnType?.Name,
                                    YarnQuality = ds.YarnQuality?.Name,
                                    Po = ds.BuyerPO,
                                    No = ds.InwardGatePassId.Value,
                                    IsDateWise = true
                                };

                                var lps = await _uow.PPCPlanningService.GetListbyIgpDetailId(ds.Id);
                                if (lps.Count > 0)
                                {
                                    lps.ForEach(lp => detailItem.PPCPlaningList.Add(new YarnReceivedReportPPCPlaningModel
                                    {
                                        Kgs = lp.Kgs,
                                        LPSNo = lp.Id
                                    }));
                                    detailItem.BalanceKgs = (ds.NetWeightInKg - lps.Sum(lpskg => lpskg.Kgs));
                                }
                                r.Items.Add(detailItem);
                            }));
                        }));

                        res.Add(r);
                    }));

                }

                // party wise
                if (options.GroupBy.Equals(ReportingConsts.YARN_RECEIEVED_PARTY_WISE))
                {
                    // title += " (Party Wise)";

                    var gb = rec.GroupBy(x => x.PartyId).ToList();

                    await Task.WhenAll(gb.Select(async x =>
                    {
                        var r = new YarnReceivedReportViewModel
                        {
                            Descriminator = x.FirstOrDefault().Party?.Name,
                        };

                        await Task.WhenAll(x.Select(async b =>
                        {
                            await Task.WhenAll(b.InwardGatePassDetails.Select(async ds =>
                            {
                                var detailItem = new YarnReceivedReportItemViewModel
                                {
                                    Date = b.IgpDate,
                                    Bags = ds.Bags,
                                    Kgs = ds.GrossWeightInKg,
                                    NetKgs = ds.NetWeightInKg,
                                    YarnManufacturer = ds.YarnManufacturer?.Name,
                                    YarnType = ds.YarnType?.Name,
                                    YarnQuality = ds.YarnQuality?.Name,
                                    Po = ds.BuyerPO,
                                    No = ds.InwardGatePassId.Value,
                                    IsDateWise = false
                                };

                                var lps = await _uow.PPCPlanningService.GetListbyIgpDetailId(ds.InwardGatePassId);
                                if (lps.Count > 0)
                                {
                                    lps.ForEach(lp => detailItem.PPCPlaningList.Add(new YarnReceivedReportPPCPlaningModel
                                    {
                                        Kgs = lp.Kgs,
                                        LPSNo = lp.Id
                                    }));
                                }
                                r.Items.Add(detailItem);
                            }));
                        }));

                        res.Add(r);
                    }));
                }

            }

            ViewBag.ReportTitle = title;
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            //    return View(res);
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;
        }

        [HttpGet]
        public async Task<IActionResult> P7EcruYarnStockReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.EcruYarnStockRepository_P7(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P7EcruYarnStockReportViewModel>();
            var title = "Ecru Yarn Stock Report";


            var gb = rec.GroupBy(x => x.IgpDate).ToList();
            gb.ToList().ForEach(x =>
            {

                var r = new P7EcruYarnStockReportViewModel();

                r.Date = x.Key.ToString("MM-dd-yyyy");
                r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();


                x.ToList().ForEach(b =>
                {
                    var detailLPSItem = new P7EcruYarnStockReportDetailViewModel();

                    detailLPSItem.No = Convert.ToInt64(b.IGPNo);

                    detailLPSItem.Party = b.Party;
                    detailLPSItem.YarnManufacturer = b.YarnManufacturer;
                    detailLPSItem.YarnType = b.Yarntype;
                    detailLPSItem.YarnQuality = b.YarnQuality;
                    detailLPSItem.ReturnedKgs = b.ReturnedKgs;
                    detailLPSItem.DyedKgs = b.DyedKgs;
                    detailLPSItem.InStockKgs = b.InStockKgs;
                    detailLPSItem.NetKgs = b.NetKgs;
                    detailLPSItem.Bags = b.Bags; detailLPSItem.TearWeightInKg = b.TearWeightInKg;

                    r.P7EcruYarnStockReportDetailViewModel.Add(detailLPSItem);

                }

                );
                res.Add(r);
            });

            res = res.OrderBy(x => x.Date).ToList();



            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
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

            // return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> P9IssunaceRecordReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.IssuanceRecordRepository_P9ViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P9IssunaceRecordReportlViewModel>();
            var title = "Ecru Yarn Stock Report";


            var gb = rec.GroupBy(x => x.LotNo).ToList();
            gb.ToList().ForEach(x =>
            {

                var r = new P9IssunaceRecordReportlViewModel();

                //r.Date = x.Key.ToString("MM-dd-yyyy");
                r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;
                r.LotNo = x.FirstOrDefault().LotNo;



                x.ToList().ForEach(b =>
                {
                    var detailLPSItem = new P9IssunaceRecordReportDetailViewModel();



                    detailLPSItem.LPSNo = b.LPSNo;
                    detailLPSItem.Kgs = b.Kgs;
                    detailLPSItem.NoOfRolls = b.NoOfRolls;
                    detailLPSItem.Status = b.Status;


                    r.P9IssunaceRecordReportDetailViewModel.Add(detailLPSItem);

                }

                );
                res.Add(r);
            });

            //res = res.OrderBy(x => x.Date).ToList();



            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
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

            // return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> P7EcruYarnStockReport1(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.EcruYarnStockRepository1_P7(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P7EcruYarnStockReportViewModel>();
            var title = "Ecru Yarn Stock Report";


            var gb = rec.GroupBy(x => x.Party).ToList();
            gb.ToList().ForEach(x =>
            {

                var r = new P7EcruYarnStockReportViewModel();

                r.Date = x.Key;
                if (filter.IsYarn == true)
                {
                    r.IsYarn = true;
                }
                else
                {
                    r.IsYarn = false;

                }

                r.DateFrom = DateTime.Now.ToString("MMM-dd-yyyy");
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;


                x.ToList().ForEach(b =>
                {

                    var detailLPSItem = new P7EcruYarnStockReportDetailViewModel();

                    detailLPSItem.No = Convert.ToInt64(b.IGPNo);

                    detailLPSItem.Date = b.IgpDate.ToString("MM-dd-yyyy");
                    detailLPSItem.YarnManufacturer = b.YarnManufacturer;
                    detailLPSItem.YarnType = b.Yarntype;
                    detailLPSItem.YarnQuality = b.YarnQuality;
                    detailLPSItem.ReturnedKgs = b.ReturnedKgs;
                    detailLPSItem.DyedKgs = b.DyedKgs;
                    detailLPSItem.InStockKgs = b.InStockKgs;
                    detailLPSItem.NetKgs = b.NetKgs;
                    detailLPSItem.Bags = b.Bags;
                    detailLPSItem.FabricQuality = b.FabricQuality;
                    detailLPSItem.FabricType = b.FabricType;
                    detailLPSItem.Dia = b.Dia;
                    detailLPSItem.GSM = b.GSM;
                    detailLPSItem.NoOfRolls = b.NoOfRolls;
                    detailLPSItem.IsYarn = b.IsYarn;
                    detailLPSItem.Knitter = b.Knitter;
                    detailLPSItem.TearWeightInKg = b.TearWeightInKg;
                    detailLPSItem.IssuedKgs = b.IssuedKgs;
                    detailLPSItem.Buyer = b.Buyer;
                    if (b.GateReffId != null)
                    {
                        detailLPSItem.GateReffId = b.GateReffId.Value;
                    }
                    if (b.InStockKgs > 5)
                    {
                        r.P7EcruYarnStockReportDetailViewModel.Add(detailLPSItem);

                    }
                }

                );
                res.Add(r);
            });

            res = res.OrderBy(x => x.Date).ToList();
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
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

            // return View(res);
        }

        public async Task<IActionResult> P7EcruYarnStockReport11(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.EcruYarnStockRepository11_P7(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P7EcruYarnStockReportViewModel>();
            var title = "Ecru Yarn Stock Report";


            var gb = rec.GroupBy(x => x.Party).ToList();
            gb.ToList().ForEach(x =>
            {

                var r = new P7EcruYarnStockReportViewModel();
                r.TotalSum = x.Sum(y => y.InStockKgs);


                r.Date = x.Key;
                if (filter.IsYarn == true)
                {
                    r.IsYarn = true;
                }
                else
                {
                    r.IsYarn = false;

                }

                r.DateFrom = DateTime.Now.ToString("MMM-dd-yyyy");
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;





                x.ToList().ForEach(b =>
                {

                    var detailLPSItem = new P7EcruYarnStockReportDetailViewModel();

                    detailLPSItem.No = Convert.ToInt64(b.IGPNo);

                    detailLPSItem.Date = b.IgpDate.ToString("MM-dd-yyyy");
                    detailLPSItem.YarnManufacturer = b.YarnManufacturer;
                    detailLPSItem.YarnType = b.Yarntype;
                    detailLPSItem.YarnQuality = b.YarnQuality;
                    detailLPSItem.ReturnedKgs = b.ReturnedKgs;
                    detailLPSItem.DyedKgs = b.DyedKgs;
                    detailLPSItem.InStockKgs = b.InStockKgs;
                    detailLPSItem.NetKgs = b.NetKgs;
                    detailLPSItem.Bags = b.Bags;
                    detailLPSItem.FabricQuality = b.FabricQuality;
                    detailLPSItem.FabricType = b.FabricType;
                    detailLPSItem.Dia = b.Dia;
                    detailLPSItem.GSM = b.GSM;
                    detailLPSItem.NoOfRolls = b.NoOfRolls;
                    detailLPSItem.IsYarn = b.IsYarn;
                    detailLPSItem.Knitter = b.Knitter;
                    detailLPSItem.TearWeightInKg = b.TearWeightInKg;
                    detailLPSItem.IssuedKgs = b.IssuedKgs;
                    detailLPSItem.Buyer = b.Buyer;
                    detailLPSItem.BuyerPO = b.BuyerPO;
                    detailLPSItem.FinishingKgs = b.FinishingKgs;
                    if (b.GateReffId != null)
                    {
                        detailLPSItem.GateReffId = b.GateReffId.Value;
                    }
                    if (b.InStockKgs > 5)
                    {
                        r.P7EcruYarnStockReportDetailViewModel.Add(detailLPSItem);

                    }
                }

                );
                res.Add(r);
            });

            //res = res.OrderBy(x => x.).ToList();
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
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

            // return View(res);
        }

        public async Task<IActionResult> P10EcruStockSummary(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.EcruStockSummary_P10RepositoryViewModal(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P10EcruStockSummaryViewModal>();
            var title = "Ecru Yarn Stock Report";



            var gb = rec.GroupBy(x => x.Buyer).ToList();
            gb.ToList().ForEach(x =>
            {

                var r = new P10EcruStockSummaryViewModal();
                r.DateFrom = DateTime.Now.ToString("MMM-dd-yyyy");
                r.DateTo = DateTime.Now.ToString("MMM-dd-yyyy");
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;
                r.Buyer = x.Key;
                r.Sum = x.Sum(y => y.TotalWeight);

                x.ToList().ForEach(b =>
                {

                    var detailLPSItem = new P10EcruStockSummaryDetailViewModal();


                    detailLPSItem.FabricQuality = b.FabricQuality;
                    detailLPSItem.FabricType = b.FabricType;
                    detailLPSItem.TotalWeight = b.TotalWeight;

                    if (b.TotalWeight > 5)
                    {
                        r.P10EcruStockSummaryDetailViewModal.Add(detailLPSItem);

                    }
                }

                );


                res.Add(r);
            });

            //res = res.OrderBy(x => x.).ToList();
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
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

            // return View(res);
            // return View(res);
        }

        public async Task<IActionResult> EcruStockSummaryYarnCountGsmWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.EcruStockSummary_YarnCountGsm(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P10EcruStockSummaryYarnContGsmViewModal>();

            foreach (var item in rec)
            {
                var data1 = new P10EcruStockSummaryYarnContGsmViewModal();
                data1.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data1.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data1.CompanyName = _companySettings.Value.CompanyName;
                data1.CompanyBranch = filter.BranchName;

                data1.Buyer = item.Buyer;
                data1.FabricType = item.FabricType;
                data1.FabricQuality = item.FabricQuality;
                data1.GSM = item.GSM;
                data1.YarnCountOfFabric = item.YarnCountOfFabric;
                data1.TotalWeight = item.TotalWeight;


                res.Add(data1);
            }


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

            // return View(res);
            // return View(res);
        }



        [HttpGet]
        public async Task<IActionResult> P3DailProductionReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.DailyProductionService_P3(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P3DailyProductionReportViewModel>();
            var title = "Yarn Received Report";

            if (rec.Count != 0)
            {


                if (rec.FirstOrDefault().IsYarn == true)
                {
                    var gb = rec.GroupBy(x => x.IssuedDate.Date).ToList();
                    gb.ForEach(x =>
                    {
                        var r = new P3DailyProductionReportViewModel();

                        r.Date = x.Key.ToString("MM-dd-yyyy");
                        r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                        r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();
                        r.KgSum = x.Sum(y => y.EcruKg.Value);
                        r.CompanyName = _companySettings.Value.CompanyName;
                        r.CompanyBranch = filter.BranchName;
                        r.DayedKgSum = (decimal)x.Sum(y => y.DyedKg);
                        r.DayedBagsSum = (decimal)x.Sum(y => y.Bags);

                        if (filter.IsYarn == true)
                        {
                            r.Type = true;
                        }
                        else
                        {
                            r.Type = false;

                        }



                        x.ToList().ForEach(b =>
                        {
                            var detailLPSItem = new P3LPSDetailReportViewModel();
                            detailLPSItem.LPSNo = Convert.ToInt64(b.LPSId);
                            detailLPSItem.PurchaseOrderNo = Convert.ToInt64(b.Po);
                            detailLPSItem.LotNo = Convert.ToInt64(b.LotNo);
                            detailLPSItem.Buyer = b.Buyer;
                            detailLPSItem.Shade = b.Color;
                            detailLPSItem.YarnType = b.YarnType;
                            detailLPSItem.YarnQuality = b.YarnQualities;
                            detailLPSItem.Loss = b.Loss;
                            detailLPSItem.Status = b.Status;
                            detailLPSItem.EcruKgs = b.EcruKg;
                            detailLPSItem.FabricType = b.FabricType;
                            detailLPSItem.Dia = b.Dia;
                            detailLPSItem.GSM = b.GSM;
                            detailLPSItem.Cones = b.Cones;
                            detailLPSItem.IsYarn = b.IsYarn.Value;
                            if (b.DyedKg != null)
                            {
                                detailLPSItem.DyeKgs = b.DyedKg.Value;
                            }
                            if (b.Bags != null)
                            {
                                detailLPSItem.DyedBags = b.Bags.Value;

                            }
                            r.LPSDetail.Add(detailLPSItem);


                        }


                        );
                        res.Add(r);
                    });
                }
                else
                {
                    var gb = rec.GroupBy(x => x.IssuedDate.Date).ToList();
                    gb.ForEach(x =>
                    {


                        var fb = x.GroupBy(z => z.LotNo).ToList();
                        fb.ForEach(z =>
                        {

                            var hb = z.GroupBy(y => y.Buyer).ToList();
                            hb.ForEach(y =>
                            {



                                var r = new P3DailyProductionReportViewModel();

                                r.Date = x.Key.ToString("MM-dd-yyyy");
                                r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                                r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();
                                r.KgSum = y.Sum(a => a.EcruKg.Value);
                                r.CompanyName = _companySettings.Value.CompanyName;
                                r.CompanyBranch = filter.BranchName;
                                r.DayedKgSum = (decimal)y.Sum(a => a.DyedKg);
                                r.DayedBagsSum = (decimal)y.Sum(a => a.Bags);

                                if (filter.IsYarn == true)
                                {
                                    r.Type = true;
                                }
                                else
                                {
                                    r.Type = false;

                                }



                                y.ToList().ForEach(b =>
                                {
                                    var detailLPSItem = new P3LPSDetailReportViewModel();
                                    detailLPSItem.LPSNo = Convert.ToInt64(b.LPSId);
                                    detailLPSItem.PurchaseOrderNo = Convert.ToInt64(b.Po);
                                    detailLPSItem.LotNo = Convert.ToInt64(b.LotNo);
                                    detailLPSItem.Buyer = b.Buyer;
                                    detailLPSItem.Shade = b.Color;
                                    detailLPSItem.YarnType = b.YarnType;
                                    detailLPSItem.YarnQuality = b.YarnQualities;
                                    detailLPSItem.Loss = b.Loss;
                                    detailLPSItem.Status = b.Status;
                                    detailLPSItem.EcruKgs = b.EcruKg;
                                    detailLPSItem.FabricType = b.FabricType;
                                    detailLPSItem.Dia = b.Dia;
                                    detailLPSItem.GSM = b.GSM;
                                    detailLPSItem.Cones = b.Cones;
                                    detailLPSItem.IsYarn = b.IsYarn.Value;
                                    if (b.DyedKg != null)
                                    {
                                        detailLPSItem.DyeKgs = b.DyedKg.Value;
                                    }
                                    if (b.Bags != null)
                                    {
                                        detailLPSItem.DyedBags = b.Bags.Value;

                                    }
                                    r.LPSDetail.Add(detailLPSItem);


                                }


                                );
                                res.Add(r);

                            });
                        });
                    });







                }

                res = res.OrderBy(x => x.Date).ToList();
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
        public async Task<IActionResult> P2EcruYarnConsumptionReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.EcruYarnConsumptionService_P2(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<YarnReceivedReportViewModel>();
            var title = "Yarn Received Report";

            var gb = rec.GroupBy(x => x.IGPDetailId).ToList();
            gb.ToList().ForEach(x =>
                   {

                       var r = new YarnReceivedReportViewModel();
                       r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                       r.DateTo = filter.DateTo.Value.ToShortDateString();
                       r.CompanyName = _companySettings.Value.CompanyName;
                       r.CompanyBranch = filter.BranchName;
                       var detailItem = new YarnReceivedReportItemViewModel
                       {
                           Date = x.FirstOrDefault().IgpDate,
                           Party = x.FirstOrDefault().Name,
                           Bags = x.FirstOrDefault().Bags,
                           No = x.FirstOrDefault().IGPNo,
                           TearWeightInKg = x.FirstOrDefault().TearWeightInKg,
                           NetKgs = x.FirstOrDefault().NetWeightInKg,
                           YarnManufacturer = x.FirstOrDefault().YarnManufacturer,
                           YarnType = x.FirstOrDefault().Yarntype,
                           YarnQuality = x.FirstOrDefault().YarnQuality,
                       };



                       x.ToList().ForEach(y =>
                       {



                           var detaillpsitem = new YarnReceivedReportPPCPlaningModel
                           {
                               Kgs = y.Kgs,
                               LPSNo = y.LPSId,
                               Cones = y.Cones,

                           };


                           detailItem.PPCPlaningList.Add(detaillpsitem);
                       });

                       r.Items.Add(detailItem);
                       res.Add(r);
                   });




            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
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
        public async Task<IActionResult> P1EcruYarnReceivedReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.EcruYarnReceiveService_P1(Convert.ToInt64(User.Identity.GetUserId()));

            var res = new List<YarnReceivedReportViewModel>();
            var title = "Yarn Received Report";

            var gb = rec.GroupBy(x => x.IgpDate.Date)./*OrderBy(s => s.Key)*/ToList();
            gb.ForEach(x =>
               {
                   ;
                   var r = new YarnReceivedReportViewModel
                   {
                       Descriminator = x.Key.ToString("MM-dd-yyyy"),
                       DateFrom = filter.DateFrom.Value.ToShortDateString(),
                       DateTo = filter.DateTo.Value.ToShortDateString(),
                       CompanyName = _companySettings.Value.CompanyName,
                       CompanyBranch = filter.BranchName,
                       
                   };
                   x.ToList().ForEach(b =>
                   {

                       var detailItem = new YarnReceivedReportItemViewModel
                       {
                           Date = b.IgpDate,
                           Party = b.Name,
                           Bags = b.Bags,
                           No = b.IGPNo,
                           TearWeightInKg = b.TearWeightInKg,
                           NetKgs = b.NetWeightInKg,
                           YarnManufacturer = b.YarnManufacturer,
                           YarnType = b.Yarntype,
                           YarnQuality = b.YarnQuality,
                       };

                       r.Items.Add(detailItem);
                    //});
                });
                   res.Add(r);
               });
            // }
            //}
            res = res.OrderBy(x => x.Descriminator).ToList();
            ViewBag.ReportTitle = title;
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            //    return View(res);
            ViewData["Message"] = "Your contact page.";
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            // return View(res);
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };

        }
        [HttpGet]
        public async Task<IActionResult> P5DyedYarnDespatchedReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.DyedYarnDespatchedService_P5(Convert.ToInt64(User.Identity.GetUserId()));

            var res = new List<YarnReceivedReportViewModel>();
            var title = "Yarn Received Report";
            var gb = rec.GroupBy(x => x.OgpDate.Date).ToList();
            gb.ForEach(x =>
            {
                YarnReceivedReportViewModel r = new YarnReceivedReportViewModel();


                r.Descriminator = x.Key.ToString("MM-dd-yyyy");
                r.DateFrom = filter.DateFrom.Value.ToShortDateString();
                r.DateTo = filter.DateTo.Value.ToShortDateString();
                r.CompanyName = _companySettings.Value.CompanyName;
                r.CompanyBranch = filter.BranchName;
                r.KgSum = x.Sum(y => y.Kgs);

                if (filter.IsYarn == true)
                {
                    r.IsYarn = true;
                }
                else
                {
                    r.IsYarn = false;

                }


                x.ToList().ForEach(b =>
                {

                    var detailItem = new YarnReceivedReportItemViewModel
                    {
                        Date = b.OgpDate,
                        Party = b.Party,
                        Bags = b.Bags,
                        No = b.OGPNo,
                        NetKgs = b.Kgs,
                        Shade = b.Shade,
                        Buyer = b.Buyer,

                        YarnManufacturer = b.Manufacturer,
                        YarnType = b.YarnType,
                        FabricType = b.FabricType,
                        Knitter = b.Knitter,
                        IsYarn = b.IsYarn,
                        YarnQuality = b.Quality,
                        LotNo = b.LotNo,
                        //LPSId = b.LPSId.Value,
                        Description = b.Description,
                    };
                    r.Items.Add(detailItem);

                });
                res.Add(r);
            });
            res = res.OrderBy(x => x.Descriminator).ToList();

            ViewBag.ReportTitle = title;
            ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
            ViewData["Message"] = "Your contact page.";
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
        public async Task<IActionResult> YarnReceivedDespatchReport(PagingOptions options)
        {
            // date wise

            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.IGPService.GetIgpRepYarnRecievedReport(filter);

            var res = new List<YarnReceivedReportViewModel>();
            var title = "Yarn Received Report";


            var gb = rec.GroupBy(x => x.IgpDate.Date).ToList();

            await Task.WhenAll(gb.Select(async x =>
            {
                var r = new YarnReceivedReportViewModel
                {
                    Descriminator = x.Key.ToShortDateString(),
                };

                await Task.WhenAll(x.Select(async b =>
                {
                    await Task.WhenAll(b.InwardGatePassDetails.Select(async ds =>
                    {
                        var detailItem = new YarnReceivedReportItemViewModel
                        {
                            Date = b.IgpDate,
                            Party = b.Party?.Name,
                            Bags = ds.Bags,
                            Kgs = ds.GrossWeightInKg,
                            NetKgs = ds.NetWeightInKg,
                            YarnManufacturer = ds.YarnManufacturer?.Name,
                            YarnType = ds.YarnType?.Name,
                            YarnQuality = ds.YarnQuality?.Name,
                            Po = ds.BuyerPO,
                            No = ds.InwardGatePassId.Value,

                            IsDateWise = true
                        };

                        var lps = await _uow.PPCPlanningService.GetListbyIgpDetailId(ds.Id);
                        if (lps.Count > 0)
                        {
                            await Task.WhenAll(lps.Select(async lp =>
                            {
                                YarnReceivedReportPPCPlaningModel yarnReceivedReportPPCPlaningModel = new YarnReceivedReportPPCPlaningModel();

                                yarnReceivedReportPPCPlaningModel.Kgs = lp.Kgs;
                                yarnReceivedReportPPCPlaningModel.LPSNo = lp.Id;
                                var issuenoteid = await _uow.IssueNoteDetailService.GetByIGPNO(lp.Id);
                                if (issuenoteid != null)
                                {

                                    yarnReceivedReportPPCPlaningModel.IssueId = issuenoteid.IssueNoteId;
                                    yarnReceivedReportPPCPlaningModel.IssueDate = issuenoteid.IssueNote.IssueDate;
                                }

                            }));
                            //var issue = _uow.IssueNoteDetailService.GetByIGPNO(lp.Id);
                            //if(issue.Id!= 0)
                            //{
                            //    detailItem.IssueList.Add(new YarnReceivedReportIssueNoteModel
                            //    {
                            //        Id=issue.IssueNoteId,
                            //        IssueDate=issue.IssueNote.IssueDate

                            //    });
                            //}
                            detailItem.BalanceKgs = (ds.NetWeightInKg - lps.Sum(lpskg => lpskg.Kgs));
                        }

                        r.Items.Add(detailItem);
                    }));

                }));

                res.Add(r);
            }));
        
            res = res.OrderBy(x => x.Descriminator).ToList();

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
        [HttpGet]
        public async Task<IActionResult> P4DyedStockReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.DyedStockService_P4(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P3DailyProductionReportViewModel>();
            
            //var o = new P3DailyProductionReportViewModel();
       
                var title = "Yarn Received Report";
            if (rec.Count() > 0)
            {
                if (rec.FirstOrDefault().IsYarn == true)
                {

                    var gb = rec.GroupBy(x => x.Buyer).ToList();
                    gb.ForEach(x =>
                    {

                        P3DailyProductionReportViewModel r = new P3DailyProductionReportViewModel();

                        r.Buyer = x.Key;
                        r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                        r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();
                        r.CompanyName = _companySettings.Value.CompanyName;
                        r.CompanyBranch = filter.BranchName;

                        r.Buyer = x.Key;
                        r.KgSum = x.Sum(y => y.EcruKg.Value);
                        r.DayedKgSum = (decimal)x.Sum(y => (y.RemainingKgs));
                        r.DayedBagsSum = (decimal)x.Sum(y => y.DyedBags);



                        if (filter.IsYarn == true)
                        {
                            r.Type = true;
                        }
                        else
                        {
                            r.Type = false;
                            r.TotalWeight = x.FirstOrDefault().TotalWeight.Value;
                            r.Loss = (r.DayedKgSum - r.TotalWeight) / r.DayedKgSum * 100;
                        }

                


                    x.ToList().ForEach(b =>
                        {
                    

                        P3LPSDetailReportViewModel detailLPSItem = new P3LPSDetailReportViewModel();
                            if (b.LPSId != null) { detailLPSItem.LPSNo = Convert.ToInt64(b.LPSId); }
                            if (b.Po != null)
                            { detailLPSItem.PurchaseOrderNo = Convert.ToInt64(b.Po); }
                            if (b.LotNo != null) { detailLPSItem.LotNo = Convert.ToInt64(b.LotNo); }

                            detailLPSItem.IssuedDate = b.IssuedDate;
                            detailLPSItem.Shade = b.Shade;
                            detailLPSItem.Status = b.Status;
                            detailLPSItem.YarnType = b.YarnType;
                            detailLPSItem.YarnQuality = b.YarnQuality;
                            detailLPSItem.DyedBags = b.DyedBags;

                            detailLPSItem.FabricType = b.FabricType;
                            detailLPSItem.Dia = b.Dia;
                            detailLPSItem.GSM = b.GSM;


                            detailLPSItem.EcruKgs = b.EcruKg;
                            detailLPSItem.DyeKgs = b.RemainingKgs;
                            detailLPSItem.IsYarn = b.IsYarn;

                            if (b.IssuedDate != null)
                            {
                                detailLPSItem.IssuedDate = b.IssuedDate;


                            }

                            if (b.Loss != null) { detailLPSItem.Loss = b.Loss; }




                        r.LPSDetail.Add(detailLPSItem);


                        });


                        res.Add(r);

                    });

                }
                else
                {
                    var fb = rec.GroupBy(x => x.LotNo).ToList();
                    fb.ForEach(x =>
                    {

                        var gb = x.GroupBy(y => y.Buyer).ToList();
                        gb.ForEach(y =>
                        {


                            P3DailyProductionReportViewModel r = new P3DailyProductionReportViewModel();

                            r.Buyer = y.Key;
                            r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                            r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();


                            r.Buyer = y.FirstOrDefault().Buyer;
                            r.KgSum = y.Sum(z => z.EcruKg.Value);
                        //r.TotalWeight = y.FirstOrDefault().RemainingKgs.Value;
                        r.DayedBagsSum = (decimal)y.Sum(z => z.DyedBags);



                            if (filter.IsYarn == true)
                            {
                                r.Type = true;
                            }
                            else
                            {
                                r.Type = false;
                                r.TotalWeight = y.FirstOrDefault().TotalWeight.Value;
                            //r.Loss = (r.DayedKgSum - r.TotalWeight) / r.DayedKgSum * 100;
                        }


                        y.ToList().ForEach(b =>
                            {
                          

                            P3LPSDetailReportViewModel detailLPSItem = new P3LPSDetailReportViewModel();
                                if (b.LPSId != null) { detailLPSItem.LPSNo = Convert.ToInt64(b.LPSId); }
                                if (b.Po != null)
                                { detailLPSItem.PurchaseOrderNo = Convert.ToInt64(b.Po); }
                                if (b.LotNo != null) { detailLPSItem.LotNo = Convert.ToInt64(b.LotNo); }

                                detailLPSItem.IssuedDate = b.IssuedDate;
                                detailLPSItem.Shade = b.Shade;
                                detailLPSItem.Status = b.Status;
                                detailLPSItem.YarnType = b.YarnType;
                                detailLPSItem.YarnQuality = b.YarnQuality;
                                detailLPSItem.DyedBags = b.DyedBags;
                                detailLPSItem.Buyer = b.Buyer;

                                detailLPSItem.FabricType = b.FabricType;
                                detailLPSItem.Dia = b.Dia;
                                detailLPSItem.GSM = b.GSM;


                                detailLPSItem.EcruKgs = b.EcruKg;
                                detailLPSItem.DyeKgs = b.RemainingKgs;
                                detailLPSItem.IsYarn = b.IsYarn;

                                if (b.IssuedDate != null)
                                {
                                    detailLPSItem.IssuedDate = b.IssuedDate;


                                }

                                if (b.Loss != null) { detailLPSItem.Loss = b.Loss; }

                        



                            r.LPSDetail.Add(detailLPSItem);


                            });

                            if (r.TotalWeight!=0)
                            {
                                res.Add(r);
                            }
                           

                        });
                    }
                    );

                }


            }
            res = res.OrderBy(x => x.Date).ToList();
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
        public async Task<IActionResult> P4DyedStockReport1(PagingOptions options)
            {
                var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
                if (filter == null) return NotFound("Report filter doesn't have any record.");

                var rec = await _uow.PPCPlanningService.DyedStockService1_P4(Convert.ToInt32(User.Identity.GetUserId()));

                var res = new List<P3DailyProductionReportViewModel>();

                //var o = new P3DailyProductionReportViewModel();

                var title = "Yarn Received Report";
                if (rec.Count() > 0)
                {
                    if (rec.FirstOrDefault().IsYarn == true)
                    {

                        var gb = rec.GroupBy(x => x.Buyer).ToList();
                        gb.ForEach(x =>
                        {

                            P3DailyProductionReportViewModel r = new P3DailyProductionReportViewModel();

                            r.Buyer = x.Key;
                            r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                            r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();

                            r.Buyer = x.Key;
                            r.KgSum = x.Sum(y => y.EcruKg.Value);
                            r.DayedKgSum = (decimal)x.Sum(y => (y.RemainingKgs));
                            r.DayedBagsSum = (decimal)x.Sum(y => y.DyedBags);
                            r.CompanyName = _companySettings.Value.CompanyName;
                            r.CompanyBranch = filter.BranchName;



                            if (filter.IsYarn == true)
                            {
                                r.Type = true;
                            }
                            else
                            {
                                r.Type = false;
                                r.TotalWeight = x.FirstOrDefault().TotalWeight.Value;
                                r.Loss = (r.DayedKgSum - r.TotalWeight) / r.DayedKgSum * 100;
                            }
                            x.ToList().ForEach(b =>
                            {                  
                              P3LPSDetailReportViewModel detailLPSItem = new P3LPSDetailReportViewModel();
                                if (b.LPSId != null) { detailLPSItem.LPSNo = Convert.ToInt64(b.LPSId); }
                                if (b.Po != null)
                                { detailLPSItem.PurchaseOrderNo = Convert.ToInt64(b.Po); }
                                if (b.LotNo != null) { detailLPSItem.LotNo = Convert.ToInt64(b.LotNo); }

                                detailLPSItem.IssuedDate = b.IssuedDate;
                                detailLPSItem.Shade = b.Shade;
                                detailLPSItem.Status = b.Status;
                                detailLPSItem.YarnType = b.YarnType;
                                detailLPSItem.YarnQuality = b.YarnQuality;
                                detailLPSItem.DyedBags = b.DyedBags;

                                detailLPSItem.FabricType = b.FabricType;
                                detailLPSItem.Dia = b.Dia;
                                detailLPSItem.GSM = b.GSM;


                                detailLPSItem.EcruKgs = b.EcruKg;
                                detailLPSItem.DyeKgs = b.RemainingKgs;
                                detailLPSItem.IsYarn = b.IsYarn;

                                if (b.IssuedDate != null)
                                {
                                    detailLPSItem.IssuedDate = b.IssuedDate;


                                }
                                if (b.Loss != null) { detailLPSItem.Loss = b.Loss; }                              
                                r.LPSDetail.Add(detailLPSItem);
                            });
                            res.Add(r);
                        });
                    }
                    else
                    { 
                        var fb = rec.GroupBy(x => x.LotNo).ToList();
                        fb.ForEach(x =>
                        {
                            
                            var gb = x.GroupBy(y => y.Buyer).ToList();
                            gb.ForEach(y =>
                            {
                                P3DailyProductionReportViewModel r = new P3DailyProductionReportViewModel();
                                r.Buyer = y.Key;
                                r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                                r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();
                                r.Buyer = y.FirstOrDefault().Buyer;
                                r.KgSum = y.Sum(z => z.EcruKg.Value);                             
                                r.DayedBagsSum = (decimal)y.Sum(z => z.DyedBags);
                                r.CompanyName = _companySettings.Value.CompanyName;
                                r.CompanyBranch = filter.BranchName;
                                if (filter.IsYarn == true)
                                {
                                    r.Type = true;
                                }
                                else
                                {
                                    r.Type = false;
                                    if (y.FirstOrDefault().TotalWeight != null)
                                    {
                                        r.TotalWeight = y.FirstOrDefault().TotalWeight.Value;

                                    } 
                              
                                }
                                y.ToList().ForEach(b =>
                                {
                                    P3LPSDetailReportViewModel detailLPSItem = new P3LPSDetailReportViewModel();
                                    if (b.LPSId != null) { detailLPSItem.LPSNo = Convert.ToInt64(b.LPSId); }
                                    if (b.Po != null)
                                    { detailLPSItem.PurchaseOrderNo = Convert.ToInt64(b.Po); }
                                    if (b.LotNo != null) { detailLPSItem.LotNo = Convert.ToInt64(b.LotNo); }

                                    detailLPSItem.IssuedDate = b.IssuedDate;
                                    detailLPSItem.Shade = b.Shade;
                                    detailLPSItem.Status = b.Status;
                                    detailLPSItem.YarnType = b.YarnType;
                                    detailLPSItem.YarnQuality = b.YarnQuality;
                                    detailLPSItem.DyedBags = b.DyedBags;
                                    detailLPSItem.Buyer = b.Buyer;

                                    detailLPSItem.FabricType = b.FabricType;
                                    detailLPSItem.Dia = b.Dia;
                                    detailLPSItem.GSM = b.GSM;


                                    detailLPSItem.EcruKgs = b.EcruKg;
                                    detailLPSItem.DyeKgs = b.RemainingKgs;
                                    detailLPSItem.IsYarn = b.IsYarn;

                                    if (b.IssuedDate != null)
                                    {
                                        detailLPSItem.IssuedDate = b.IssuedDate;


                                    }

                                    if (b.Loss != null) { detailLPSItem.Loss = b.Loss; }





                                    r.LPSDetail.Add(detailLPSItem);


                                });

                                if (r.TotalWeight != 0)
                                {
                                    res.Add(r);
                                }


                            });
                        }
                        );

                    }


                }



                res = res.OrderBy(x => x.Date).ToList();
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
        public async Task<IActionResult> P8IssuedLPSReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.LPSIssuanceRepository_P8ViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<P3DailyProductionReportViewModel>();
            var title = "Yarn Received Report";

            if (rec.Count != 0)
            {            
                    var gb = rec.GroupBy(x => x.IssuanceDate.Date).ToList();
                    gb.ForEach(x =>
                    {
                        var fb = x.GroupBy(z => z.LotNo).ToList();
                        fb.ForEach(z =>
                        {
                            var hb = z.GroupBy(y => y.Buyer).ToList();
                            hb.ForEach(y =>
                            {
                                var r = new P3DailyProductionReportViewModel();

                                r.Date = x.Key.ToString("MM-dd-yyyy");
                                r.DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString();
                                r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();
                                r.CompanyName = _companySettings.Value.CompanyName;
                                r.CompanyBranch = filter.BranchName;
                                r.KgSum = y.Sum(a => a.EcruKg.Value);
                              

                                if (filter.IsYarn == true)
                                {
                                    r.Type = true;
                                }
                                else
                                {
                                    r.Type = false;
                                }
                                y.ToList().ForEach(b =>
                                {
                                    var detailLPSItem = new P3LPSDetailReportViewModel();
                                    detailLPSItem.LPSNo = Convert.ToInt64(b.LPSId);
                                    detailLPSItem.PurchaseOrderNo = Convert.ToInt64(b.Po);
                                    detailLPSItem.FactoryPo = b.FactoryPo;
                                    detailLPSItem.LotNo = Convert.ToInt64(b.LotNo);
                                    detailLPSItem.Buyer = b.Buyer;
                                    detailLPSItem.Shade = b.Color;
                                  
                                    detailLPSItem.EcruKgs = b.EcruKg;
                                    detailLPSItem.FabricType = b.FabricType;
                                    detailLPSItem.Dia = b.Dia;
                                    detailLPSItem.GSM = b.GSM;
                                    detailLPSItem.IsIssued = b.IsIssued;
                                
                                    detailLPSItem.IsYarn = b.IsYarn.Value;
                                   
                                    r.LPSDetail.Add(detailLPSItem);
                                }

                                );
                                res.Add(r);

                            });
                        });
                    });

                res = res.OrderBy(x => x.Date).ToList();
            }
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
        [HttpGet]
        public async Task<IActionResult> P5DailProductionReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.DyedYarnDespatchedService_P5(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<YarnReceivedReportViewModel>();
            var title = "Yarn Received Report";

            // if (!string.IsNullOrEmpty(options.GroupBy))
            // {

            // date wise
            //if (options.GroupBy.Equals(ReportingConsts.YARN_RECEIEVED_DATE_WISE))
            //{
            //  title += " (Date Wise)";

            var gb = rec.GroupBy(x => x.OgpDate.Date).ToList();



            gb.ForEach(x =>
            {
                var r = new YarnReceivedReportViewModel
                {
                    Descriminator = x.Key.ToShortDateString(),
                    DateFrom = filter.DateFrom.Value.ToShortDateString(),
                DateTo = filter.DateTo.Value.ToShortDateString(),
                    KgSum = x.Sum(y => y.Kgs),


            };
   
                x.ToList().ForEach(b =>
                {
var detailItem = new YarnReceivedReportItemViewModel
                    {
                        Date = b.OgpDate,
                        Party = b.Party,
                        Bags = b.Bags,
                        No = b.OGPNo,
                        NetKgs = b.Kgs,
                        YarnManufacturer = b.Manufacturer,
                        YarnType = b.YarnType,
                        YarnQuality = b.Quality,
                        LotNo= b.LotNo,
                        LPSId = b.LPSId.Value,
                        Description=b.Description,
                    };

                    //    var lps = _uow.PPCPlanningService.GetListbyIgpDetailId(ds.Id);
                    //    if (lps.Count > 0)
                    //    {
                    //        lps.ForEach(lp => detailItem.PPCPlaningList.Add(new YarnReceivedReportPPCPlaningModel
                    //        {
                    //            Kgs = lp.Kgs,
                    //            LPSNo = lp.Id
                    //        }));
                    //        detailItem.BalanceKgs = (ds.NetWeightInKg - lps.Sum(lpskg => lpskg.Kgs));
                    //    }
                    r.Items.Add(detailItem);
                    //});
                });
                res.Add(r);
            });
            res = res.OrderBy(x => x.Descriminator).ToList();
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };}
        [HttpGet]
        public async Task<IActionResult> P6YarnDespatchedReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.OGPDetailService.GetIgpRepYarnRecievedReport(filter);

            var res = new List<P6YarnDespatchedViewModel>();
            var title = "Yarn Received Report";

            var gb = rec.GroupBy(x => x.OutwardGatePass.OgpDate).ToList();
            gb.ForEach(x =>
            {
                var r = new P6YarnDespatchedViewModel
                {
                    Date = x.Key.ToShortDateString(),
                    DateFrom = Convert.ToDateTime(filter.DateFrom.Value).ToShortDateString(),
                    DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString(),
                    Total = x.Sum(y => y.Kgs),

                };

                x.ToList().ForEach(b =>
                {
                    var detailLPSItem = new P6YarnDespatchedDetailViewModel
                    {
                        LpsNo = Convert.ToInt64(b.PPCPlanningId),
                        LotNo = b.PPCPlanning.LotNo,
                        Color = b.PPCPlanning.BuyerColor.Name,
                        YarnType = b.PPCPlanning.YarnType.Name,
                        YarnQuality = b.PPCPlanning.YarnQuality.Name,
                        Party = b.PPCPlanning.BuyerColor.Buyer.Party.Name,
                        No = b.PPCPlanning.Id,
                        NetKgs = b.Kgs,
                    };

                    r.YarnDespatchedDetail.Add(detailLPSItem);
                });
           res.Add(r);
            }


                );

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;

        }
        [HttpGet]
        public async Task<IActionResult> P11DyedStockSummaryReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.DyedStockSummary_P11RepositoryViewModal(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<DyedStockSummary_P11ViewModal>();
            
            var title = "Yarn Received Report";
            var fb = rec.GroupBy(x => x.Buyer).ToList();
            fb.ForEach(x =>
            {

                var gb = rec.GroupBy(y => y.Shade).ToList();
            //var ng= gb.GroupBy()
            gb.ToList().ForEach(y =>
            {

                var r = new DyedStockSummary_P11ViewModal();
                
                r.DateFrom = DateTime.Now.ToString("MMM-dd-yyyy");
                r.Shade = y.Key;
                r.Buyer = x.Key;
              r.Total=  (decimal)rec.Sum(j=>j.Sum
              );
              r.Total2=  (decimal)y.Sum(n => n.Sum);

                //r.Sum = x.Sum(y => y.TotalWeight);

                y.ToList().ForEach(b =>
                {

                    var detailLPSItem = new DyedStockSummaryDetail_P11ViewModal();


                    detailLPSItem.Sum = b.Sum;
                    detailLPSItem.FabricType = b.FabricType;
                    detailLPSItem.Shade = b.Shade;

                    if (b.Sum > 5)
                    {
                        r.DyedStockSummaryDetail_P11ViewModal.Add(detailLPSItem);

                    }
                }

                );


                res.Add(r);
            });
            }
             );

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;

        }
        [HttpGet]
        public async Task<IActionResult> P12FactoryPoKgsPeportReport(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var rec = await _uow.PPCPlanningService.FactoryPoKgRepository_P12ViewModel();

            var res = new List<FactoryPoKgs_P12ViewModel>();

            var title = "Yarn Received Report";
            var fb = rec.GroupBy(x => x.Buyer).ToList();
            fb.ForEach(x =>
            {

              //  var gb = rec.GroupBy(y => y.Buyer).ToList();
                //var ng= gb.GroupBy()
                //gb.ToList().ForEach(y =>
                //{

                    var r = new FactoryPoKgs_P12ViewModel();

                   // r.DateFrom = DateTime.Now.ToString("MMM-dd-yyyy");
                    //r.Color = y.Key;
                    r.Buyer = x.Key;
                   
            

                    //r.Sum = x.Sum(y => y.TotalWeight);

                    x.ToList().ForEach(b =>
                    {

                        var detailLPSItem = new FactoryPoKgsDetail_P12ViewModel();


                  
                        detailLPSItem.FabricType = b.FabricType;
                        detailLPSItem.Kgs = b.Kgs;
                        detailLPSItem.NetKg = b.NetKg;
                        detailLPSItem.OgpKgs = b.OgpKgs;
                        detailLPSItem.TotalKgs = b.TotalKgs;
                        detailLPSItem.Color = b.Color;
                        detailLPSItem.Po = b.Po;
                            r.FactoryPoKgsDetail_P12ViewModel.Add(detailLPSItem);

                       
                    }

                    );


                    res.Add(r);
                });
       

            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;

        }

        [HttpGet]
        public async Task<IActionResult> DyeingLossLotWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var res1 = await _uow.PPCPlanningService.DyeingLossLotWiseViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            List<DyeingLossLotWiseReportViewModel> res = new List<DyeingLossLotWiseReportViewModel>();

            foreach (var d in res1)
            {
                DyeingLossLotWiseReportViewModel data = new DyeingLossLotWiseReportViewModel();

                data.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data.CompanyName = _companySettings.Value.CompanyName;
                data.CompanyBranch = filter.BranchName;
                data.Po = d.Po;
                data.LotNo = d.LotNo;
                data.DyedKgs = d.DyedKgs;
                data.DispatchedKgs = d.DispatchedKgs;
                data.BalanceKgs = d.BalanceKgs;
                data.PercentageBalance = d.PercentageBalance;
                data.FabricType = d.FabricType;
                data.YarnType = d.YarnType;
                data.Party = d.Party;
                data.Buyer = d.Buyer;
                data.BuyerColor = d.BuyerColor;

                res.Add(data);
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

        [HttpGet]
        public async Task<IActionResult> DyeingLossPOWiseColorWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var res1 = await _uow.PPCPlanningService.DyeingLossPOWiseColorWiseViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            List<DyeingLossPOWiseColorWiseReportViewModel> res = new List<DyeingLossPOWiseColorWiseReportViewModel>();

            foreach (var d in res1)
            {
                DyeingLossPOWiseColorWiseReportViewModel data = new DyeingLossPOWiseColorWiseReportViewModel();

                data.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data.CompanyName = _companySettings.Value.CompanyName;
                data.CompanyBranch = filter.BranchName;
                data.Po = d.Po;
                data.LotNo = d.LotNo;
                data.DyedKgs = d.DyedKgs;
                data.DispatchedKgs = d.DispatchedKgs;
                data.BalanceKgs = d.BalanceKgs;
                data.PercentageBalance = d.PercentageBalance;
                data.FabricType = d.FabricType;
                data.YarnType = d.YarnType;
                data.Party = d.Party;
                data.Buyer = d.Buyer;
                data.BuyerColor = d.BuyerColor;

                res.Add(data);
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

        [HttpGet]
        public async Task<IActionResult> InwardEcruFabricPOWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var res1 = await _uow.PPCPlanningService.EcruFabricPOWiseKnitterWiseViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<EcruFabricPOWiseKnitterWiseReportViewModel>();

            foreach (var item in res1)
            {
                var data = new EcruFabricPOWiseKnitterWiseReportViewModel();
                data.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data.CompanyName = _companySettings.Value.CompanyName;
                data.CompanyBranch = filter.BranchName;

                data.GateTrId = item.GateTrId;
                data.IgpDate = item.IgpDate;
                data.Buyer = item.Buyer;
                data.FactoryPO = item.FactoryPO;
                data.FabricQuality = item.FabricQuality;
                data.FabricType = item.FabricType;
                data.Dia = item.Dia;
                data.NetWeightInKg = item.NetWeightInKg;
                data.Knitter = item.Knitter;
                data.GSM = item.GSM;

                res.Add(data);
            }


            ViewBag.DateFrom = filter.DateFrom;
            ViewBag.DateTo = filter.DateTo;


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

        [HttpGet]
        public async Task<IActionResult> InwardEcruFabricKnitterWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var res1 = await _uow.PPCPlanningService.EcruFabricPOWiseKnitterWiseViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<EcruFabricPOWiseKnitterWiseReportViewModel>();

            foreach (var item in res1)
            {
                var data = new EcruFabricPOWiseKnitterWiseReportViewModel();
                data.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data.CompanyName = _companySettings.Value.CompanyName;
                data.CompanyBranch = filter.BranchName;

                data.GateTrId = item.GateTrId;
                data.IgpDate = item.IgpDate;
                data.Buyer = item.Buyer;
                data.FactoryPO = item.FactoryPO;
                data.FabricQuality = item.FabricQuality;
                data.FabricType = item.FabricType;
                data.Dia = item.Dia;
                data.NetWeightInKg = item.NetWeightInKg;
                data.Knitter = item.Knitter;
                data.GSM = item.GSM;

                res.Add(data);
            }


            ViewBag.DateFrom = filter.DateFrom;
            ViewBag.DateTo = filter.DateTo;


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

        [HttpGet]
        public async Task<IActionResult> EcruFabricIssuencePoWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var res1 = await _uow.PPCPlanningService.EcruFabricIssuenceViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<EcruFabricIssuenceReportViewModel>();

            foreach (var item in res1)
            {
                var data = new EcruFabricIssuenceReportViewModel();
                data.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data.CompanyName = _companySettings.Value.CompanyName;
                data.CompanyBranch = filter.BranchName;

                data.LpsId = item.LpsId;
                data.FactoryPO = item.FactoryPO;
                data.Fabric = item.Fabric;
                data.IssuedDate = item.IssuedDate;
                data.LotNo = item.LotNo;
                data.IssuedKgs = item.IssuedKgs;
                data.PartyName = item.PartyName;
                data.BuyerName = item.BuyerName;
                data.BuyerColorName = item.BuyerColorName;

                res.Add(data);
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
        [HttpGet]
        public async Task<IActionResult> EcruFabricIssuencePartyWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var res1 = await _uow.PPCPlanningService.EcruFabricIssuenceViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<EcruFabricIssuenceReportViewModel>();

            foreach (var item in res1)
            {
                var data = new EcruFabricIssuenceReportViewModel();
                data.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data.CompanyName = _companySettings.Value.CompanyName;
                data.CompanyBranch = filter.BranchName;

                data.LpsId = item.LpsId;
                data.FactoryPO = item.FactoryPO;
                data.Fabric = item.Fabric;
                data.IssuedDate = item.IssuedDate;
                data.LotNo = item.LotNo;
                data.IssuedKgs = item.IssuedKgs;
                data.PartyName = item.PartyName;
                data.BuyerName = item.BuyerName;
                data.BuyerColorName = item.BuyerColorName;

                res.Add(data);
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
        [HttpGet]
        public async Task<IActionResult> EcruFabricIssuenceColorWise(PagingOptions options)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");

            var res1 = await _uow.PPCPlanningService.EcruFabricIssuenceViewModel(Convert.ToInt32(User.Identity.GetUserId()));

            var res = new List<EcruFabricIssuenceReportViewModel>();

            foreach (var item in res1)
            {
                var data = new EcruFabricIssuenceReportViewModel();
                data.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data.CompanyName = _companySettings.Value.CompanyName;
                data.CompanyBranch = filter.BranchName;

                data.LpsId = item.LpsId;
                data.FactoryPO = item.FactoryPO;
                data.Fabric = item.Fabric;
                data.IssuedDate = item.IssuedDate;
                data.LotNo = item.LotNo;
                data.IssuedKgs = item.IssuedKgs;
                data.PartyName = item.PartyName;
                data.BuyerName = item.BuyerName;
                data.BuyerColorName = item.BuyerColorName;

                res.Add(data);
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

        [HttpGet]
        public async Task<IActionResult> YarnDyeingPurchaseContractForm()
        {
            return new ViewAsPdf()
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
                //CustomSwitches = footer
            }
              ;
        }


        //[HttpGet]
        //public IActionResult PoWiseDispatchReport(PagingOptions options)
        //{
        //    string message = "Simple MessageBox";
        //    string title = "Title";
        ////    MessageBox.Show(message, title);

        //    //  return PartialView();

        //}

        private async Task<IActionResult> DisplayMessageBoxText()
        {
            return View();
        }


        public async Task<IActionResult> AnalysisReport()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var data = (await _uow.DefectAnalysisService.GetAll()).Where(x => x.AnalysisTypeID == filter.AnalysisTypeId).ToList();


            return new ViewAsPdf(data)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                //CustomSwitches = footer
            }
              ;
        }

        public async Task<IActionResult> GetFabricDetailByLot()
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var data = (await _uow.PPCPlanningService.GetFabricDetailByLot(filter.LotNo.Value)).ToList();

            var res = new List<GetFabricDetailByLotReportViewModel>();

            foreach (var item in data)
            {
                var data1 = new GetFabricDetailByLotReportViewModel();
                data1.DateFrom = filter.DateFrom?.ToString("dd-MMM-yyyy");
                data1.DateTo = filter.DateTo?.ToString("dd-MMM-yyyy");
                data1.CompanyName = _companySettings.Value.CompanyName;
                data1.CompanyBranch = filter.BranchName;

                data1.LotNo = item.LotNo;
                data1.LPSID = item.LPSID;
                data1.FactoryPo = item.FactoryPo;
                data1.FabricType = item.FabricType;
                data1.KnittingParty = item.KnittingParty;
                data1.Kgs = item.Kgs;
                data1.Buyer = item.Buyer;
                data1.BuyerColor = item.BuyerColor;
                data1.PartyName = item.PartyName;

                res.Add(data1);
            }

                return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }

    }
}