using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IOGPService : IDefaultService<OutwardGatePass>
    {

        Task<List<OutwardGatePass>> GetBetweenDateRangeFabric(DateTime start, DateTime end);
        Task<OutwardGatePass> UpdateFabric(OutwardGatePass o);
        Task<List<OutwardGatePassDetail>> GetOgpReport(PagingOptions options);
        Task<OutwardGatePass> CreateFabric(OutwardGatePass o);


        //List<GetbteweenRange_OGPRepositoryViewModel_P8> GetbteweenRange_OGP(DateTime start, DateTime end, long userid);
        //OutwardGatePass UpdateFabric(OutwardGatePass o);
    }
}
