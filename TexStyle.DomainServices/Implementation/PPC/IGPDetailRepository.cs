using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    internal class IGPDetailRepository : Repository<InwardGatePassDetail>, IIGPDetailRepository
    {   
        private AppDbContext _db;
        public IGPDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
         }

        public async Task<IList<InwardGatePassDetail>> GetAll()
        {
            return await _db.InwardGatePassDetails

                .Include(x => x.InwardGatePass).ThenInclude(x => x.Party)
                 .Include(x => x.InwardGatePass).ThenInclude(x => x.Buyer)
                .Include(x => x.YarnType)
                 .Include(x => x.YarnManufacturer)
                  .Include(x => x.YarnQuality)
                  .Include(x => x.StoreLocation)
                  .Include(x => x.BagMarking)
                  .Include(x => x.ConeMarking)
                   .Include(x => x.FabricTypes)
                    .Include(x => x.FabricQuality)
                     .Include(x => x.RollMarking)
                      .Include(x => x.knittingParty)
                      .Include(x => x.ActivityType)
                //.Include(x=>x.Weight)
                //.Include(x=>x.GSM)
                //.Include(x=>x.Dia)


                .Where(x => x.IsDeleted == false).AsNoTracking().ToListAsync();
        }



        //public override IList<InwardGatePassDetail> GetAll(params Expression<Func<InwardGatePassDetail, object>>[] navigationProperties)
        //{
        //    return _db.InwardGatePassDetails

        //        .Include(x => x.InwardGatePass).ThenInclude(x => x.Party)
        //        .Include(x => x.YarnType)
        //         .Include(x => x.YarnManufacturer)
        //          .Include( x => x.YarnQuality)
        //          .Include( x => x.StoreLocation) 
        //          .Include( x => x.BagMarking) 
        //          .Include( x => x.ConeMarking)
        //           .Include(x => x.FabricTypes)
        //            .Include(x => x.FabricQuality)
        //             .Include(x => x.RollMarking)
        //              .Include(x => x.knittingParty)
        //              .Include(x => x.ActivityType)
        //              //.Include(x=>x.Weight)
        //              //.Include(x=>x.GSM)
        //              //.Include(x=>x.Dia)


        //        .Where(x => x.IsDeleted == false).AsNoTracking().ToList();
        //}


        public async Task<InwardGatePassDetail> GetSingle()
        {
            return await Task.FromResult( _db.InwardGatePassDetails
                 .Include(x => x.InwardGatePass).ThenInclude(x => x.Buyer)
              .Include(x => x.InwardGatePass).ThenInclude(x => x.Party)
               .Include(x => x.YarnType)
                 .Include(x => x.YarnManufacturer)
                  .Include(x => x.YarnQuality)
                  .Include(x => x.StoreLocation)
                  .Include(x => x.BagMarking)
                  .Include(x => x.ConeMarking)
                     .Include(x => x.FabricTypes)
                    .Include(x => x.FabricQuality)
                     .Include(x => x.RollMarking)
                      .Include(x => x.knittingParty)
                      .Include(x => x.ActivityType)
              //.Include(x => x.Weight)
              //.Include(x => x.GSM)
              //.Include(x => x.Dia)

              .Where(x => x.IsDeleted == false)
              .AsNoTracking()
                .SingleOrDefault());

        }



        //public override InwardGatePassDetail GetSingle(Func<InwardGatePassDetail, bool> where, params Expression<Func<InwardGatePassDetail, object>>[] navigationProperties)
        //{
        //    return _db.InwardGatePassDetails

        //      .Include(x => x.InwardGatePass).ThenInclude(x => x.Party)
        //       .Include(x => x.YarnType)
        //         .Include(x => x.YarnManufacturer)
        //          .Include(x => x.YarnQuality)
        //          .Include(x => x.StoreLocation)
        //          .Include(x => x.BagMarking)
        //          .Include(x => x.ConeMarking)
        //             .Include(x => x.FabricTypes)
        //            .Include(x => x.FabricQuality)
        //             .Include(x => x.RollMarking)
        //              .Include(x => x.knittingParty) 
        //              .Include(x => x.ActivityType)
        //              //.Include(x => x.Weight)
        //              //.Include(x => x.GSM)
        //              //.Include(x => x.Dia)

        //      .Where(x => x.IsDeleted == false)
        //      .AsNoTracking()
        //        .SingleOrDefault(where);

        //}
    }
}
