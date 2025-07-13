using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.YD;

namespace TexStyle.ApplicationServices.Interfaces.IYD
{
    public interface IRecipeLPSService : IDefaultService<RecipeLPS>
    {
        Task<decimal> GetWeightByRecNo(long id, bool isreprocess);
        Task<decimal> GetPcsByRecNo(long id, bool isreprocess);
        Task<int> GetConsByRecNo(long id, bool isreprocess);
        //int GetByLps(long id);
        Task<RecipeLPS> GetByLps(long id);
        Task<List<RecipeLPS>> GelListByLps(long? id);
    }
}
