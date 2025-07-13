using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

using TexStyle.Core.ReportsViewModel.PPC;

namespace TexStyle.DomainServices.Interfaces.IPPC
{
    public interface IOGPRepository : IRepository<OutwardGatePass>
    {

        Task<List<GetbteweenRange_OGPRepositoryViewModel_P8>> GetbteweenRange_OGPRepositoryViewModel_P8(DateTime start, DateTime end, long userid);

    }
}
