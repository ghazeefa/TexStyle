using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class ReturnNoteService : IReturnNoteService {
        private readonly IReturnNoteRepository _repo;
        private readonly IReturnNoteDetailRepository _drepo;
        public ReturnNoteService(IReturnNoteRepository repo, IReturnNoteDetailRepository drepo)
        {
            _repo = repo;
            _drepo = drepo;
        }
        public async Task<ReturnNote> Create(ReturnNote o)
        {
            //create yarn type user
            try
            {
                o.CreatedOn = DateTime.Now;
                o.BranchId = 1;
                o.IsYarn = true;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReturnNote> CreateFabric(ReturnNote o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                o.BranchId = 2;
                o.IsYarn = false;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<ReturnNote> Delete(ReturnNote o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<ReturnNote> Update(ReturnNote o) {
            try {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<ReturnNote> UpdateFabric(ReturnNote o)
        {
            try
            {
                // update Fabric user
                o.IsYarn = false;
                o.BranchId = 2;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ReturnNote>> GetAll() {

            try {
                var rn = await _repo.GetList(x => x.IsDeleted == false, navigationProperties: nav => nav.ReturnNoteDetails);

                //rn.ForEach(rtw => {
                //    rtw.ReturnNoteDetails.ToList().ForEach(d => {
                //        var detail = _drepo.GetSingle(x => x.Id == d.Id,
                //            //nav => nav.IssueNote,
                //            nav => nav.ReturnNote,
                //            nav => nav.StoreLocation
                //            //, nav => nav.YarnType,
                //            //nav => nav.YarnQuality,
                //            //nav => nav.YarnManufacturer
                            
                          
                //            );

                //        //d.IssueNote = detail.IssueNote;
                //        d.ReturnNote = detail.ReturnNote;
                //        d.StoreLocation = detail.StoreLocation;
                //        //d.YarnQuality = detail.YarnQuality;
                //        //d.YarnManufacturer = detail.YarnManufacturer;
                //        //d.YarnType = detail.YarnType;
                //    });

                //    rtw.ReturnNoteDetails.ToList().RemoveAll(x => x.IsDeleted);

                //});



                return rn.ToList(); ;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<ReturnNote> GetById(long id) {
            try {
                var rn = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, navigationProperties: nav => nav.ReturnNoteDetails);
                await Task.WhenAll(rn.ReturnNoteDetails.Select(async d => {
                    var detail = await _drepo.GetSingle(x => x.Id == d.Id,
                        //nav => nav.IssueNote,
                        nav => nav.ReturnNote,
                        nav => nav.StoreLocation
                      
                        //,nav => nav.YarnQuality,
                        //nav => nav.YarnType,
                        //nav => nav.YarnManufacturer
                        );

                    //d.IssueNote = detail.IssueNote;
                    d.ReturnNote = detail.ReturnNote;
                    d.StoreLocation = detail.StoreLocation;
                    //d.YarnQuality = detail.YarnQuality;
                    //d.YarnManufacturer = detail.YarnManufacturer;
                    //d.YarnType = detail.YarnType;
                }));

                rn.ReturnNoteDetails.ToList().RemoveAll(x => x.IsDeleted);

                return await Task.FromResult(rn);
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public async Task<IList<ReturnNote>> CheckLotNo(long? buyerid, long? lotNo)
        {
            try
            {
                return await _repo.GetList(x => x.IsDeleted == false && x.LotNo==lotNo && x.BuyerId==buyerid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ReturnNote>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.ReturnDate.Date >= start.Date && x.ReturnDate.Date <= end.Date
                && x.IsYarn == true && x.BranchId == 1, n => n.Buyer);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }

            //public List<ReturnNote> GetIgpRepYarnRecievedReport(ReportFilter filter)
            //{
            //    try
            //    {

            //        Func<ReturnNote, bool> igpPred = x => x.IsDeleted == false;

            //        if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            //        {
            //            igpPred += (x => x.IsDeleted == false && x.ReturnDate.Date >= filter.DateFrom.Value.Date && x.ReturnDate.Date <= filter.DateTo.Value.Date);
            //        }

            //        var res = _repo.GetList(igpPred,
            //            nav => nav.ReturnNoteDetails);
            //        //res.ToList().ForEach(b => {
            //        //    res.FirstOrDefault().Party.Name = _partyService.GetById(Convert.ToInt64(b.PartyId)).Name;
            //        //});

            //        res.ToList().ForEach(igp => {
            //            if (igp.ReturnNoteDetails.Count > 0)
            //            {
            //                //Func<InwardGatePassDetail, bool> predicate = x => x.IsDeleted == false;

            //                //if (filter.YarnQualityId.HasValue)
            //                //{
            //                //    predicate += x => x.YarnQualityId == filter.YarnQualityId;
            //                //}

            //                //if (filter.YarnTypeId.HasValue)
            //                //{
            //                //    predicate += x => x.YarnTypeId == filter.YarnTypeId;
            //                //}

            //               // igp.InwardGatePassDetails = igp.InwardGatePassDetails.Where(predicate).ToList();

            //                igp.ReturnNoteDetails.ToList().ForEach(b => {
            //                    var dtl = _drepo.GetSingle(x => x.Id == b.Id,
            //                    x => x.PPCPlanning,
            //                    x => x.Reprocess);

            //                    b.PPCPlanningId = dtl.PPCPlanningId;
            //                    b.PPCPlanning.PurchaseOrderId = dtl.PPCPlanning.PurchaseOrder.Po;
            //                    b.PPCPlanning.LotNo = dtl.PPCPlanning.LotNo;
            //                    b.PPCPlanning.BuyerColor.Buyer = dtl.PPCPlanning.BuyerColor.Buyer;
            //                    b.PPCPlanning.BuyerColor = dtl.PPCPlanning.BuyerColor;
            //                    b.PPCPlanning.YarnType = dtl.PPCPlanning.YarnType;
            //                    b.PPCPlanning.YarnQuality = dtl.PPCPlanning.YarnQuality;
            //                    b.PPCPlanning.Cones = dtl.PPCPlanning.Cones;
            //                    b.PPCPlanning.Kgs = dtl.PPCPlanning.Kgs;
            //                });
            //            }
            //        });
            //        return res.ToList();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}


        }

        public async Task<List<ReturnNote>> GetBetweenDateRangeFabric(DateTime start, DateTime end)
        {
            try
            {

                var res = await _repo.GetList(x => x.IsDeleted == false && x.ReturnDate.Date >= start.Date && x.ReturnDate.Date <= end.Date && x.IsYarn == false && x.BranchId == 2);
                return res.ToList();
                    }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ReturnNote>> GetProductionDetailReport(ReportFilter filter)
        {
            try
            {

                Func<ReturnNote, bool> igpPred = x => x.IsDeleted == false;

                if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
                {
                    igpPred += (x => x.IsDeleted == false && x.ReturnDate.Date >= filter.DateFrom.Value.Date && x.ReturnDate.Date <= filter.DateTo.Value.Date);
                }

                var res = await _repo.GetList(igpPred,
                    nav => nav.ReturnNoteDetails);
                //res.ToList().ForEach(b => {
                //    res.FirstOrDefault().Party.Name = _partyService.GetById(Convert.ToInt64(b.PartyId)).Name;
                //});

                res.ToList().ForEach(igp => {
                    if (igp.ReturnNoteDetails.Count > 0)
                    {
                        //Func<InwardGatePassDetail, bool> predicate = x => x.IsDeleted == false;

                        //if (filter.YarnQualityId.HasValue)
                        //{
                        //    predicate += x => x.YarnQualityId == filter.YarnQualityId;
                        //}

                        //if (filter.YarnTypeId.HasValue)
                        //{
                        //    predicate += x => x.YarnTypeId == filter.YarnTypeId;
                        //}

                        // igp.InwardGatePassDetails = igp.InwardGatePassDetails.Where(predicate).ToList();

                        igp.ReturnNoteDetails.ToList().ForEach(async b => {
                            var dtl = await _drepo.GetSingle(x => x.Id == b.Id,
                            x => x.PPCPlanning,
                            x => x.Reprocess);

                            b.PPCPlanning = dtl.PPCPlanning;
                            b.Reprocess = dtl.Reprocess;
                            //b.PPCPlanning.PurchaseOrder.Po = dtl.PPCPlanning.PurchaseOrder.Po;
                            //b.PPCPlanning.LotNo = dtl.PPCPlanning.LotNo;
                            //b.PPCPlanning.BuyerColor.Buyer = dtl.PPCPlanning.BuyerColor.Buyer;
                            //b.PPCPlanning.BuyerColor = dtl.PPCPlanning.BuyerColor;
                            //b.PPCPlanning.YarnType = dtl.PPCPlanning.YarnType;
                            //b.PPCPlanning.YarnQuality = dtl.PPCPlanning.YarnQuality;
                            //b.PPCPlanning.Cones = dtl.PPCPlanning.Cones;
                            //b.PPCPlanning.Kgs = dtl.PPCPlanning.Kgs;
                        });
                    }
                });
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
