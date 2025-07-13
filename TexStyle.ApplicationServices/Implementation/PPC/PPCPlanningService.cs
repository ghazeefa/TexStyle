using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    class PPCPlanningService : IPPCPlanningService {

        private IPPCPlanningRepository _repo;
        public PPCPlanningService(IPPCPlanningRepository pPCPlanningRepo) {
            _repo = pPCPlanningRepo;
        }
        
        public async Task<PPCPlanning> Create(PPCPlanning o) {
            //create yarn type user
            try {
                o.CreatedOn = DateTime.Now;
                o.BranchId = 1;
                o.IsYarn = true;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<PPCPlanning> CreateFabric(PPCPlanning o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                o.BranchId = 2;
                o.IsYarn = false;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PPCPlanning> Delete(PPCPlanning o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<EcruYarnReceiveRepository_P1ViewModel>> EcruYarnReceiveService_P1(long userid)
        {
            try
            {

                var list = await _repo.EcruYarnReceiveRepository_P1(userid);

                //x => x.InwardGatePass,

                return list.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }    
        public async Task<List<EcruYarnConsumptionRepository_P2ViewModel>> EcruYarnConsumptionService_P2(long userid)
        {
            try
            {

                var list = await _repo.EcruYarnConsumptionRepository_P2(userid);

                //x => x.InwardGatePass,

                return list.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<PPCPlanning>> GetAll() {
            try {

                var list = await _repo.GetAll();

                //x => x.InwardGatePass,

                return list.ToList();

            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<PPCPlanning>> GetAllFabric()
        {
            try
            {

                var list = await _repo.GetAllFabric();

                //x => x.InwardGatePass,

                return list.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<PPCPlanning>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.IssuedDate.Date >= start.Date && x.IssuedDate.Date <= end.Date && x.IsYarn == true && x.BranchId == 1);
                return list.ToList();
                //return _repo.GetList(x => x.IsDeleted == false && x.IssuedDate.Date >= start.Date && x.IssuedDate.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<PPCPlanning>> GetBetweenDateRangeFabric(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.IssuedDate.Date >= start.Date && x.IssuedDate.Date <= end.Date && x.IsYarn == false && x.BranchId == 2);
                return list.ToList();
}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PPCPlanning> GetById(long id) {
            try {
                var PPC = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                      x => x.ProductionType,
                      x => x.BuyerColor.Buyer.Party,
                      x => x.YarnManufacturer,
                      x => x.YarnQuality,
                      x => x.YarnType,
                      x => x.FabricTypes,
                      x => x.FabricType,
                      x => x.FabricQuality,
                      x => x.InwardGatePassDetail,
                      x => x.PurchaseOrder,
                      x => x.knittingParty
                      );
                return PPC;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<PPCPlanning>> GetListbyIgpDetailId(long? id) {
            try {
                var list = await _repo.GetList(x => x.InwardGatePassDetailId == id && x.IsDeleted == false,
                      x => x.ProductionType,
                      x => x.BuyerColor.Buyer.Party,
                      x => x.YarnManufacturer,
                      x => x.YarnQuality,
                      x => x.YarnType,
                      x => x.FabricQuality,
                      x => x.FabricTypes,
                      x => x.FabricType,
                      x => x.InwardGatePassDetail,
                      x => x.PurchaseOrder,
                      x => x.knittingParty
                      );
                return list.ToList();

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IQueryable<PPCPlanning>> GetReprocessById(long? id) {
            try {
                var list = await _repo.GetAllQueryable(
                      x => x.ProductionType,
                      x => x.BuyerColor.Buyer.Party,
                      x => x.YarnManufacturer,
                      x => x.YarnQuality,
                      x => x.YarnType,
                      x => x.FabricQuality,
                      x => x.FabricTypes,
                      x => x.FabricType,
                      x => x.InwardGatePassDetail,
                      x => x.PurchaseOrder,
                      x => x.Reprocesses,
                      x => x.knittingParty
                      );
                return list.Where(x => x.Id == id && x.IsDeleted == false);

            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<PPCPlanning>> GetListByPoCode(string id)
        {
            try
            {
                //return _repo.GetAllQueryable(
                //      x => x.ProductionType,
                //      x => x.BuyerColor.Buyer.Party,
                //      x => x.YarnManufacturer,
                //      x => x.YarnQuality,
                //      x => x.YarnType,
                //      x => x.FabricQuality,
                //      x => x.FabricTypes,
                //      x => x.FabricType,
                //      x => x.InwardGatePassDetail,
                //      x => x.PurchaseOrder,
                //      x => x.Reprocesses,
                //      x => x.knittingParty
                //      ).Where(d && x.IsDeleted == false);
                var list = await _repo.GetList(x => x.FactoryPo == id && x.IsDeleted == false,
                 x => x.ProductionType,
                 x => x.BuyerColor.Buyer.Party,
                 x => x.YarnManufacturer,
                 x => x.YarnQuality,
                 x => x.YarnType,
                 x => x.FabricQuality,
                 x => x.FabricTypes,
                 x => x.FabricType,
                 x => x.InwardGatePassDetail,
                 x => x.PurchaseOrder,
                 x => x.knittingParty
                 );
                return list.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PPCPlanning> Update(PPCPlanning o) {
            try {
                o.IsYarn = true;
                o.BranchId = 1;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<PPCPlanning> UpdateFabric(PPCPlanning o)
        {
            try
            {
                // update Fabric user
                o.IsYarn = false;
                o.BranchId = 2;
                 await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DailyProductionRepository_P3ViewModel>> DailyProductionService_P3(long userid)
        {

            try
            {

                var list = await _repo.DailyProductionRepository_P3(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 
        public async Task<List<DyedStockRepository_P4ViewModel>> DyedStockService_P4(long userid)
        {

            try
            {

                var list = await _repo.DyedStockRepository_P4(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DyedStockRepository1_P4ViewModel>> DyedStockService1_P4(long userid)
        {

            try
            {

                var list = await _repo.DyedStockRepository1_P4(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }      
        public async Task<List<LPSIssuanceRepository_P8ViewModel>> LPSIssuanceRepository_P8ViewModel(long userid)
        {

            try
            {

                var list = await _repo.LPSIssuanceRepository_P8ViewModel(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DyedYarnDespatchedRepository_P5ViewModel>> DyedYarnDespatchedService_P5(long userid)
        {

            try
            {
                var list = await _repo.DyedYarnDespatchedRepository_P5(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository_P7(long userid)
        {
            try
            {

                var list = await _repo.EcruYarnStockRepository_P7(userid);
                return list.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository1_P7(long userid)
        {
            try
            {
               
               var list = await _repo.EcruYarnStockRepository1_P7(userid);
                return list.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository11_P7(long userid)
        {
            try
            {

                var list = await _repo.EcruYarnStockRepository11_P7(userid);

                //var res=
                //var orderByResult = from s in result
                //                    orderby s.Buyer
                //                    select s;
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
        public async Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecordRepository_P9ViewModel(long userid)
        {
            try
            {
                var list = await _repo.IssuanceRecordRepository_P9ViewModel(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        public async Task<List<EcruStockSummary_P10RepositoryViewModal>> EcruStockSummary_P10RepositoryViewModal(long userid)
        {
            try
            {

                var list = await _repo.EcruStockSummary_P10RepositoryViewModal(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
        public async Task<List<DyedStockSummary_P11RepositoryViewModal>> DyedStockSummary_P11RepositoryViewModal(long userid)
        {
            try
            {

                var list = await _repo.DyedStockSummary_P11RepositoryViewModal(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
        public async Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecord1Repository_P9ViewModel(int LotNo)
        {
            try
            {
                var list = await _repo.IssuanceRecord1Repository_P9ViewModel(LotNo);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        
        public async Task<List<FactoryPoKgRepository_P12ViewModel>> FactoryPoKgRepository_P12ViewModel()
        {
            try
            {

                var list = await _repo.FactoryPoKgRepository_P12ViewModel();
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
        public async Task<List<CKL1POViewModelRepository>> CKL1POViewModelRepository(long po)
        {


            var list = await _repo.CKL1POViewModelRepository(po);
            return list.ToList();

            //catch (Exception ex)
            //{
            //    return null;
            //}
        }       
        
        public async Task<List<IGPRecordFabric_P8ViewModel>> IGPRecordFabric_P8ViewModel(long BuyerId, long FabricTypeId, long FabricQualityId)
        {
            try
            {
                var list = await _repo.IGPRecordFabric_P8ViewModel(BuyerId, FabricTypeId, FabricQualityId);

                //x => x.InwardGatePass,

                return list.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<long> AddRecord(long headerid, long detailid, long po, long ProductionTypeId, DateTime? issueddate, bool IsConfirmed, decimal Qty, int lotno,long BuyerColorId, string FactoryPo, int dyeingWOID, int dyeingWODetailID)
                      {
            try
            {
                //store procedure
                var id = await _repo.AddRecord(headerid, detailid, po, ProductionTypeId, issueddate, IsConfirmed, Qty, lotno, BuyerColorId, FactoryPo, dyeingWOID, dyeingWODetailID);
                return 1;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<DyeingLossLotWiseViewModel>> DyeingLossLotWiseViewModel(long userid)
        {
            try
            {
                var list = await _repo.DyeingLossLotWiseViewModel(userid);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DyeingLossPOWiseColorWiseViewModel>> DyeingLossPOWiseColorWiseViewModel(long userid)
        {
            try
            {
                var list = await _repo.DyeingLossPOWiseColorWiseViewModel(userid);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<EcruFabricPOWiseKnitterWiseViewModel>> EcruFabricPOWiseKnitterWiseViewModel(long userid)
        {
            try
            {
                var list = await _repo.EcruFabricPOWiseKnitterWiseViewModel(userid);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<EcruFabricIssuenceViewModel>> EcruFabricIssuenceViewModel(long userid)
        {
            try
            {
                var list = await _repo.EcruFabricIssuenceViewModel(userid);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<GetFabricDetailByLot>> GetFabricDetailByLot(long lotNo)
        {
            try
            {
                var list = await _repo.GetFabricDetailByLot(lotNo);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<EcruStockSummary_YarnCountGsm_RepositoryViewModal>> EcruStockSummary_YarnCountGsm(long userid)
        {
            try
            {
                var list = await _repo.EcruStockSummary_YarnCountGsm_RepositoryViewModal(userid);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //public long DyeChemicalUpdateDr(long headerid, DateTime? invoicedate)
        //{

        //    try
        //    {
        //        var id = _repo.DyeChemicalUpdateDr(headerid, fairprice, igprefno, qtycr, trtype, invoiceno, dtreno, invoicedate);
        //        return id;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

    }


}

