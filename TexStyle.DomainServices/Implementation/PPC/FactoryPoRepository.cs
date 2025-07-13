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
    internal class FactoryPoRepository : Repository<FactoryPo>, IFactoryPoRepository
    {
         private AppDbContext _db;
        public FactoryPoRepository(AppDbContext appDbContext) :
            base(appDbContext)
        {
            _db = appDbContext;

        }

        public async Task<IList<FactoryPo>> GetAll()
        {
            return await _db.FactoryPo

                .Include(x => x.Buyer)
                .Include(x => x.BuyerColor)
                .Where(x => x.IsDeleted == false).AsNoTracking().ToListAsync();

        }


        public async Task<FactoryPo> GetSingle()
        {
            return await Task.FromResult( _db.FactoryPo
                              // .Include(x => x.FactoryPoDetail)

               .Include(x => x.FactoryPoDetail).ThenInclude(z => z.BuyerColor)
               .Include(x => x.FactoryPoDetail).ThenInclude(z => z.FabricQuality)
               .Include(x => x.FactoryPoDetail).ThenInclude(z => z.FabricTypes)
              .Where(x => x.IsDeleted == false)
              .AsNoTracking()
                .SingleOrDefault());

        }

    }
}
