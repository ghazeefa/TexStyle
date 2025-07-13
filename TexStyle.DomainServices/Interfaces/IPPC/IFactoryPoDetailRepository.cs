using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
namespace TexStyle.DomainServices.Interfaces.IPPC
{
    public interface IFactoryPoDetailRepository : IRepository<FactoryPoDetail>
    {
        Task<long?> GetPoDetailId(long? factoryPo, long? buyerColorId, long? fabricTypeId);
    }
}
