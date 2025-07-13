using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD
{
    class RecipeFormatHeaderRepository:Repository<RecipeFormatHeader> , IRecipeFormatHeaderRepository
    {
        public readonly AppDbContext _db;
        public RecipeFormatHeaderRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<RecipeFormatHeader> GetSingle()
        {
            return await Task.FromResult( _db.RecipeFormatHeaders
                .Include(x => x.RecipeFormatDetails).ThenInclude(id => (id as RecipeFormatDetail).Dye).Where(x => x.IsDeleted == false)
               .Include(x => x.RecipeFormatDetails).ThenInclude(id => (id as RecipeFormatDetail).RecipeStep).Where(x => x.IsDeleted == false)
               .Include(x => x.RecipeFormatDetails).ThenInclude(id => (id as RecipeFormatDetail).Chemical).Where(x => x.IsDeleted == false)
                .AsNoTracking().ToList()
               .SingleOrDefault());
        }

        public override async Task<RecipeFormatHeader> GetSingle(Func<RecipeFormatHeader, bool> where, params Expression<Func<RecipeFormatHeader, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.RecipeFormatHeaders
                .Include(x => x.RecipeFormatDetails).ThenInclude(id => (id as RecipeFormatDetail).Dye).Where(x => x.IsDeleted == false)
               .Include(x => x.RecipeFormatDetails).ThenInclude(id => (id as RecipeFormatDetail).RecipeStep).Where(x => x.IsDeleted == false)
               .Include(x => x.RecipeFormatDetails).ThenInclude(id => (id as RecipeFormatDetail).Chemical).Where(x => x.IsDeleted == false)
                .AsNoTracking().ToList()
               .SingleOrDefault(where));
        }

    }
}
