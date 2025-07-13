using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    internal class ChemicalDilutionTrDetailService : IChemicalDilutionTrDetailService {
        private IChemicalDilutionTrDetailRepository _repo;
        public ChemicalDilutionTrDetailService(IChemicalDilutionTrDetailRepository repo) {
            _repo = repo;
        }

        public ChemicalDilutionTrDetail Create(ChemicalDilutionTrDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public ChemicalDilutionTrDetail Delete(ChemicalDilutionTrDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<ChemicalDilutionTrDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<ChemicalDilutionTrDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public ChemicalDilutionTrDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public ChemicalDilutionTrDetail Update(ChemicalDilutionTrDetail o) {
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
