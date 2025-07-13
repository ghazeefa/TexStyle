using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface ILotMarkingService: IDefaultService<LotMarking>
    {
         Task<LotMarking> GetByLPSNo(long id);
    }
}
