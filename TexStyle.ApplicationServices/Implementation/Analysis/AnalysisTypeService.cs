using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IAnalysis;
using TexStyle.Core.Analysis;
using TexStyle.DomainServices.Interfaces.IAnalysis;

namespace TexStyle.ApplicationServices.Implementation.Analysis
{
    internal class AnalysisTypeService:IAnalysisTypeService
    {
        private IAnalysisTypeRepository _repo;
        public AnalysisTypeService(IAnalysisTypeRepository activityTypeRepository)
        {
            _repo = activityTypeRepository;
        }

        public async Task<AnalysisType> Create(AnalysisType o)
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
        public async Task<AnalysisType> Delete(AnalysisType o)
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
        public async Task<List<AnalysisType>> GetAll()
        {
            try
            {
                return (await _repo.GetList(x => x.IsDeleted == false)).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AnalysisType>> GetBetweenDateRange(DateTime start, DateTime end)
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
        public async Task<AnalysisType> GetById(long id)
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
        public async Task<AnalysisType> Update(AnalysisType o)
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
