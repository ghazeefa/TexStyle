using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;

namespace TexStyle.ApplicationServices.Interfaces.ICS
{
   public interface IDyeChemicalTrService : IDefaultService<DyeChemicalTr>
    {

        Task<List<DyeChemicalTr>> GetBetweenDateRangeRecipe(DateTime start, DateTime end, long trtype, long trtype2);
        Task<DyeTotal_Balance_ViewModel> DyeTotal_Balance(long dyeid);
        Task<DyeChemicalTotal_Balance_ViewModel> DyeChemicalTotal_Balance(long chemid);
        Task<List<DyeChemicalTr>>  GetBetweenDateRange(DateTime start, DateTime end, long trtype);
        Task<List<DyeChemicalTr>> GetAllByTrType(long TrType);
        Task<List<DyeChemicalTr>> GetByRecipeNo(long Id);
        Task<List<DyeChemicalTr>> GetAllTypesBetweenDateRange(DateTime start, DateTime end);
        Task<List<LoanTakenInOutRepository_C4ViewModel>> LoanTakenInOutReportServiceC4(int userid);
        Task<List<DyeSummaryRepository_C1ViewModel>> DyeSummaryReportServiceC1(int userid);
        Task<List<LoanPartyInOUTRepository_C5ViewModel>> LoanPartyInOutReportServiceC5(int userid);
        Task<List<StockDetailOutRepository_C3ViewModel>> StockDetailOutReportServiceC3();
        Task<List<StockDetailInRepository_C2ViewModel>> StockDetailInReportServiceC2();
        Task<List<NullRateQtyRepository_C6ViewModel>> NullRateQtyRepositoryServiceC6();
        Task<IList<DyeChemicalDetailRepository_C7ViewModel>> DyeChemicalRateDetailServiceC7(long userid);
        Task<IList<StoreRecipeSortReportReporistory_ViewModel>> StoreRecipeSortReportReporistory_ViewModel(long userid);
        Task<IList<StoreRecipeChemicalConsumptionReportRepository_ViewModel>> StoreRecipeChemicalConsumptionReportRepository_ViewModel(long userid);
        Task<IList<StockOutSummaryReportRepository_ViewModel>> StockOutSummaryReportRepository_ViewModel(long userid);
        Task<IList<StockOutSummaryReportRepository_ViewModel1>> StockOutSummaryReportRepository_ViewModel1(long userid);
        Task<IList<StockOutSummaryReportRepository_ViewModel2>> StockOutSummaryReportRepository_ViewModel2(long userid);
        Task<List<ManualRateUpdate>> ManualRateUpdateService();
        Task<IList<DetailOpenStockReportRepository_C11ViewModel>> DetailOpenStockReportRepository_C11ViewModels(long userid);
        
        Task<IList<ChemicalStoreLedgerRepository_ViewModel>> ChemicalStoreLedgerRepository_ViewModels(long userid);

        Task<bool> UpdateRateService(long id);

        Task<List<long>> GetAllRecipeNoUsedService();
        Task<IList<DyeChemicalDetailsTrTypeWiseRepository_ViewModel>> DyeChemicalDetailsTrTypeWise(long userid);

        Task<IList<Vw_Chemical_UncopiedGatePasINViewModel>> Vw_Chemical_UncopiedGatePasINViewModel(long id);

        Task<IList<DyeChemicalLoanReturnOut_RemainingBlncViewModel>> DyeChemicalLoanReturnOut_RemainingBlncService(long headerid);
        Task<IList<Vw_LoanReturnOut_PartiallyDispatchedViewModel>> Vw_LoanReturnOut_PartiallyDispatchedViewModel(long id);
        Task<List<LeatestRateDyesandChemicalsRepository_C14ViewModel>> LeatestRateDyesandChemicals_C14(long userid);
        Task<int> CountDyeChemicalTrByRecipeId(long id);
        Task<List<DyeingProductionAndCosting_ViewModel>> DyeingProductionAndCosting(long userid);
        Task<List<DyesAndChemicalConsumption_ViewModel>> DyesAndChemicalConsumption(long userid, bool IsYarn);
        Task<List<DyesChemicalAndEnergyConsumption_ViewModel>> DyesChemicalAndEnergyConsumption_ViewModel(long userid, bool IsYarn);
    }
}
