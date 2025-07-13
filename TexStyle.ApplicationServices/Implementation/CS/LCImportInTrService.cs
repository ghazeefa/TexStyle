using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    internal class LCImportInTrService : ILCImportInTrService {
        private ILCImportInTrRepository _repo;
        public LCImportInTrService(ILCImportInTrRepository repo) {
            _repo = repo;
        }
        public LCImportInTr Create(LCImportInTr o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LCImportInTr Delete(LCImportInTr o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LCImportInTr> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LCImportInTr> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date , x=>x.GateTr).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LCImportInTr GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LCImportInTr Update(LCImportInTr o) {
            try {
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
