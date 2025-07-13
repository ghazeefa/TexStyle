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
    class DyeChemicalTrService : IDyeChemicalTrService
    {
        private IDyeChemicalTrRepository _repo;
        public DyeChemicalTrService(IDyeChemicalTrRepository repo)
        {
            _repo = repo;
        }

        public async Task<DyeChemicalTr> Create(DyeChemicalTr o)
        {
            try
            {
                await _repo.AddByNewSno(o);

                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DyeChemicalTr> Delete(DyeChemicalTr o)
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

        public async Task<List<DyeChemicalTr>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false, x => x.DyeChemicalTrDetails);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<DyeChemicalTr>> GetAllByTrType(long TrType)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.TrType == TrType, x => x.DyeChemicalTrDetails);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DyeChemicalTr>> GetByRecipeNo(long Id)
        {
            try
            {
                var list= await _repo.GetList(x => x.IsDeleted == false && x.TrType == 11 && x.RecipeId == Id, x => x.DyeChemicalTrDetails);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Vw_Chemical_UncopiedGatePasINViewModel>> Vw_Chemical_UncopiedGatePasINViewModel(long id)
        {
            var list = await _repo.Vw_Chemical_UncopiedGatePasINViewModel(id);
            return list.ToList();
        }

        public async Task<List<DyeChemicalTr>> GetBetweenDateRange(DateTime start, DateTime end, long trtype)
        {
            try
            {
                var list= await _repo.GetList(x => x.IsDeleted == false && x.TrType == trtype);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DyeChemicalTr>> GetBetweenDateRangeRecipe(DateTime start, DateTime end, long trtype, long trtype2)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && (x.TrType == trtype ||x.TrType==trtype2) && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DyeChemicalTr>> DetailStockInReportC_2(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<LoanTakenInOutRepository_C4ViewModel>> LoanTakenInOutReportServiceC4(int userid)
        {
            try
            {
                var list = await _repo.LoantakenInOutReportRepositoryC4(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DyeSummaryRepository_C1ViewModel>> DyeSummaryReportServiceC1(int userid)
        {
            try
            {
                var list = await _repo.DyeSummaryReportRepositoryC1(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DyeChemicalTr>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            throw  new NotImplementedException();
        }

        public async Task<DyeChemicalTr> GetById(long id)
        {
            try
            {
                //return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, 
                //    x => x.DyeChemicalTrDetails, x => x.Party, x => x.GateTr, x => x.Recipe);
                return await _repo.GetSingleById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DyeChemicalTr> Update(DyeChemicalTr o)
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

        public async Task<List<DyeChemicalTr>> GetAllTypesBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LoanPartyInOUTRepository_C5ViewModel>> LoanPartyInOutReportServiceC5(int userid)
        {
            try
            {
                var list = await _repo.LoanPartyInOutReportRepositoryC5(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StockDetailOutRepository_C3ViewModel>> StockDetailOutReportServiceC3()
        {
            try
            {
                var list = await _repo.StockDetailOutReportRepositoryC3();
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StockDetailInRepository_C2ViewModel>> StockDetailInReportServiceC2()
        {
            try
            {
                var list = await _repo.StockDetailInReportRepositoryC2();
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRateService(long id)
        {
            try
            {
                return await _repo.UpdateRate(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<NullRateQtyRepository_C6ViewModel>> NullRateQtyRepositoryServiceC6()
        {
            try
            {
                return await _repo.NullRateQtyRepositoryRepositoryC6();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<long>> GetAllRecipeNoUsedService()
        {
            try
            {
                return await _repo.GetAllRecipeNoUsedRepository();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 

        public async Task<IList<DyeChemicalDetailRepository_C7ViewModel>> DyeChemicalRateDetailServiceC7(long userid)
        {
            try
            {
                return await _repo.DyeChemicalRateDetailRepositoryC7(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<StoreRecipeSortReportReporistory_ViewModel>> StoreRecipeSortReportReporistory_ViewModel(long userid)
        {
            try
            {
                return await _repo.StoreRecipeSortReportReporistory_ViewModel(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IList<StoreRecipeChemicalConsumptionReportRepository_ViewModel>> StoreRecipeChemicalConsumptionReportRepository_ViewModel(long userid)
        {
            try
            {
                return await _repo.StoreRecipeChemicalConsumptionReportRepository_ViewModel(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }    
        public async Task<IList<StockOutSummaryReportRepository_ViewModel>> StockOutSummaryReportRepository_ViewModel(long userid)
        {
            try
            {
                return await _repo.StockOutSummaryReportRepository_ViewModel(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            } 

        }
        public async Task<IList<StockOutSummaryReportRepository_ViewModel1>> StockOutSummaryReportRepository_ViewModel1(long userid)
        {
            try
            {
                return await _repo.StockOutSummaryReportRepository_ViewModel1(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<StockOutSummaryReportRepository_ViewModel2>> StockOutSummaryReportRepository_ViewModel2(long userid)
        {
            try
            {
                return await _repo.StockOutSummaryReportRepository_ViewModel2(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<DyeChemicalDetailsTrTypeWiseRepository_ViewModel>> DyeChemicalDetailsTrTypeWise(long userid)
        {
            try
            {
                return await _repo.DyeChemicalDetailsTrTypeWise(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<DetailOpenStockReportRepository_C11ViewModel>> DetailOpenStockReportRepository_C11ViewModels(long userid)
        {
            try
            {
                return await _repo.DetailOpenStockReportRepository_C11ViewModels(userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<ChemicalStoreLedgerRepository_ViewModel>> ChemicalStoreLedgerRepository_ViewModels(long userid)
        {
            try
            {
                return await _repo.ChemicalStoreLedgerRepository_ViewModels(userid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<ManualRateUpdate>> ManualRateUpdateService()
        {
            try
            {
                var list = await _repo.ManualRateUpdate();
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DyeChemicalTotal_Balance_ViewModel> DyeChemicalTotal_Balance(long chemid)
        {
            try
            {
                return await _repo.DyeChemicalTotal_Balance(chemid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DyeTotal_Balance_ViewModel> DyeTotal_Balance(long dyeid)
        {
            try
            {
                return await _repo.DyeTotal_Balance(dyeid);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IList<DyeChemicalLoanReturnOut_RemainingBlncViewModel>> DyeChemicalLoanReturnOut_RemainingBlncService(long headerid)
        {
            try
            {
                return await _repo.DyeChemicalLoanReturnOut_RemainingBlncRepository(headerid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IList<Vw_LoanReturnOut_PartiallyDispatchedViewModel>> Vw_LoanReturnOut_PartiallyDispatchedViewModel(long id)
        {
            try
            {
                var list = await _repo.Vw_LoanReturnOut_PartiallyDispatchedViewModel(id);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<LeatestRateDyesandChemicalsRepository_C14ViewModel>> LeatestRateDyesandChemicals_C14(long userid)
        {
            try
            {
                return await _repo.LeatestRateDyesandChemicalsRepository_C14(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountDyeChemicalTrByRecipeId(long id)
        {
            try
            {
                return await _repo.CountDyeChemicalTrByRecipeId(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DyeingProductionAndCosting_ViewModel>> DyeingProductionAndCosting(long userid)
        {
            return await _repo.DyeingProductionAndCosting(userid);
        }

        public async Task<List<DyesAndChemicalConsumption_ViewModel>> DyesAndChemicalConsumption(long userid, bool IsYarn)
        {
            var list = (await _repo.DyesAndChemicalConsumption(userid, IsYarn)).ToList();
            return list;
        }
        public async Task<List<DyesChemicalAndEnergyConsumption_ViewModel>> DyesChemicalAndEnergyConsumption_ViewModel(long userid, bool IsYarn)
        {
            var list = (await _repo.DyesChemicalAndEnergyConsumption_ViewModel(userid, IsYarn)).ToList();
            return list;
        }
    }
  }

