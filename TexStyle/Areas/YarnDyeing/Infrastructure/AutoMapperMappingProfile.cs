using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.Analysis;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Core.YD;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Accounts;
using TexStyle.ViewModels.Analysis;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;
using TexStyle.ViewModels.Gate;
using TexStyle.ViewModels.GATE;
using TexStyle.ViewModels.PPC;
using TexStyle.ViewModels.PPC.Forms;
using TexStyle.ViewModels.PPC.Reports;
using TexStyle.ViewModels.YD;

namespace TexStyle.Infrastructure {
    public class AutoMapperMappingProfile : Profile {

        public AutoMapperMappingProfile() {

            //CreateMap<YarnQualityViewModel, YarnQuality>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<YarnQuality, YarnQualityViewModel>();

            //CreateMap<YarnTypeViewModel, YarnType>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<YarnType, YarnTypeViewModel>();

            //CreateMap<BagMarkingViewModel, BagMarking>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<BagMarking, BagMarkingViewModel>();

            //CreateMap<ConeMarkingViewModel, ConeMarking>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<ConeMarking, ConeMarkingViewModel>();

            //CreateMap<StoreLocationViewModel, StoreLocation>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<StoreLocation, StoreLocationViewModel>();

            //CreateMap<PartyViewModel, Party>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<Party, PartyViewModel>();

            //CreateMap<YarnCountViewModel, YarnCount>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<YarnCount, YarnCountViewModel>();

            //CreateMap<BuyerColorViewModel, BuyerColor>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<BuyerColor, BuyerColorViewModel>();

            //CreateMap<BuyerViewModel, Buyer>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<Buyer, BuyerViewModel>();

            //CreateMap<ShadeViewModel, Shade>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<Shade, ShadeViewModel>();

            //CreateMap<ActivityTypeViewModel, ActivityType>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            //CreateMap<ActivityType, ActivityTypeViewModel>();

            // Basic Types
            this.MappBasicTypes();

            // configure custom ones 
            CreateMap<BuyerColorViewModel, BuyerColor>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id : 0))
                .ForMember(des => des.Buyer, opt => opt.MapFrom(src => new Buyer { Id = src.BuyerId }));
            CreateMap<BuyerColor, BuyerColorViewModel>()
                .ForMember(des => des.BuyerId, opt => opt.MapFrom(src => src.BuyerId));


