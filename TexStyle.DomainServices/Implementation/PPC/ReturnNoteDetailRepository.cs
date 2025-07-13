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
    internal class ReturnNoteDetailRepository : Repository<ReturnNoteDetail>, IReturnNoteDetailRepository
    {
        private readonly AppDbContext _db;
        public ReturnNoteDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IList<ReturnNoteDetail>> GetAll()
        {
            //return base.GetAll(navigationProperties);
            return await _db.ReturnNoteDetails
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnType)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnQuality)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)

                 //.Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricTypes)
                 //.Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricQuality)

                 // .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
                 .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnQuality)
                                          .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.knittingParty)
                //.Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricTypes)
                //.Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricQuality)

                //  .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnType)
                // .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
                //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
                //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();
        }



        public override async Task<IList<ReturnNoteDetail>> GetAll(params Expression<Func<ReturnNoteDetail, object>>[] navigationProperties)
        {
            //return base.GetAll(navigationProperties);
            return await _db.ReturnNoteDetails
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnType)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnQuality)
                            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
                                          .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)
                                                        .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)

                 //.Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricTypes)
                 //.Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricQuality)

                 // .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
                 .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.BuyerColor)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnQuality)
                            .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.knittingParty)
                //.Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricTypes)
                //.Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricQuality)

                //  .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnType)
                // .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
                //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
                //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<ReturnNoteDetail> GetSingle()
        {
            return await Task.FromResult( _db.ReturnNoteDetails
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
                            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).Buyer)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnType)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnQuality)
                            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor)

                 //.Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricTypes)
                 //.Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricQuality)
                 //.Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)

                 .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.BuyerColor)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnQuality)

              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricTypes)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricQuality)
                                          .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.knittingParty)

               //  .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnType)
               // .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
               //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
               //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
               // .Where(x => x.IsDeleted == false);
               .AsNoTracking()
               .SingleOrDefault());

        }


        //public override ReturnNoteDetail GetSingle(Func<ReturnNoteDetail, bool> where, params Expression<Func<ReturnNoteDetail, object>>[] navigationProperties)
        //{
        //    return _db.ReturnNoteDetails
        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)
        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnType)
        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnQuality)

        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricTypes)
        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).FabricQuality)
        //      .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).PurchaseOrder)

        //         .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
        //      .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
        //      .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnType)
        //      .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.YarnQuality)

        //      .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricTypes)
        //      .Include(x => x.Reprocess).ThenInclude(id => (id as Reprocess).PPCPlanning).ThenInclude(y => y.FabricQuality)

        //         .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnType)
        //        .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
        //       .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
        //       .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
        //        .Where(x => x.IsDeleted == false);
        //       .AsNoTracking()
        //       .SingleOrDefault(where);

        //}

    }
}
