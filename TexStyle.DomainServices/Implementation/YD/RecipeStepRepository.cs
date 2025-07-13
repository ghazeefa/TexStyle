using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD
{
    internal class RecipeStepRepository : Repository<RecipeStep>, IRecipeStepRepository
    {
        public RecipeStepRepository(AppDbContext db) : base(db)
        {
        }
    }
}
