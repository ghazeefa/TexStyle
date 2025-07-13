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
    internal class FactoryPoDetailRepository : Repository<FactoryPoDetail>, IFactoryPoDetailRepository
    {
        private AppDbContext _db;
        public FactoryPoDetailRepository(AppDbContext appDbContext) :
            base(appDbContext)
        {
            _db = appDbContext;
        }

        public async Task<long?> GetPoDetailId(long? factoryPo, long? buyerColorId, long? fabricTypeId)
        {
            var result = await (from fpo in _db.FactoryPo
                                join factoryPoDetail in _db.FactoryPoDetail
                                on fpo.Id equals factoryPoDetail.FactoryPoId
                                where fpo.Po == factoryPo 
                                && factoryPoDetail.BuyerColorId == buyerColorId
                                && factoryPoDetail.FabricTypesId == fabricTypeId
                                select factoryPoDetail.Id)
                                .FirstOrDefaultAsync();
            return result;
        }

        //public IList<FactoryPoDetail> GetAll()
        //{
        //    return _db.FactoryPoDetail

        //         .Include(x => x.FactoryPo).ThenInclude(x => x.Buyer)
        //           .Include(x => x.FabricTypes)
        //            .Include(x => x.FabricQuality)
        //        .Where(x => x.IsDeleted == false).AsNoTracking().ToList();
        //}



        //public FactoryPoDetail GetSingle()
        //{
        //    return _db.FactoryPoDetail
        //       .Include(x => x.FactoryPo).ThenInclude(x => x.Buyer)
        //    .Include(x => x.FabricTypes)
        //       .Include(x => x.FabricQuality)
        //      .Where(x => x.IsDeleted == false)
        //      .AsNoTracking()
        //        .SingleOrDefault();

        //}




        public async Task<FactoryPoDetail> GetSingle()
        {
            return await Task.FromResult( _db.FactoryPoDetail

                     .Include(x => x.FabricTypes)
                    .Include(x => x.FabricQuality)
                     .Include(x => x.BuyerColor)
                      
              //.Include(x => x.Weight)
              //.Include(x => x.GSM)
              //.Include(x => x.Dia)

              .Where(x => x.IsDeleted == false)
              .AsNoTracking()
                .SingleOrDefault());

        }


    }

}

