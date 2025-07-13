using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.YD;

namespace TexStyle.ApplicationServices.Interfaces.IYD
{
 public  interface IRecipeFormatHeaderService:IDefaultService<RecipeFormatHeader>
    {
        Task<List<RecipeFormatHeader>> GetAllFabric();
        Task<List<RecipeFormatHeader>> GetAllYarn();
    }
}