            CreateMap<IGPViewModel, InwardGatePass>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id : 0));
            CreateMap<InwardGatePass, IGPViewModel>();


            CreateMap<IGPDetailViewModel, InwardGatePassDetail>().ForMember(des => des.Id, src => src.MapFrom(x => x.Id.HasValue ? x.Id : 0));
            CreateMap<InwardGatePassDetail, IGPDetailViewModel>();


            #region Chemical store

            CreateMap<TrDetailViewModel, LCImportInTrDetail>().ForMember(des => des.LCImportInTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<LCImportInTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.LCImportInTrId));


            CreateMap<TrDetailViewModel, LCImportInTrDetail>().ForMember(des => des.GateTrDetailId, src => src.MapFrom(x => x.GateTrDetailId));
            CreateMap<LCImportInTrDetail, TrDetailViewModel>().ForMember(des => des.GateTrDetailId, src => src.MapFrom(x => x.GateTrDetailId));

            CreateMap<TrDetailViewModel, LoanPartyReturnInTrDetail>().ForMember(des => des.LoanPartyReturnInTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<LoanPartyReturnInTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.LoanPartyReturnInTrId));

            CreateMap<TrDetailViewModel, LoanTakenInTrDetail>().ForMember(des => des.LoanTakenInTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<LoanTakenInTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.LoanTakenInTrId));

            CreateMap<TrDetailViewModel, LocalPurchaseInTrDetail>().ForMember(des => des.LocalPurchaseInTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<LocalPurchaseInTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.LocalPurchaseInTrId));

            CreateMap<TrDetailViewModel, LocalPurchaseInTrDetail>().ForMember(des => des.GateTrDetailId, src => src.MapFrom(x => x.GateTrDetailId));
            CreateMap<LocalPurchaseInTrDetail, TrDetailViewModel>().ForMember(des => des.GateTrDetailId, src => src.MapFrom(x => x.GateTrDetailId));

            CreateMap<TrDetailViewModel, InterUnitOutTrDetail>().ForMember(des => des.InterUnitOutTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<InterUnitOutTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.InterUnitOutTrId));


            CreateMap<TrDetailViewModel, LoanPartyGivenOutTrDetail>().ForMember(des => des.LoanPartyGivenOutTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<LoanPartyGivenOutTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.LoanPartyGivenOutTrId));

            CreateMap<TrDetailViewModel, LoanTakenReturnOutTrDetail>().ForMember(des => des.LoanTakenReturnOutTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<LoanTakenReturnOutTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.LoanTakenReturnOutTrId));
            //CreateMap<TrDetailViewModel, LoanTakenReturnOutTrDetail>().ForMember(des => des.LoanTakenInTrDetailId, src => src.MapFrom(x => x.DetailId));
            //CreateMap<LoanTakenReturnOutTrDetail, TrDetailViewModel>().ForMember(des => des.DetailId, src => src.MapFrom(x => x.LoanTakenInTrDetailId));


            CreateMap<TrDetailViewModel, ChemicalDilutionTrDetail>().ForMember(des => des.ChemicalDilutionTrId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<ChemicalDilutionTrDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.ChemicalDilutionTrId));

            CreateMap<TrDetailViewModel, DyesChemicalOpenningDetail>().ForMember(des => des.DyesChemicalOpenningId, src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<DyesChemicalOpenningDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.DyesChemicalOpenningId));

            CreateMap<TrDetailViewModel, StoreReturnNoteDetail>().ForMember(des => des.StoreReturnNoteId,  src => src.MapFrom(x => x.DyeChemicalTrId));
            CreateMap<StoreReturnNoteDetail, TrDetailViewModel>().ForMember(des => des.DyeChemicalTrId, src => src.MapFrom(x => x.StoreReturnNoteId));

            CreateMap<TrDetailViewModel, StoreReturnNoteDetail>().ForMember(des => des.LocalPurchaseInTrDetailId, src => src.MapFrom(x => x.GateTrDetailId));
            CreateMap<StoreReturnNoteDetail, TrDetailViewModel>().ForMember(des => des.GateTrDetailId, src => src.MapFrom(x => x.LocalPurchaseInTrDetailId));


            #endregion
        }

        private List<(dynamic viewModel, dynamic dto)> types = new List<(dynamic viewModel, dynamic dto)> {
            #region PPC
             AddType(typeof(knittingPartyViewModel),typeof(knittingParty)),
             AddType(typeof(RollMarkingViewModel),typeof(RollMarking)),
             AddType(typeof(FabricQualityViewModel),typeof(FabricQuality)),

            AddType(typeof(PartyViewModel),typeof(Party)),
            AddType(typeof(BuyerViewModel),typeof(Buyer)),
          //  AddType(typeof(ShadeViewModel),typeof(Shade)),
            AddType(typeof(BuyerColorViewModel),typeof(BuyerColor)),
            AddType(typeof(YarnTypeViewModel),typeof(YarnType)),
            AddType(typeof(BagMarkingViewModel),typeof(BagMarking)),
            AddType(typeof(YarnQualityViewModel),typeof(YarnQuality)),
            AddType(typeof(ConeMarkingViewModel),typeof(ConeMarking)),
            AddType(typeof(ActivityTypeViewModel),typeof(ActivityType)),
            AddType(typeof(StoreLocationViewModel),typeof(StoreLocation)),
            AddType(typeof(YarnManufacturerViewModel),typeof(YarnManufacturer)),
            AddType(typeof(SeasonViewModel),typeof(Season)),
            AddType(typeof(PurchaseOrderViewModel),typeof(PurchaseOrder)),
            AddType(typeof(PPCPlanningViewModel),typeof(PPCPlanning)),
            AddType(typeof(IssueNoteViewModel),typeof(IssueNote)),
            AddType(typeof(IssueNoteDetailViewModel),typeof(IssueNoteDetail)),

            AddType(typeof(ReturnNoteViewModel),typeof(ReturnNote)),
            AddType(typeof(ReturnNoteDetailViewModel),typeof(ReturnNoteDetail)),
            AddType(typeof(ReprocessViewModel),typeof(Reprocess)),
            AddType(typeof(OGPViewModel),typeof(OutwardGatePass)),
            AddType(typeof(OGPDetailViewModel),typeof(OutwardGatePassDetail)),
            AddType(typeof(FabricTypesViewModel),typeof(FabricTypes)),
           AddType(typeof(OutwardGatePassReportViewModel),typeof(OutwardGatePass)),
	#endregion 

            #region Yarn Dyeing

            AddType(typeof(DefaultItemViewModel),typeof(MachineType)),
            AddType(typeof(MachineViewModel),typeof(Machine)),

            AddType(typeof(DefaultItemViewModel),typeof(ProcessType)),
            AddType(typeof(DyeViewModel),typeof(Dye)),
            AddType(typeof(ChemicalViewModel),typeof(Chemical)),
            AddType(typeof(CostingViewModel),typeof(Costing)),

            AddType(typeof(DefaultItemViewModel),typeof(RecipeStep)),
            AddType(typeof(RecipeFormatHeaderViewModel),typeof(RecipeFormatHeader)),


            AddType(typeof(RecipeViewModel),typeof(Recipe)),
            AddType(typeof(RecipeDetailViewModel),typeof(RecipeDetail)),
            AddType(typeof(RecipeLPSViewModel),typeof(RecipeLPS)),
            AddType(typeof(RecipeFormatDetailViewModel),typeof(RecipeFormatDetail)),
            AddType(typeof(StickerViewModel),typeof(Sticker)),

            #endregion

            #region Chemical store
             AddType(typeof(SupplierViewModel),typeof(Supplier)),

             AddType(typeof(LCImportInTrViewModel),typeof(LCImportInTr)),

             AddType(typeof(LoanPartyReturnInTrViewModel),typeof(LoanPartyReturnInTr)),

             AddType(typeof(LoanTakenInTrViewModel),typeof(LoanTakenInTr)),

             AddType(typeof(LocalPurchaseInTrViewModel),typeof(LocalPurchaseInTr)),

             AddType(typeof(InterUnitOutTrViewModel),typeof(InterUnitOutTr)),

               AddType(typeof(LoanPartyGivenOutTrViewModel),typeof(LoanPartyGivenOutTr)),

              AddType(typeof(LoanTakenReturnOutTrViewModel),typeof(LoanTakenReturnOutTr)),


              AddType(typeof(ChemicalDilutionTrViewModel),typeof(ChemicalDilutionTr)),

              AddType(typeof(ChemicalIssuanceRecipeTrViewModel),typeof(ChemicalIssuanceRecipeTr)),

              AddType(typeof(DyesChemicalOpenningViewModel),typeof(DyesChemicalOpenning)),
              AddType(typeof(DyesChemicalOpenningDetailViewModel),typeof(DyesChemicalOpenningDetail)),

                 AddType(typeof(StoreReturnNoteViewModel),typeof(StoreReturnNote)),
             AddType(typeof(TrViewModel),typeof(DyeChemicalTr)),
              AddType(typeof(TrDetailViewModel),typeof(DyeChemicalTrDetail)),

   
            #endregion

            #region Gate
             //AddType(typeof(GateIGPViewModel),typeof(GateIgp)),
             //AddType(typeof(GateIGPDetailViewModel),typeof(GateIgpDetail)),
             //AddType(typeof(GateOgpViewModel),typeof(GateOgp)),
             //AddType(typeof(GateOgpDetailViewModel),typeof(GateOgpDetail)),
             AddType(typeof(GateTrViewModel),typeof(GateTr)),
             AddType(typeof(GateTrDetailViewModel),typeof(GateTrDetail)),

            #endregion

            #region Analysis
              AddType(typeof(DefectViewModel),typeof(Defect)),
            AddType(typeof(AnalysisTypeViewModel),typeof(AnalysisType)),
            AddType(typeof(DefectAnalysisViewModel),typeof(DefectAnalysis)),
            #endregion


            AddType(typeof(AccountViewModel),typeof(Account)),
             AddType(typeof(ReportFilterViewModel),typeof(ReportFilter)),
        };

        private void MappBasicTypes() {
            types.ForEach((t) => {
                CreateMap(t.viewModel, t.dto);
                CreateMap(t.dto, t.viewModel);
            });
        }

        private static (dynamic viewModel, dynamic dto) AddType(dynamic viewmodel, dynamic model) => (viewmodel, model);
    }
}
