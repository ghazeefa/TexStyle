using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
   public interface IPurchaseOrderService :IDefaultService<PurchaseOrder>
    {
        Task<PurchaseOrder> CreateFabric(PurchaseOrder o);
        Task<List<PurchaseOrder>> GetBetweenDateRangeFabric(DateTime start, DateTime end);
        Task<List<PurchaseOrder>> GetCommercialPo();
    }
}
