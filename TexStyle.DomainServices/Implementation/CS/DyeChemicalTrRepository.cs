using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;
using OfficeOpenXml.Drawing.Chart;

namespace TexStyle.DomainServices.Implementation.CS
{
    class DyeChemicalTrRepository : Repository<DyeChemicalTr>, IDyeChemicalTrRepository
    {
        private AppDbContext _db;
        public DyeChemicalTrRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public virtual async Task AddByNewSno(DyeChemicalTr o)
        {
            using (var commit = _db.Database.BeginTransaction())
            {
                try
                {
                   
                    _db.Entry(o).State = EntityState.Added;
                    o.CreatedOn = DateTime.Now;
                    o.Sno = (_db.DyeChemicalTrs.Where(x => x.TrType == o.TrType).Select(x => (long?)x.Sno).Max() ?? 0) + 1;
                    await _db.SaveChangesAsync();
                    commit.Commit();
                }
                catch (Exception ex)
                {
                    commit.Rollback();
                    throw ex;
                }
            }
        }


        public async Task<DyeChemicalTr> GetSingle()
        {
            return await Task.FromResult( _db.DyeChemicalTrs
                .Include(x => x.Party)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Dye)
              .Include(x => x.GateTr).ThenInclude(y => y.GateActivityType)
              .Include(x => x.Recipe)
                .SingleOrDefault());
        }


