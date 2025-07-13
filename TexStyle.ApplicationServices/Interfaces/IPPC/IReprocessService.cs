using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC {
    public interface IReprocessService : IDefaultService<Reprocess> {
        Task<List<Reprocess>> GetReprocessesByLpsId(long id);

        Task<Reprocess> CreateFabric(Reprocess o);
        Task<Reprocess> UpdateFabric(Reprocess o);
        //bool UpdateCount(long? ppcid);
    }
}
