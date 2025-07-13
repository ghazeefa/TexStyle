using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.YD;

namespace TexStyle.ApplicationServices.Interfaces.IYD
{
    public interface IRecipeDetailService : IDefaultService<RecipeDetail>
    {
       Task<IList<RecipeDetail>> DeletebyId(long recipeno);
       // IList<RecipeDetail> GetByRecNo(long id);
    }
}
