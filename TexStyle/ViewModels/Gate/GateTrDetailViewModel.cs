using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.Gate
{
    public class GateTrDetailViewModel
    {
        public long Id { get; set; }
        [DisplayName("Qty Dr")]
        public decimal? QtyDr { get; set; }
        [DisplayName("Qty Cr")]
        public decimal? QtyCr { get; set; }
        [DisplayName("Packet")]
        public decimal? Packet { get; set; }
        public decimal? Rate { get; set; }
        public string Description { get; set; }

        public long GateDetailXrefId { get; set; }
        [DisplayName("Dye")]
        public long? DyeId { get; set; }
        [DisplayName("Chemical")]
        public long? ChemicalId { get; set; }
        public decimal? Bags { get; set; }
        [DisplayName("No of Rolls")]
        public decimal? NoOfRolls { get; set; }
        public long YarnTypeId { get; set; }
        [DisplayName("Select Fabric Type")]
        public long FabricTypeId { get; set; }
        [DisplayName("Header Id")]
        public long GateTrId { get; set; }
        public long? OGPGateTrDetailId { get; set; }
        public decimal AvailableKgs { get; set; }
        public long? OutwardGatePassDetailId { get; set; }

        public long? LoanTakenReturnOutTrDetailId { get; set; }

        public long? LoanPartyGivenOutTrDetailId { get; set; }
        public long? BillityNo { get; set; }
       

    }
}
