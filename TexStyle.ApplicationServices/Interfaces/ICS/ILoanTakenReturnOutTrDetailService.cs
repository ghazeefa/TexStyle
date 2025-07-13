using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;

namespace TexStyle.ApplicationServices.Interfaces.ICS
{
    public interface ILoanTakenReturnOutTrDetailService:IDefaultService<LoanTakenReturnOutTrDetail>
    {
         decimal GetUsedKgLoanTaken(long id);
    }
}
