using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    class ActivityTypeService : IActivityTypeService
    {
        private IActivityTypeRepository _repo;
        public ActivityTypeService(IActivityTypeRepository activityTypeRepository)
        {
            _repo = activityTypeRepository;
        }

        public async Task<ActivityType> Create(ActivityType o)
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

        public async Task<ActivityType> Delete(ActivityType o)
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

        public async Task<List<ActivityType>> GetAll()
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

        public async Task<List<ActivityType>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<ActivityType> GetById(long id)
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

        public async Task<ActivityType> Update(ActivityType o)
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
