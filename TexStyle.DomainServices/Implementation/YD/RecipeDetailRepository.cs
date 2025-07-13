using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD
{
    internal class RecipeDetailRepository : Repository<RecipeDetail>, IRecipeDetailRepository
    {
        private AppDbContext _db;
        public RecipeDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<RecipeDetail> GetSingleById(long id)
        {
            var res = await Task.FromResult(_db.RecipeDetails
                .Include(x => x.Recipe)
                .Include(x => x.RecipeStep)
                .Include(x => x.Dye)
                .Include(x => x.Chemical)
                .Where(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefault());

            return res;
        }
    }
}
