using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS
{
    class DyeingEnergyConsumptionService : IDyeingEnergyConsumptionService
    {
        private IDyeingEnergyConsumptionRepository _repo;
        public DyeingEnergyConsumptionService(IDyeingEnergyConsumptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<DyeingEnergyConsumption> Create(DyeingEnergyConsumption o)
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

        public async Task<DyeingEnergyConsumption> Delete(DyeingEnergyConsumption o)
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

        public async Task<List<DyeingEnergyConsumption>> GetAll()
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

        public async Task<List<DyeingEnergyConsumption>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.Date >= start.Date && x.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DyeingEnergyConsumption> GetById(long id)
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

        public async Task<DyeingEnergyConsumption> GetEnergyConsumptionBetweenDates(DateTime dateFrom, DateTime dateTo, bool IsYarn)
        {
            var data = await _repo.GetEnergyConsumptionBetweenDates(dateFrom, dateTo, IsYarn);
            return data;
        }

        public async Task<DyeingEnergyConsumption> Update(DyeingEnergyConsumption o)
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

