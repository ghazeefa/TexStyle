
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
    internal class FactoryPoService : IFactoryPoService
    {
        private IFactoryPoRepository _repo;
        private readonly IFactoryPoDetailRepository _igpDetailRepo;
        public FactoryPoService(IFactoryPoRepository facPoRepo,
            IFactoryPoDetailRepository iGPDetailRepository)
        {
            _repo = facPoRepo;
            _igpDetailRepo = iGPDetailRepository;
        }

        public async Task<FactoryPo> Create(FactoryPo o)
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

        public async Task<FactoryPo> Delete(FactoryPo o)
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

        public async Task<List<FactoryPo>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false,
                           nav => nav.FactoryPoDetail,
                         nav => nav.Buyer,
                         nav => nav.BuyerColor);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<FactoryPo>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FactoryPo> GetById(long id)
        {
            try
            {
                var igp = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                    nav => nav.FactoryPoDetail,
                    nav => nav.Buyer,
                    nav => nav.BuyerColor);

                if (igp != null)
                {
                    await Task.WhenAll(igp.FactoryPoDetail.Select(async b =>
                    {
                        var dtl = await _igpDetailRepo.GetSingle(x => x.Id == b.Id,
                            x => x.FabricQuality,
                            x => x.FabricTypes,
                            x => x.BuyerColor);

                        if (dtl != null)
                        {
                            b.FabricTypes = dtl.FabricTypes;
                            b.FabricQuality = dtl.FabricQuality;
                            b.BuyerColor = dtl.BuyerColor;
                        }
                    }));
                }

                return igp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FactoryPo> Update(FactoryPo o)
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

        public async Task<List<FactoryPo>> GetFactoryPoList()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.Po > 0);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
