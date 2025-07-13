
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IGate;
using TexStyle.Core.Gate;
using TexStyle.DomainServices.Interfaces.IGate;

namespace TexStyle.ApplicationServices.Implementation.Gate
{
    class GatePassTypeService : IGatePassTypeService
    {
        private IGatePassTypeRepository _repo;
        public GatePassTypeService(IGatePassTypeRepository repo)
        {
            _repo = repo;
        }
        public async Task<GatePassType> Create(GatePassType o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GatePassType> Delete(GatePassType o)
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

        public async Task<List<GatePassType>> GetAll()
        {
            try
            {
                var list=await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public async Task<List<GatePassType>> GetBetweenDateRange(DateTime start, DateTime end)
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

        public async Task<GatePassType> GetById(long id)
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

        public async Task<GatePassType> GetByName(string name)
        {
            try
            {
                return await _repo.GetSingle(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GatePassType> Update(GatePassType o)
        {
            try
            {
                o.UpdatedOn = DateTime.Now;
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

