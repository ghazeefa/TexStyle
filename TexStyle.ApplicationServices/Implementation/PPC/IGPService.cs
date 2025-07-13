using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IGate;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class IGPService : IIGPService
    {
        private readonly IIGPRepository _repo;
        private readonly IIGPDetailRepository _igpDetailRepo;
        private readonly IGateTrRepository _gateTrRepo;
        private readonly IPartyService _partyService;
        public IGPService(IIGPRepository repo, IIGPDetailRepository iGPDetailRepository
            , IGateTrRepository gateTrRepo, IPartyService partyService) {
            _repo = repo;
            _igpDetailRepo = iGPDetailRepository;
            _gateTrRepo = gateTrRepo;
            _partyService = partyService;
        }

        public async Task<InwardGatePass> Create(InwardGatePass o)
        {
            try
            {
                // create yarn
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
        public async Task<InwardGatePass> CreateFabric(InwardGatePass o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                // create fabric because fabric branch id is 2
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

        public async Task<InwardGatePass> Delete(InwardGatePass o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<InwardGatePass>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false, nav => nav.InwardGatePassDetails, nav => nav.ActivityType);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public async Task<InwardGatePass> GetByXreffId(long id)
        {
            try
            {
                var ipg = await _repo.GetSingle(x => x.GateTrId == id && x.IsDeleted == false, nav => nav.InwardGatePassDetails);
                return ipg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InwardGatePass> GetById(long id) {
            try
            {
                var igp = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                    nav => nav.InwardGatePassDetails,
                    nav => nav.GateTr,
                    nav => nav.Party,
                    nav => nav.Buyer,
                    nav => nav.ActivityType);

                if (igp != null)
                {
                    igp.GateTr = await _gateTrRepo.GetSingle(x => x.Id == igp.GateTrId);

                    if (igp.InwardGatePassDetails != null)
                    {
                        await Task.WhenAll(igp.InwardGatePassDetails.Select(async b =>
                        {
                            var dtl = await _igpDetailRepo.GetSingle(x => x.Id == b.Id,
                                x => x.YarnType,
                                x => x.YarnManufacturer,
                                x => x.YarnQuality,
                                x => x.StoreLocation,
                                x => x.BagMarking,
                                x => x.ConeMarking,
                                x => x.FabricQuality,
                                x => x.FabricTypes,
                                x => x.RollMarking,
                                x => x.knittingParty,
                                x => x.ActivityType);

                            if (dtl != null)
                            {
                                b.ConeMarking = dtl.ConeMarking;
                                b.YarnType = dtl.YarnType;
                                b.YarnManufacturer = dtl.YarnManufacturer;
                                b.YarnQuality = dtl.YarnQuality;
                                b.StoreLocation = dtl.StoreLocation;
                                b.BagMarking = dtl.BagMarking;
                                b.FabricTypes = dtl.FabricTypes;
                                b.FabricQuality = dtl.FabricQuality;
                                b.RollMarking = dtl.RollMarking;
                                b.knittingParty = dtl.knittingParty;
                                b.ActivityType = dtl.ActivityType;
                            }
                        }));
                    }
                }

                return igp;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw ex;
            }
        }
        public async Task<InwardGatePass> Update(InwardGatePass o) {
            try {
                // update yarn user
                o.IsYarn = true;
                o.BranchId = 1;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<InwardGatePass> UpdateFabric(InwardGatePass o)
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

        public async Task<List<InwardGatePassDetail>> GetIgpReport(PagingOptions options) {
            var igpRep = await _igpDetailRepo.GetAllQueryable(x => x.InwardGatePass);
            Expression<Func<InwardGatePassDetail, bool>> predicate = null;
            predicate = x => x.IsDeleted == false;

            if (options.Offset.HasValue && options.Limit.HasValue) {
                igpRep = igpRep
                    .Skip(options.Offset.Value)
                    .Take(options.Limit.Value);
            }

            if (predicate != null) {
                igpRep.Where(predicate);
            }
            var res = await Task.FromResult(igpRep.ToList()); // ok sql you can run this command
            return res;
        }

        public async Task<List<InwardGatePass>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {

                var list = await _repo.GetList(x => x.IsDeleted == false && x.IgpDate.Date >= start.Date && x.IgpDate.Date <= end.Date && x.IsYarn == true && x.BranchId == 1, x => x.ActivityType, x => x.GateTr, x => x.Party);
                return list.ToList(); ;
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public async Task<List<InwardGatePass>> GetBetweenDateRangeFabric(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.IgpDate.Date >= start.Date && x.IgpDate.Date <= end.Date && x.IsYarn == false && x.BranchId == 2, x => x.ActivityType, x => x.GateTr, x => x.Party, x => x.Buyer);
                return list.ToList(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<InwardGatePass>> GetIgpRepYarnRecievedReport(ReportFilter filter) {
            try {

                Func<InwardGatePass, bool> igpPred = x => x.IsDeleted == false;

                if (filter.DateFrom.HasValue && filter.DateTo.HasValue) {
                    igpPred += (x => x.IsDeleted == false && x.IgpDate.Date >= filter.DateFrom.Value.Date && x.IgpDate.Date <= filter.DateTo.Value.Date);
                }

                if (filter.YarnPartyId.HasValue) {
                    igpPred += x => x.PartyId == filter.YarnPartyId;
                }

                var res = await _repo.GetList(igpPred,
                    nav => nav.Party,
                    nav => nav.InwardGatePassDetails,
                    nav => nav.ActivityType,
                    nav  =>nav.GateTr);

                await Task.WhenAll(res.Select(async b =>
                {
                    b.Party.Name = (await _partyService.GetById(Convert.ToInt64(b.PartyId)))?.Name;
                }));


                await Task.WhenAll(res.Select(igp =>
                {
                    if (igp.InwardGatePassDetails.Count > 0)
                    {
                        Func<InwardGatePassDetail, bool> predicate = x => x.IsDeleted == false;

                        if (filter.YarnQualityId.HasValue)
                        {
                            predicate += x => x.YarnQualityId == filter.YarnQualityId;
                        }

                        if (filter.YarnTypeId.HasValue)
                        {
                            predicate += x => x.YarnTypeId == filter.YarnTypeId;
                        }

                        igp.InwardGatePassDetails = igp.InwardGatePassDetails.Where(predicate).ToList();

                        return Task.WhenAll(igp.InwardGatePassDetails.Select(async b =>
                        {
                            var dtl = await _igpDetailRepo.GetSingle(x => x.Id == b.Id,
                                x => x.YarnType,
                                x => x.YarnManufacturer,
                                x => x.YarnQuality,
                                x => x.StoreLocation,
                                x => x.BagMarking,
                                x => x.ConeMarking, x => x.RollMarking,
                                x => x.FabricTypes,
                                x => x.FabricQuality,
                                x => x.knittingParty,
                                x => x.ActivityType);

                            b.ConeMarking = dtl.ConeMarking;
                            b.YarnType = dtl.YarnType;
                            b.YarnManufacturer = dtl.YarnManufacturer;
                            b.YarnQuality = dtl.YarnQuality;
                            b.StoreLocation = dtl.StoreLocation;
                            b.BagMarking = dtl.BagMarking;
                            b.RollMarking = dtl.RollMarking;
                            b.FabricQuality = dtl.FabricQuality;
                            b.FabricTypes = dtl.FabricTypes;
                            b.knittingParty = dtl.knittingParty;
                            b.ActivityType = dtl.ActivityType;
                        }));
                    }

                    return Task.CompletedTask;
                }));

                return res.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}
