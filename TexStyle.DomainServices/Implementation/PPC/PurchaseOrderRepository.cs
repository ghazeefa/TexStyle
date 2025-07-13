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
    class PurchaseOrderRepository : Repository<PurchaseOrder> , IPurchaseOrderRepository
    {
        private readonly AppDbContext _db;

        public PurchaseOrderRepository(Infrastructure.AppDbContext db):base(db)
        {
            _db = db;

        }

        public async Task<IList<PurchaseOrder>> GetAll()
        {
            var list = _db.PurchaseOrders
                .Include(x => x.BuyerColor).ThenInclude(bc => bc.Buyer).ThenInclude(b => b.Party)
                .Include(x => x.Season)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)
                 .Include(x => x.FabricTypes)
                .Include(x => x.FabricQuality)

                .Where(x => x.IsDeleted == false);

            return await Task.FromResult(list.AsNoTracking().ToList());
        }

        public override async Task<IList<PurchaseOrder>> GetAll(params Expression<Func<PurchaseOrder, object>>[] navigationProperties)
        {
            var list = await _db.PurchaseOrders
                .Include(x => x.BuyerColor).ThenInclude(bc => bc.Buyer).ThenInclude(b => b.Party)
                .Include(x => x.Season)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)
                .Include(x => x.FabricTypes)
                .Include(x => x.FabricQuality)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }


        public async Task<PurchaseOrder> GetSingle()
        {
            return await Task.FromResult( _db.PurchaseOrders
                .Include(x => x.BuyerColor).ThenInclude(bc => bc.Buyer).ThenInclude(b => b.Party)
                .Include(x => x.Season)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)
                 .Include(x => x.FabricTypes)
                .Include(x => x.FabricQuality)
                .AsNoTracking().Where(x=>x.IsDeleted == false)
                .SingleOrDefault());
        }

        public override async Task<PurchaseOrder> GetSingle(Func<PurchaseOrder, bool> where, params Expression<Func<PurchaseOrder, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.PurchaseOrders
                .Include(x => x.BuyerColor).ThenInclude(bc => bc.Buyer).ThenInclude(b => b.Party)
                .Include(x => x.Season)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)
                 .Include(x => x.FabricTypes)
                .Include(x => x.FabricQuality)
                .AsNoTracking()
                .SingleOrDefault(where));
        }
        public override async Task<IList<PurchaseOrder>> GetList(Func<PurchaseOrder, bool> where, params Expression<Func<PurchaseOrder, object>>[] navigationProperties)
        {
            var list = _db.PurchaseOrders
                .Include(x => x.BuyerColor).ThenInclude(bc => bc.Buyer).ThenInclude(b => b.Party)
                .Include(x => x.Season)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)
                 .Include(x => x.FabricTypes)
                .Include(x => x.FabricQuality)
                   .Where(where);
            return await Task.FromResult(list.ToList());
        }
        public async Task<IList<PurchaseOrder>> GetList()
        {
            var list = _db.PurchaseOrders
                .Include(x => x.BuyerColor).ThenInclude(bc => bc.Buyer).ThenInclude(b => b.Party)
                .Include(x => x.Season)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)
                 .Include(x => x.FabricTypes)
                .Include(x => x.FabricQuality)
                .ToList();
                 //  .Where(where);
            return await Task.FromResult(list.ToList());
        }
    }
}
