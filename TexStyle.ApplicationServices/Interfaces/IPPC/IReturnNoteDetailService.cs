using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IReturnNoteDetailService : IDefaultService<ReturnNoteDetail>
    {
        Task<IList<ReturnNoteDetail>> GetByXreffId(long id);


    }
}
