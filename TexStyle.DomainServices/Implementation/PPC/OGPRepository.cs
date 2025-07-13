using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    internal class OGPRepository : Repository<OutwardGatePass>, IOGPRepository
    {
        private readonly AppDbContext _db;

        public OGPRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IList<OutwardGatePass>> GetList()
        {
            return await Task.FromResult( _db.OutwardGatePasses
               .Include(x => x.ActivityType)
               .Include(x => x.YarnType)
               .Include(x => x.Party)
                    .Include(x => x.Buyer)
                .Include(x => x.FabricTypes)
               .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.OutwardGatePass)
               //.Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.ReturnNote)
               .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.YarnType)
                              .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.FabricTypes)
               .Where(x => x.IsDeleted == false)
               .AsNoTracking().ToList());

        }


        public override async Task<IList<OutwardGatePass>> GetList(Func<OutwardGatePass, bool> where, params Expression<Func<OutwardGatePass, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.OutwardGatePasses
               .Include(x => x.ActivityType)
               .Include(x => x.YarnType)
               .Include(x => x.Party)
                .Include(x => x.FabricTypes)
                .Include(x => x.Buyer)
               .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.OutwardGatePass)
                //.Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.ReturnNote)
                       .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.FabricTypes)
               .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.YarnType)
               .Where(where).ToList());
            //      .Where(x => x.IsYarn==false)
            //.AsNoTracking().ToList();

        }




        //public override IList<OutwardGatePass> GetList(Func<OutwardGatePass, bool> where, params Expression<Func<OutwardGatePass, object>>[] navigationProperties)
        //{
        //    return _db.OutwardGatePasses
        //        .Include(x => x.ActivityType)
        //         .Include(x => x.YarnType)
        //        .Include(x => x.Party)
        //        .Include(x => x.FabricTypes)
        //         .Where(where).ToList();
        //}




        public async Task<OutwardGatePass> GetSingle()
        {
            return await Task.FromResult( _db.OutwardGatePasses
                .Include(x => x.ActivityType)
                .Include(x => x.YarnType)
                .Include(x => x.Party)
                     .Include(x => x.Buyer)
                .Include(x => x.FabricTypes)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.OutwardGatePass)
                //.Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.ReturnNote)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.YarnType)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.FabricTypes)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.PPCPlanning)
                .AsNoTracking().Where(x=>x.IsDeleted ==false)
                .SingleOrDefault());
        }


        public override async Task<OutwardGatePass> GetSingle(Func<OutwardGatePass, bool> where, params Expression<Func<OutwardGatePass, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.OutwardGatePasses
                .Include(x => x.ActivityType)
                .Include(x => x.YarnType)
                .Include(x => x.Party)
                .Include(x => x.Buyer)
                .Include(x => x.FabricTypes)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.OutwardGatePass)
                //.Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.ReturnNote)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.YarnType)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.FabricTypes)
                .Include(x => x.OutwardGatePassDetails).ThenInclude((OutwardGatePassDetail x) => x.PPCPlanning)
                .AsNoTracking()
                .SingleOrDefault(where));
        }


        //.AsNoTracking()
        //.SingleOrDefault(where);


        public async Task<List<GetbteweenRange_OGPRepositoryViewModel_P8>> GetbteweenRange_OGPRepositoryViewModel_P8(DateTime start, DateTime end, long userid)
        {
            return await Task.FromResult( _db.GetbteweenRange_OGPRepositoryViewModel_P8.FromSql($"usp_GetbteweenRangeOGP_P8 @start = {start}, @end = {end}, @userid={userid}").ToList());
        }








    }
}
