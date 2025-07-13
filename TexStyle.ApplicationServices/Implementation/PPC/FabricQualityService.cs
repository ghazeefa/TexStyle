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
    internal class FabricQualityService : IFabricQualityService
    {

        private IFabricQualityRepository _repo;
        public FabricQualityService(IFabricQualityRepository repo)
        {
            _repo = repo;
        }

        public async Task<FabricQuality> Create(FabricQuality o)
        {
            try
            {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FabricQuality> Delete(FabricQuality o)
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

        public async Task<List<FabricQuality>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<FabricQuality>> GetBetweenDateRange(DateTime start, DateTime end)
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

        public async Task<FabricQuality> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FabricQuality> Update(FabricQuality o)
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
