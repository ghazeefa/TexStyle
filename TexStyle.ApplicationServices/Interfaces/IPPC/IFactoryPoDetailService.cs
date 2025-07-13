using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IFactoryPoDetailService : IDefaultService<FactoryPoDetail>
   
    {
        Task<long?> GetPoDetailId(long? factoryPo, long? buyerColorId, long? fabricTypeId);
    }
}
