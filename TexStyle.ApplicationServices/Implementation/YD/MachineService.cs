using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.YD {
    internal class MachineService : IMachineService {
        private IMachineRepository _repo;
        public MachineService(IMachineRepository repo) {
            _repo = repo;
        }

        public async Task<Machine> Create(Machine o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Machine> Delete(Machine o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Machine>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false, n => n.MachineType);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Machine>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Machine> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, n => n.MachineType);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Machine> Update(Machine o) {
            try {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
