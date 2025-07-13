using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IAnalysis;
using TexStyle.Core.Analysis;
using TexStyle.DomainServices.Interfaces.IAnalysis;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
   internal class DefectService:IDefectService
    {
        private IDefectRepository _repo;
        public DefectService(IDefectRepository activityTypeRepository)
        {
            _repo = activityTypeRepository;
        }

        public async Task<Defect> Create(Defect o)
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

        public async Task<Defect> Delete(Defect o)
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

        public async Task<List<Defect>> GetAll()
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
        public async Task<List<Defect>> GetBetweenDateRange(DateTime start, DateTime end)
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
        public async Task<Defect> GetById(long id)
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
        public async Task<Defect> Update(Defect o)
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