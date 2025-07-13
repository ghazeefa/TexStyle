using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IIGPDetailService : IDefaultService<InwardGatePassDetail>
    {
        Task<InwardGatePassDetail> GetById(long id,long sno);
        Task<List<InwardGatePassDetail>> GetByHeaderId(long id);
    }
}
