using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IIssueNoteDetailService : IDefaultService<IssueNoteDetail>
    {
        Task<IssueNoteDetail> GetByIGPNO(long id);
    }
}
