//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace TexStyle.ApplicationServices.Implementation.PPC
//{
//    class FactoryPoDetailService
//    {
//    }
//}

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
    internal class FactoryPoDetailService : IFactoryPoDetailService
    {
        private IFactoryPoDetailRepository _repo;
        public FactoryPoDetailService(IFactoryPoDetailRepository facPoRepo)
        {
            _repo = facPoRepo;
        }

        public async Task<FactoryPoDetail> Create(FactoryPoDetail o)
        {
            try
            {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                // TODO: need to add custom exception here
                throw ex;
            }
        }

        public async Task<FactoryPoDetail> Delete(FactoryPoDetail o)
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

        public async Task<List<FactoryPoDetail>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false,
                             nav => nav.FabricQuality,
                    nav => nav.FabricTypes);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<FactoryPoDetail>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date,
                           nav => nav.FabricQuality,
                    nav => nav.FabricTypes);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FactoryPoDetail> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                           nav => nav.FabricQuality,
                    nav => nav.FabricTypes);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<long?> GetPoDetailId(long? factoryPo, long? buyerColorId, long? fabricTypeId)
        {
            try
            {
               return await _repo.GetPoDetailId(factoryPo, buyerColorId, fabricTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FactoryPoDetail> Update(FactoryPoDetail o)
        {
            try
            {
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
