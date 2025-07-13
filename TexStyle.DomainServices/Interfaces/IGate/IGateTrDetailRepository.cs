using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.Gate;
using TexStyle.Core.ReportsViewModel.CS.RateDetail;


namespace TexStyle.DomainServices.Interfaces.IGate
{
   public interface IGateTrDetailRepository : IRepository<GateTrDetail>
    {
        Task<bool> UpdateRates(List<RateDetail> rateDetail);

    }
}
