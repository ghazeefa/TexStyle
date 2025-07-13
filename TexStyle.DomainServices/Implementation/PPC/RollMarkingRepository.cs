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
    internal class RollMarkingRepository : Repository<RollMarking>, IRollMarkingRepository
    {

        private readonly AppDbContext _db;
        public RollMarkingRepository(Infrastructure.AppDbContext db) : base(db)
        {
            _db = db;
        }




        public async Task<IList<RollMarking>> GetAll()
        {
            //return base.GetAll(navigationProperties);
            return await _db.RollMarkings
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
              .Include(x => x.PPCPlanning)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).InwardGatePassDetail)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)
                    .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricType)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)


                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();
        }



        public override async Task<IList<RollMarking>> GetAll(params Expression<Func<RollMarking, object>>[] navigationProperties)
        {

            return await _db.RollMarkings
            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
              .Include(x => x.PPCPlanning)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).InwardGatePassDetail)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Party)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)
                    .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricType)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)

              .Where(x => x.IsDeleted == false)
              .AsNoTracking()
              .ToListAsync();

        }


        public async Task<RollMarking> GetSingle()
        {
            return await Task.FromResult( _db.RollMarkings
                            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
              .Include(x => x.PPCPlanning)
                       .Include(x => x.RollMarkingDetails)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).InwardGatePassDetail)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Party)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)
                    .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricType)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)

               .AsNoTracking().Where(x => x.IsDeleted == false)
               .SingleOrDefault());
        }


        public override async Task<RollMarking> GetSingle(Func<RollMarking, bool> where, params Expression<Func<RollMarking, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.RollMarkings
                            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
              .Include(x => x.PPCPlanning)
              .Include(x=> x.RollMarkingDetails)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).InwardGatePassDetail)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Party)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)
                    .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricType)

              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
               .AsNoTracking()
               .SingleOrDefault(where));
        }





        //public override IList<RollMarking> GetList();
        //{


        //var list = _db.RollMarkings
        //    .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
        //      .Include(x => x.PPCPlanning)

        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).InwardGatePassDetail)
        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)

        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
        //       .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)
        //       .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)
        //            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricType)

        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)

        //      .Where(x => x.IsDeleted == false);

        //    return list.AsNoTracking().ToList();













    }
}
