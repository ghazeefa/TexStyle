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
    internal class ReturnNoteRepository : Repository<ReturnNote>, IReturnNoteRepository
    {

        private readonly AppDbContext _db;
        public ReturnNoteRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IList<ReturnNote>> GetAll()
        {
            //return base.GetAll(navigationProperties);
            return await _db.ReturnNotes
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricTypes)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Buyer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.BuyerColor)


              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

                //  .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnType)
                // .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
                //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
                .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();

        }


        public override async Task<IList<ReturnNote>> GetAll(params Expression<Func<ReturnNote, object>>[] navigationProperties)
        {
            //return base.GetAll(navigationProperties);
            return await  _db.ReturnNotes
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricQuality)
                .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Buyer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.BuyerColor)

              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

                //  .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnType)
                // .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
                //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
                .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();

    }

    public async Task<ReturnNote> GetSingle()
        {
            return await Task.FromResult( _db.ReturnNotes
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricQuality)
                .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Buyer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.BuyerColor)

              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

               //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
               //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
               .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
               .AsNoTracking().Where(x=>x.IsDeleted==false)
               .SingleOrDefault());
        }


        public override async Task<ReturnNote> GetSingle(Func<ReturnNote, bool> where, params Expression<Func<ReturnNote, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.ReturnNotes
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.FabricQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.Buyer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.BuyerColor)

              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricType)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.FabricQuality)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.Party)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.PurchaseOrder)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

               //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnQuality)
               //.Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetails).YarnManufacturer)
               .Include(x => x.ReturnNoteDetails).ThenInclude(id => (id as ReturnNoteDetail).StoreLocation)
               .AsNoTracking()
               .SingleOrDefault(where));
        }

        public override async Task<IList<ReturnNote>> GetList(Func<ReturnNote, bool> where, params Expression<Func<ReturnNote, object>>[] navigationProperties)
        {
            return await Task.FromResult(_db.ReturnNotes

                .Include(x => x.Buyer)
               .Where(where).ToList());
        }


    }
}
