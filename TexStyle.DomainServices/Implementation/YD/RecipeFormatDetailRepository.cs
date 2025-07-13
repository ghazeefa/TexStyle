using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD
{
    class RecipeFormatDetailRepository :Repository<RecipeFormatDetail>, IRecipeFormatDetailRepository
    {
        public RecipeFormatDetailRepository(AppDbContext db) : base(db)
        {
        }
    }
}
