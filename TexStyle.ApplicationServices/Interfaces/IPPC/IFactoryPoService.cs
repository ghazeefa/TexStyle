using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
namespace TexStyle.ApplicationServices.Interfaces.IPPC
{

   public interface IFactoryPoService : IDefaultService<FactoryPo>
    {
        Task<List<FactoryPo>> GetFactoryPoList();
    }
}



   