        public override async Task<DyeChemicalTr> GetSingle(Func<DyeChemicalTr, bool> where, params Expression<Func<DyeChemicalTr, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.DyeChemicalTrs
                .Include(x => x.Party)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Dye)
              .Include(x => x.GateTr)
              .Include(x => x.Recipe)
                .SingleOrDefault(where));
        }

        public async Task<IList<DyeChemicalTr>> GetList()
        {
            return await Task.FromResult( _db.DyeChemicalTrs
               .Include(x => x.Party)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Dye)
               .Include(x => x.GateTr)
               .Include(x => x.Recipe)
                .ToList());
        }

        public override async Task<IList<DyeChemicalTr>> GetList(Func<DyeChemicalTr, bool> where, params Expression<Func<DyeChemicalTr, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.DyeChemicalTrs
               .Include(x => x.Party)
               .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Chemical)
               .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Dye)
               .Include(x => x.GateTr)
               .Include(x => x.Recipe)
               .Where(where).ToList());
        }

        public async Task<IList<Vw_Chemical_UncopiedGatePasINViewModel>> Vw_Chemical_UncopiedGatePasINViewModel(long id)
        {

            return await Task.FromResult( _db.Vw_Chemical_UncopiedGatePasIN.Where(x=>x.TypeId==id).ToList());

        }

        public async Task<IList<LoanTakenInOutRepository_C4ViewModel>> LoantakenInOutReportRepositoryC4(int userid)
        {

            return await Task.FromResult( _db.loanTakenInOutC_4ViewModels.FromSql($"dbo.usp_LoanTaken_InOUT_C4 {userid}").ToList());

        }
        public async Task<IList<LoanPartyInOUTRepository_C5ViewModel>> LoanPartyInOutReportRepositoryC5(int userid)
        {

            return await Task.FromResult( _db.LoanPartyInOUTRepository_C5ViewModels.FromSql($"dbo.usp_LoanParty_InOUT_C5 {userid}").ToList());

        }

        public async Task<IList<DyeSummaryRepository_C1ViewModel>> DyeSummaryReportRepositoryC1(int userid)
        {

            return await Task.FromResult( _db.DyeSummaryRepository_C1ViewModels.FromSql($"dbo.usp_DyedStockSummary_C1 {userid}").ToList());

        } 
        public async Task<IList<StockDetailOutRepository_C3ViewModel>> StockDetailOutReportRepositoryC3()
        {

            return await Task.FromResult( _db.StockDetailOutRepository_C3ViewModels.FromSql("dbo.usp_StockDetailOut_C3").ToList());

        }   
        public async Task<IList<StockDetailInRepository_C2ViewModel>> StockDetailInReportRepositoryC2()
        {

            return await Task.FromResult( _db.StockDetailInRepository_C2ViewModels.FromSql("dbo.usp_StockDetailIn_C2").ToList());

        }
        public async Task<bool> UpdateRate(long id)
        {
            _db.Database.ExecuteSqlCommand($"UpdateRate {id}");
            return await Task.FromResult(true);
        }

        public async Task<List<NullRateQtyRepository_C6ViewModel>> NullRateQtyRepositoryRepositoryC6()
        {
            return await Task.FromResult( _db.NullRateQtyRepository_C6ViewModels.FromSql("dbo.usp_NullQtyAndRate_C6").ToList());
           // return this._db.NullRateQtyRepository_C6ViewModels.FromSql<NullRateQtyRepository_C6ViewModel>((RawSqlString)"dbo.usp_NullQtyAndRate_C6").ToList<NullRateQtyRepository_C6ViewModel>();
        }

        public async Task<List<long>> GetAllRecipeNoUsedRepository()
        {
            return await Task.FromResult( _db.DyeChemicalTrs.Where(y=>y.RecipeId!= null).Select(x => x.RecipeId.Value).ToList());
        }

        public async Task<IList<DyeChemicalDetailRepository_C7ViewModel>> DyeChemicalRateDetailRepositoryC7(long userid)
        {
            return await Task.FromResult( _db.DyeChemicalDetailRepository_C7ViewModels.FromSql($"dbo.usp_DyeAndChemicalRateDetail_C7 { userid}").ToList());
        } 
        public async Task<IList<StoreRecipeSortReportReporistory_ViewModel>> StoreRecipeSortReportReporistory_ViewModel(long userid)
        {
            return await Task.FromResult( _db.StoreRecipeSortReportReporistory_ViewModel.FromSql($"dbo.usp_StoreRecipeSortReport_C8 { userid}").ToList());
        }
        public async Task<IList<StoreRecipeChemicalConsumptionReportRepository_ViewModel>> StoreRecipeChemicalConsumptionReportRepository_ViewModel(long userid)
        {
            return await Task.FromResult( _db.StoreRecipeChemicalConsumptionReportRepository_ViewModel.FromSql($"dbo.usp_StoreRecipeChemicalConsumptionReport_C9 { userid}").ToList());
        } 
        public async Task<IList<StockOutSummaryReportRepository_ViewModel>> StockOutSummaryReportRepository_ViewModel(long userid)
        {
            return await Task.FromResult( _db.StockOutSummaryReportRepository_ViewModel.FromSql($"dbo.usp_DetailStockConsumptionReport_C9 {userid}").ToList());
        }
        public async Task<IList<StockOutSummaryReportRepository_ViewModel1>> StockOutSummaryReportRepository_ViewModel1(long userid)
        {
            return await Task.FromResult( _db.StockOutSummaryReportRepository_ViewModel1.FromSql($"usp_Detail2StockConsumptionReport_C9 { userid}").ToList());
        }
        public async Task<IList<StockOutSummaryReportRepository_ViewModel2>> StockOutSummaryReportRepository_ViewModel2(long userid)
        {
            return await Task.FromResult( _db.StockOutSummaryReportRepository_ViewModel2.FromSql($"usp_Detail3StockConsumptionReport_C9 { userid}").ToList());
        }
        public async Task<IList<DyeChemicalDetailsTrTypeWiseRepository_ViewModel>> DyeChemicalDetailsTrTypeWise(long userid)
        {
            return await Task.FromResult( _db.DyeChemicalDetailsTrTypeWiseRepository_ViewModels.FromSql($"usp_DyeingChemicalIGPDetail  {userid}").ToList());

        }
        public async Task<IList<DetailOpenStockReportRepository_C11ViewModel>> DetailOpenStockReportRepository_C11ViewModels(long userid)
        {
            return await Task.FromResult( _db.DetailOpenStockReportRepository_C11ViewModels.FromSql($"usp_Opening_C11 { userid}").ToList());
        }

        public async Task<IList<ManualRateUpdate>> ManualRateUpdate()
        {
            return await Task.FromResult( _db.ManualRateUpdate.FromSql($"usp_ManualRateUpdate_C10").ToList());

        }

        public async Task<DyeChemicalTotal_Balance_ViewModel> DyeChemicalTotal_Balance(long chemid)
        {
            try
            {
                return await Task.FromResult( _db.dyeChemicalTotal_Balances.FromSql($"usp_LoanPartyIn_ChemicalBalance {chemid}").FirstOrDefault());
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
                return await Task.FromResult( _db.dyeTotal_Balances.FromSql($"usp_LoanPartyIn_DyeBalance {dyeid}").FirstOrDefault());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IList<ChemicalStoreLedgerRepository_ViewModel>> ChemicalStoreLedgerRepository_ViewModels(long userid)
        {

                return await Task.FromResult( _db.ChemicalStoreLedgerRepository_ViewModels.FromSql($"usp_ChemicalStore_Ledger {userid}").ToList());
           
        }

        public async Task<IList<DyeChemicalLoanReturnOut_RemainingBlncViewModel>> DyeChemicalLoanReturnOut_RemainingBlncRepository(long headerid)
        {
            return await Task.FromResult( _db.DyeChemicalLoanReturnOut_RemainingBlncViewModels.FromSql($"usp_DyeChemicalLoanReturnOut_RemainingBlnc {headerid}").ToList());
        }

        public async Task<IList<Vw_LoanReturnOut_PartiallyDispatchedViewModel>> Vw_LoanReturnOut_PartiallyDispatchedViewModel(long id)
        {
            return await Task.FromResult( _db.Vw_LoanReturnOut_PartiallyDispatched.Where(x => x.TypeId == id).ToList());
        }

        public async Task<List<LeatestRateDyesandChemicalsRepository_C14ViewModel>> LeatestRateDyesandChemicalsRepository_C14(long userid)
        {
            return await Task.FromResult( _db.LeatestRateDyesandChemicalsRepository_C14.FromSql($"usp_LatestRateReport_C14  {userid}").ToList());

        }

        public async Task<int> CountDyeChemicalTrByRecipeId(long id)
        {
             int recordCount = await _db.DyeChemicalTrs
            .Where(y => y.RecipeId == id && y.IsDeleted == false && y.TrType == 11 )
            .CountAsync();

            return recordCount;
        }

        public async Task<List<DyeingProductionAndCosting_ViewModel>> DyeingProductionAndCosting(long userid)
        {
            return await Task.FromResult(_db.DyeingProductionAndCosting.FromSql($"usp_DyeingProductionAndCosting {userid}").ToList());
        }

        public async Task<DyeChemicalTr> GetSingleById(long id)
        {
            return await Task.FromResult( _db.DyeChemicalTrs
                .Include(x => x.Party)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.DyeChemicalTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.GateTr)
                .Include(x => x.Recipe)
                .Where(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefault());
        }

        public async Task<List<DyesAndChemicalConsumption_ViewModel>> DyesAndChemicalConsumption(long userid, bool IsYarn)
        {
            return await Task.FromResult(_db.DyesAndChemicalConsumption_ViewModel.FromSql($"usp_DyesAndChemicalConsumption {userid},{IsYarn}").ToList());
        }
        public async Task<List<DyesChemicalAndEnergyConsumption_ViewModel>> DyesChemicalAndEnergyConsumption_ViewModel(long userid, bool IsYarn)
        {
            return await Task.FromResult(_db.DyesChemicalAndEnergyConsumption_ViewModel.FromSql($"usp_DyesChemicalAndEnergyConsumption {userid},{IsYarn}").ToList());
        }
    }
}
