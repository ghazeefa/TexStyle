using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;

namespace TexStyle.DomainServices.Interfaces.ICS
{
   public interface IDyeChemicalTrRepository: IRepository<DyeChemicalTr>
    {
        Task AddByNewSno(DyeChemicalTr o);
       // ChemicalDyeIdSelectionData ChemicalDyeIdData(long chemdyeId, bool query);
        Task<DyeTotal_Balance_ViewModel> DyeTotal_Balance(long dyeid);
        Task<DyeChemicalTotal_Balance_ViewModel> DyeChemicalTotal_Balance(long chemid);
        Task<IList<LoanTakenInOutRepository_C4ViewModel>> LoantakenInOutReportRepositoryC4(int userid);
        Task<IList<LoanPartyInOUTRepository_C5ViewModel>> LoanPartyInOutReportRepositoryC5(int userid);
        Task<IList<DyeSummaryRepository_C1ViewModel>> DyeSummaryReportRepositoryC1(int userid);
        Task<IList<StockDetailOutRepository_C3ViewModel>> StockDetailOutReportRepositoryC3();
        Task<IList<StockDetailInRepository_C2ViewModel>> StockDetailInReportRepositoryC2();
        Task<IList<DyeChemicalDetailRepository_C7ViewModel>> DyeChemicalRateDetailRepositoryC7(long userid);
        Task<bool> UpdateRate(long id);
        Task<List<NullRateQtyRepository_C6ViewModel>> NullRateQtyRepositoryRepositoryC6();
      
        Task<IList<StoreRecipeSortReportReporistory_ViewModel>> StoreRecipeSortReportReporistory_ViewModel(long userid);
        Task<IList<StoreRecipeChemicalConsumptionReportRepository_ViewModel>> StoreRecipeChemicalConsumptionReportRepository_ViewModel(long userid);
        Task<IList<StockOutSummaryReportRepository_ViewModel>> StockOutSummaryReportRepository_ViewModel(long userid);
        Task<IList<StockOutSummaryReportRepository_ViewModel1>> StockOutSummaryReportRepository_ViewModel1(long userid);
        Task<IList<StockOutSummaryReportRepository_ViewModel2>> StockOutSummaryReportRepository_ViewModel2(long userid);

        Task<IList<ManualRateUpdate>> ManualRateUpdate();
        Task<List<long>> GetAllRecipeNoUsedRepository();
        Task<IList<DyeChemicalDetailsTrTypeWiseRepository_ViewModel>> DyeChemicalDetailsTrTypeWise(long userid);
        Task<IList<DetailOpenStockReportRepository_C11ViewModel>> DetailOpenStockReportRepository_C11ViewModels(long userid);

        Task<IList<ChemicalStoreLedgerRepository_ViewModel>> ChemicalStoreLedgerRepository_ViewModels(long userid);
        Task<IList<Vw_Chemical_UncopiedGatePasINViewModel>> Vw_Chemical_UncopiedGatePasINViewModel(long id);
        Task<IList<DyeChemicalLoanReturnOut_RemainingBlncViewModel>> DyeChemicalLoanReturnOut_RemainingBlncRepository(long headerid);
        Task<IList<Vw_LoanReturnOut_PartiallyDispatchedViewModel>> Vw_LoanReturnOut_PartiallyDispatchedViewModel(long id);

        Task<List<LeatestRateDyesandChemicalsRepository_C14ViewModel>> LeatestRateDyesandChemicalsRepository_C14(long userid);
        Task<int> CountDyeChemicalTrByRecipeId(long id);
        Task<List<DyeingProductionAndCosting_ViewModel>> DyeingProductionAndCosting(long userid);
        Task<DyeChemicalTr> GetSingleById(long id);
        Task<List<DyesAndChemicalConsumption_ViewModel>> DyesAndChemicalConsumption(long userid, bool IsYarn);
        Task<List<DyesChemicalAndEnergyConsumption_ViewModel>> DyesChemicalAndEnergyConsumption_ViewModel(long userid, bool IsYarn);

    }
}
