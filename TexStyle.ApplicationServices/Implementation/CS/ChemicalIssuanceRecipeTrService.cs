using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    class ChemicalIssuanceRecipeTrService : IChemicalIssuanceRecipeTrService {
        private IChemicalIssuanceRecipeTrRepository _repo;
        public ChemicalIssuanceRecipeTrService(IChemicalIssuanceRecipeTrRepository repo) {
            _repo = repo;
        }
        public ChemicalIssuanceRecipeTr Create(ChemicalIssuanceRecipeTr o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public ChemicalIssuanceRecipeTr Delete(ChemicalIssuanceRecipeTr o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<ChemicalIssuanceRecipeTr> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false, x=>x.Recipe).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<ChemicalIssuanceRecipeTr> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.IssuanceDate.Date >= start.Date && x.IssuanceDate.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public ChemicalIssuanceRecipeTr GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public ChemicalIssuanceRecipeTr Update(ChemicalIssuanceRecipeTr o) {
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
