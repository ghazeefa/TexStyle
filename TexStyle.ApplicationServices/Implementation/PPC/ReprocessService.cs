using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    internal class ReprocessService : IReprocessService
    {
        private readonly IReprocessRepository _repo;
        public ReprocessService(IReprocessRepository repo)
        {
            _repo = repo;
        }

        public async Task<Reprocess> Create(Reprocess o)
        {
            //create yarn type user
            try
            {
                o.CreatedOn = DateTime.Now;
             
                o.IsYarn = true;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Reprocess> CreateFabric(Reprocess o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                o.IsYarn = false;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Reprocess> Delete(Reprocess o)
        {
            try
            {
                //var found = _repo.GetSingle(x => x.Id ==o.Id);
                //found.IsDeleted = true;
                o.IsDeleted = true;
                await _repo.Update(o);
                //UpdateCount(o.PPCPlanningId);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Reprocess>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false, x => x.PPCPlanning);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Reprocess>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Reprocess> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, x => x.PPCPlanning);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Reprocess>> GetReprocessesByLpsId(long id)
        {
            try
            {
                var list = await _repo.GetList(x => x.PPCPlanningId == id && x.IsDeleted == false,
                    x => x.PPCPlanning.BuyerColor.Buyer.Party,
                    x => x.PPCPlanning.ProductionType,
                    x => x.PPCPlanning.YarnType,
                    x => x.PPCPlanning.YarnQuality,
                    x => x.PPCPlanning.FabricType,
                    x => x.PPCPlanning.FabricQuality,
                    x => x.PPCPlanning.knittingParty
                    );

               //var res = _repo.GetList(x=>x.PPCPlanningid==id, x => x.ppcplanning).tolist();
                return list.ToList();
                //Func<Reprocess,bool> pred = x => x.IsDeleted == false && x.PPCPlanningId == id;
                //if(getAll ==true) {
                //    pred = x => x.PPCPlanningId == id;
                //}
                //var res = _repo.GetList(pred,x => x.PPCPlanning).ToList();
                //return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Reprocess> Update(Reprocess o)
        {
            try
            {
                var found = await _repo.GetSingle(x => x.Id == o.Id);
                found.Cones = o.Cones;
               // found.Count = o.Count;
                found.Date = o.Date;
                found.Kgs = o.Kgs;
                found.IsYarn = true;
                // found.ReworkActivityId = o.ReworkActivityId;
                found.IsDeleted = o.IsDeleted; 
                found.PPCPlanningId = o.PPCPlanningId;
                await _repo.Update(found);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Reprocess> UpdateFabric(Reprocess o)
        {
            try
            {
                var found = await _repo.GetSingle(x => x.Id == o.Id);
                found.Cones = o.Cones;
                // found.Count = o.Count;
                found.Date = o.Date;
                found.Kgs = o.Kgs;
                found.IsYarn = false;
                // found.ReworkActivityId = o.ReworkActivityId;
                found.IsDeleted = o.IsDeleted;
                found.PPCPlanningId = o.PPCPlanningId;
                await _repo.Update(found);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        //public bool UpdateCount(long? ppcid)
        //{
        //    try
        //    {
        //        List<Reprocess> data = _repo.GetList(x => x.PPCPlanningId == ppcid.Value && x.IsDeleted == false).OrderBy(x => x.Id).ToList();
        //        int count = 1;
        //        foreach (Reprocess d in data)
        //        {
        //            d.Count = count;
        //            count++;
        //            Update(d);
        //        }
        //        // make db call only once
        //        //_repo.Update(data.ToArray());

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
