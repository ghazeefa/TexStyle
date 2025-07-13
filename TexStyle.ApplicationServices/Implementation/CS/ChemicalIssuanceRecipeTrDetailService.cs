using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {

    class ChemicalIssuanceRecipeTrDetailService : IChemicalIssuanceRecipeTrDetailService {

        private IChemicalIssuanceRecipeTrDetailRepository _repo;
        public ChemicalIssuanceRecipeTrDetailService(IChemicalIssuanceRecipeTrDetailRepository repo) {
            _repo = repo;
        }
        public ChemicalIssuanceRecipeTrDetail Create(ChemicalIssuanceRecipeTrDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public ChemicalIssuanceRecipeTrDetail Delete(ChemicalIssuanceRecipeTrDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<ChemicalIssuanceRecipeTrDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<ChemicalIssuanceRecipeTrDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public ChemicalIssuanceRecipeTrDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public ChemicalIssuanceRecipeTrDetail Update(ChemicalIssuanceRecipeTrDetail o) {
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
