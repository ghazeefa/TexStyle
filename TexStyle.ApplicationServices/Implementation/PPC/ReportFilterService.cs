using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class ReportFilterService : IReportFilterService {
        private IReportFilterRepository _repo;
        public ReportFilterService(IReportFilterRepository repo) {
            _repo = repo;
        }

        public async Task<ReportFilter> Create(ReportFilter o) {
            try {
                await _repo.Add(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<ReportFilter> Delete(ReportFilter o) {
            try {
                //o.IsDeleted = true;
                await _repo.Remove(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<ReportFilter>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<ReportFilter>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<ReportFilter> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<ReportFilter>> GetReportFiltersForUser(int UserId) {
            try {
                var list = await _repo.GetList(x => x.UserId == UserId && x.IsDeleted == false,
                    n => n.YarnParty,
                    n => n.YarnQuality,
                    n => n.YarnType,
                    n => n.FabricTypes,
                    n => n.FabricQuality,
                    n => n.Buyer
                    );
                return list.ToList();

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<ReportFilter> Update(ReportFilter o) {
            try {
                if (o.UserId == 4)
                {
                    o.IsYarn = true;


                }
                if (o.UserId == 1)
                {
                    o.IsYarn = true;


                }
                if (o.UserId == 7)
                {
                    o.IsYarn = true;


                }
                if (o.UserId == 14)
                {
                    o.IsYarn = false;

                } if (o.UserId == 15)
                {
                    o.IsYarn = false;
                }
                
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }
    }
}
