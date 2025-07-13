using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IAnalysis;
using TexStyle.Core.Analysis;
using TexStyle.DomainServices.Interfaces.IAnalysis;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
   internal class DefectAnalysisService:IDefectAnalysisService
    {
        private IDefectAnalysisRepository _repo;
        public DefectAnalysisService(IDefectAnalysisRepository activityTypeRepository)
        {
            _repo = activityTypeRepository;
        }

        public async Task<DefectAnalysis> Create(DefectAnalysis o)
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

        public async Task<DefectAnalysis> Delete(DefectAnalysis o)
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
        public async Task<List<DefectAnalysis>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false , z=>z.AnalysisType , a => a.Defect);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DefectAnalysis>> GetBetweenDateRange(DateTime start, DateTime end)
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
        public async Task<DefectAnalysis> GetById(long id)
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
        public async Task<DefectAnalysis> Update(DefectAnalysis o)
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
