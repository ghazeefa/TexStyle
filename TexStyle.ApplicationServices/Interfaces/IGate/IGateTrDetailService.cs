using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.Gate;
using TexStyle.Core.ReportsViewModel.CS.RateDetail;


namespace TexStyle.ApplicationServices.Interfaces.IGate
{
    public interface IGateTrDetailService : IDefaultService<GateTrDetail>
    {
        Task<bool> UpdateRates(List<RateDetail> rateDetail);
        Task<List<GateTrDetail>> GetByheaderId(long id);
    }
}
