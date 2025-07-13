
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    class PurchaseOrderService : IPurchaseOrderService
    {
        private IPurchaseOrderRepository _repo;
        public PurchaseOrderService(IPurchaseOrderRepository repo)
        {
            _repo = repo;
        }

     
        public async Task<PurchaseOrder> Create(PurchaseOrder o)
        {
            try
            {
                // create yarn type user 
                o.CreatedOn = DateTime.Now;
                o.BranchId = 1;
                o.IsYarn = true;
                await _repo.Add(o);

                return o;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PurchaseOrder> CreateFabric(PurchaseOrder o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                o.BranchId = 2;
                o.IsYarn = false;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PurchaseOrder> Delete(PurchaseOrder o)
        {
            try
            {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<PurchaseOrder>> GetAll()
        {
            try
            {
                // this is done to include navigation properties
                var list = await _repo.GetAll();
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PurchaseOrder>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date && x.IsYarn == true && x.BranchId == 1);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<PurchaseOrder>> GetBetweenDateRangeFabric(DateTime start, DateTime end) {
            try {var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date && x.IsYarn == false && x.BranchId == 2);
                return list .ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<PurchaseOrder> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x=> x.Id == id && x.IsDeleted == false);
                 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PurchaseOrder> Update(PurchaseOrder o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                o.BranchId = 2;
                o.IsYarn = false;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<PurchaseOrder>> GetCommercialPo()
        {
            try
            {
                var list = await _repo.GetAll();

                return list.Where(x => x.BuyerColor.Buyer.Party.IsCommercial == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
