using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.PPC;

namespace TexStyle.DomainServices.Interfaces.IPPC
{
    public  interface IPPCPlanningRepository:IRepository<PPCPlanning>
    {
        Task<IList<PPCPlanning>> GetAllFabric();
        Task<List<EcruYarnReceiveRepository_P1ViewModel>> EcruYarnReceiveRepository_P1(long userid);
        Task<List<EcruYarnConsumptionRepository_P2ViewModel>> EcruYarnConsumptionRepository_P2(long userid);
        Task<List<DailyProductionRepository_P3ViewModel>> DailyProductionRepository_P3(long userid);
        Task<List<DyedStockRepository_P4ViewModel>> DyedStockRepository_P4(long userid);
        Task<List<DyedStockRepository1_P4ViewModel>> DyedStockRepository1_P4(long userid);
        Task<List<DyedYarnDespatchedRepository_P5ViewModel>> DyedYarnDespatchedRepository_P5(long userid);
        Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecordRepository_P9ViewModel(long userid);
        Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecord1Repository_P9ViewModel(int LotNo);
        Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository_P7(long userid);   
        Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository1_P7(long userid);
        Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository11_P7(long userid);
        Task<List<IGPRecordFabric_P8ViewModel>> IGPRecordFabric_P8ViewModel(long BuyerId, long FabricTypeId, long FabricQualityId);
        Task<long> AddRecord(long headerid, long detailid, long po, long ProductionTypeId, DateTime? issueddate, bool IsConfirmed, decimal Qty, int lotno, long BuyerColorId, string FactoryPo, int dyeingWOID, int dyeingWODetailID);
        Task<List<LPSIssuanceRepository_P8ViewModel>> LPSIssuanceRepository_P8ViewModel(long userid);
        Task<List<EcruStockSummary_P10RepositoryViewModal>> EcruStockSummary_P10RepositoryViewModal(long userid);
        Task<List<DyedStockSummary_P11RepositoryViewModal>> DyedStockSummary_P11RepositoryViewModal(long userid);
        Task<List<FactoryPoKgRepository_P12ViewModel>> FactoryPoKgRepository_P12ViewModel();
        Task<List<CKL1POViewModelRepository>> CKL1POViewModelRepository(long po);
        Task<List<DyeingLossLotWiseViewModel>> DyeingLossLotWiseViewModel(long userid);
        Task<List<DyeingLossPOWiseColorWiseViewModel>> DyeingLossPOWiseColorWiseViewModel(long userid);
        Task<List<EcruFabricPOWiseKnitterWiseViewModel>> EcruFabricPOWiseKnitterWiseViewModel(long userid);
        Task<List<EcruFabricIssuenceViewModel>> EcruFabricIssuenceViewModel(long userid);
        Task<List<GetFabricDetailByLot>> GetFabricDetailByLot(long lotNo);
        Task<List<EcruStockSummary_YarnCountGsm_RepositoryViewModal>> EcruStockSummary_YarnCountGsm_RepositoryViewModal(long userid);

    }
}
