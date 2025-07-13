using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.CS
{
    class TrLinkerMasterService : ITrLinkerMasterService
    {
        private ITrLinkerMasterRepository _repo;
        private IChemicalRepository _repochemcal;
        private IDyeRepository _repodye;
        private IDyesChemicalOpenningRepository _repoopening;
        private ILCImportInTrRepository _repolcimport;
        private ILocalPurchaseInTrRepository _repolocalpurchase;
        private ILoanTakenInTrRepository _repoloantakenin;
        private ILoanPartyReturnInTrRepository _repoloanpartyreturn;
        private IChemicalDilutionTrRepository _repochemicaldilution;
        private IInterUnitOutTrRepository _repointerunitout;
        private ILoanTakenReturnOutTrRepository _repoloantakenreturnout;
        private ILoanPartyGivenOutTrRepository _repoloanpartygivenout;
        private IChemicalIssuanceRecipeTrRepository _repochemicalissuance;
        private IStoreReturnNoteRepository _repostorereturnnote;

        public TrLinkerMasterService(ITrLinkerMasterRepository repo, IChemicalRepository repochemcal,
            IDyeRepository repodye , IDyesChemicalOpenningRepository repoopening ,ILCImportInTrRepository repolcimport,
            ILocalPurchaseInTrRepository repolocalpurchase , ILoanTakenInTrRepository repoloantakenin, 
            ILoanPartyReturnInTrRepository repoloanpartyreturn , IChemicalDilutionTrRepository repochemicaldilution,
            IInterUnitOutTrRepository repointerunitout, ILoanTakenReturnOutTrRepository repoloantakenreturnout,
            ILoanPartyGivenOutTrRepository repoloanpartygivenout,IChemicalIssuanceRecipeTrRepository repochemicalissuance,
            IStoreReturnNoteRepository repostorereturnnote
            )
        {
            _repo = repo;
            _repochemcal = repochemcal;
            _repodye = repodye;
            _repoopening = repoopening;
            _repolcimport = repolcimport;
            _repolocalpurchase = repolocalpurchase;
            _repoloantakenin = repoloantakenin;
           _repoloanpartyreturn = repoloanpartyreturn;
            _repointerunitout = repointerunitout;
            _repochemicaldilution = repochemicaldilution;
           _repoloantakenreturnout = repoloantakenreturnout;
            _repoloanpartygivenout = repoloanpartygivenout;
            _repochemicalissuance = repochemicalissuance;
            _repostorereturnnote = repostorereturnnote;



        }

        public void UpdateRate(DateTime Date)
        {
            _repo.ExceRateStoreProcedure(Date);
        }
        public List<TrLinkerMaster> GetbetweenDates(DateTime DateFrom ,DateTime DateTo )
        {
          return  _repo.GetList(x=>x.IsDeleted==true &&x.Date>=DateFrom &&x.Date<=DateTo ).ToList();
        }

        //public  GetbetweenDates(DateTime DateFrom)
        //{
        //    return _repo.GetList(x => x.IsDeleted == true && x.Date >= DateFrom && x.Date <= DateTo).ToList();
        //}
        //public List<TrLinkerMaster> GetQtybyItem( long? dyeId, long? chemicalId )
        //{

        //  //  _repo.ExceStoreProcedure();

        //    try
        //    {
        //        //get the latest opening id and get all record of a chemical/dyde from onward that opening asendingly datewise 
        //       long maxopeningId= _repo.GetAll(x => x.IsDeleted == false && x.DyesChemicalOpenningDetailId != null).Max(x => x.Id);
        //        if(dyeId!= null)
        //        {
        //         return   _repo.GetList(x=>x.IsDeleted==false && x.DyeId==dyeId && x.Id>=maxopeningId).OrderBy(x=>x.Date).ToList();

        //        }
        //        else
        //        {
        //          return  _repo.GetList(x => x.IsDeleted == false && x.ChemicalId == chemicalId && x.Id >= maxopeningId).OrderBy(x => x.Date).ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public TrLinkerMaster UpdateTrMaster(DateTime date)
        //{



        //}
        //public TrLinkerMaster UpdateTrMaster(long? dyeId, long? chemicalId , DateTime? Date, long headerid , long? detailid , long transid)
        //{
        //    try
        //    {
        //        TrLinkerMaster o = new TrLinkerMaster();
        //       if(transid == ChemicalTransactions.LCImport)
        //        {
        //            if(Date!= null)
        //            {
        //             List<TrLinkerMaster>   ho = _repo.GetList(x => x.IsDeleted == false && x.LCImportInTrId == headerid ).ToList();
        //                ho.ForEach(h=>{ 
        //                  h.Date = Date.Value ;
        //                    _repo.Update(h);
        //                });

        //            }else if(dyeId!= null || chemicalId!= null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.LCImportInTrId == headerid && x.LCImportInTrDetailId == detailid);

        //                o.DyeId = dyeId;o.ChemicalId = chemicalId; 

        //           _repo.Update(o);
        //            }
        //        }
        //       else if(transid == ChemicalTransactions.LocalPurchase)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.LocalPurchaseInTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.LocalPurchaseInTrId == headerid && x.LocalPurchaseInTrDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.LoanPartyReturnIn)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.LoanPartyReturnInTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanPartyReturnInTrId == headerid && x.LoanPartyReturnInTrId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.LoanTakenIn)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.LoanTakenInTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanTakenInTrId == headerid && x.LoanTakenInTrDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.Dilution)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.ChemicalDilutionTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.ChemicalDilutionTrId == headerid && x.ChemicalDilutionTrDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.OpeningBalance)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.DyesChemicalOpenningId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.DyesChemicalOpenningId == headerid && x.DyesChemicalOpenningDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.InterUnitOut)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.InterUnitOutTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.InterUnitOutTrId == headerid && x.InterUnitOutTrDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.LoanTakenReturnOut)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.LoanTakenReturnOutTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanTakenReturnOutTrId == headerid && x.LoanTakenReturnOutTrDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.LoanPartyGivenOut)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.LoanPartyGivenOutTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanPartyGivenOutTrId == headerid && x.LoanPartyGivenOutTrDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.ChemicalIssuanceRecipe)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.ChemicalIssuanceRecipeTrId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.ChemicalIssuanceRecipeTrId == headerid && x.ChemicalIssuanceRecipeTrDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        else if (transid == ChemicalTransactions.StoreReturnNote)
        //        {
        //            if (Date != null)
        //            {
        //                List<TrLinkerMaster> ho = _repo.GetList(x => x.IsDeleted == false && x.StoreReturnNoteId == headerid).ToList();
        //                ho.ForEach(h => {
        //                    h.Date = Date.Value;
        //                    _repo.Update(h);
        //                });

        //            }
        //            else if (dyeId != null || chemicalId != null)
        //            {
        //                o = _repo.GetSingle(x => x.IsDeleted == false && x.StoreReturnNoteId == headerid && x.StoreReturnNoteDetailId == detailid);

        //                o.DyeId = dyeId; o.ChemicalId = chemicalId;

        //                _repo.Update(o);
        //            }
        //        }
        //        return o;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void AddDebitEntry()
        //{
        //    throw new NotImplementedException();
        //}

        //public TrLinkerMaster Create(TrLinkerMaster o)
        //{
        //    try
        //    {
        //        _repo.Add(o);
        //        return o;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public TrLinkerMaster DeletebyTRType(long headerid, long detailid, long transid)
        //{
        //    try
        //    {
        //        TrLinkerMaster o = new TrLinkerMaster();
        //        if (transid == ChemicalTransactions.LCImport)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.LCImportInTrId == headerid && x.LCImportInTrDetailId == detailid);
        //            if(o!=null)o.IsDeleted = true;
        //            _repo.Update(o);

        //        }
        //        else if (transid == ChemicalTransactions.LocalPurchase)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.LocalPurchaseInTrId == headerid && x.LocalPurchaseInTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;
        //            _repo.Update(o);

        //        }
        //        else if (transid == ChemicalTransactions.LoanTakenIn)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanTakenInTrId == headerid && x.LoanTakenInTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;

        //            _repo.Update(o);
        //        }
        //        else if (transid == ChemicalTransactions.Dilution)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.ChemicalDilutionTrId == headerid && x.ChemicalDilutionTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;
        //            _repo.Update(o);
        //        }
        //        else if (transid == ChemicalTransactions.OpeningBalance)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.DyesChemicalOpenningId == headerid && x.DyesChemicalOpenningDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;
        //            _repo.Update(o);
        //        }
        //        else if (transid == ChemicalTransactions.InterUnitOut)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.InterUnitOutTrId == headerid && x.InterUnitOutTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;

        //            _repo.Update(o);

        //        }
        //        else if (transid == ChemicalTransactions.LoanTakenReturnOut)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanTakenReturnOutTrId == headerid && x.LoanTakenReturnOutTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;

        //            _repo.Update(o);
        //        }
        //        else if (transid == ChemicalTransactions.LoanPartyGivenOut)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanPartyGivenOutTrId == headerid && x.LoanPartyGivenOutTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;
        //            _repo.Update(o);
        //        }
        //        else if (transid == ChemicalTransactions.LoanPartyReturnIn)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.LoanPartyReturnInTrId == headerid && x.LoanPartyReturnInTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;
        //            _repo.Update(o);
        //        }
        //        else if (transid == ChemicalTransactions.ChemicalIssuanceRecipe)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.ChemicalIssuanceRecipeTrId == headerid && x.ChemicalIssuanceRecipeTrDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;
        //            _repo.Update(o);
        //        }
        //        else if (transid == ChemicalTransactions.StoreReturnNote)
        //        {
        //            o = _repo.GetSingle(x => x.IsDeleted == false && x.StoreReturnNoteId == headerid && x.StoreReturnNoteDetailId == detailid);
        //            if (o != null) o.IsDeleted = true;
        //            _repo.Update(o);
        //        }
        //        return o;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<TrLinkerMaster> GetAll()
        {
            try
            {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TrLinkerMaster> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TrLinkerMaster GetById(long id)
        {
            try
            {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TrLinkerMaster Update(TrLinkerMaster o)
        {
            try
            {
                _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TrLinkerMaster Delete(TrLinkerMaster o)
        {
            try
            {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TrLinkerMaster Create(TrLinkerMaster o)
        {
            throw new NotImplementedException();
        }
    }
}
