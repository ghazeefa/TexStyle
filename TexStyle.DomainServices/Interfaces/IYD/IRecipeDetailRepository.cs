using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.YD;

namespace TexStyle.DomainServices.Interfaces.IYD
{
    public interface IRecipeDetailRepository : IRepository<RecipeDetail>
    {
        Task<RecipeDetail> GetSingleById(long id);
    }
}
