using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.Gate
{
    public class GateTrViewModel
    {
        public long Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public long Sno { get; set; }
        [DisplayName("Driver Name")]
        [Required]
        public string DriverName { get; set; }
        [Required]
        [DisplayName("Vehicle No")]
        public string VehicleNo { get; set; }
        [DisplayName("CNIC No")]
        public string NICNo { get; set; }
        [DisplayName("Emp No")]
        public string EmpNo { get; set; }
        [DisplayName("Xref")]
        public long Xref { get; set; }
        //// public decimal? Rate { get; set; }
        // [DisplayName("Is Reprocess")]
        // public bool IsReprocessed { get; set; }
        [DisplayName("Return From Party?")]
        public bool IsReturnFromParty { get; set; }   
            
        [DisplayName("Reprocessed?")]
        public bool IsReprocessed { get; set; }  
        
        [DisplayName("Return Finishing?")]
        public bool IsAfterFinishing { get; set; }
           
        [DisplayName("ForFinishing?")]
        public bool IsForFinishing { get; set; }


        [DisplayName("Without OGP?")]
        public bool IsWithoutOGP { get; set; }
        [DisplayName("ReWax/ReCheck?")]
        public bool IsReWaxRecheck { get; set; }

      
        [DisplayName("Comercial Finishing Dispatch?")]
        public bool IsAfterComercialFinishing { get; set; }


        [DisplayName("Comercial Finishing?")]

            public bool IsForComercialFinishing { get; set; }

        [DisplayName("Confirm?")]
        public bool IsConfirm { get; set; }
        [Required]
        [DisplayName("Party")]
        public long PartyId { get; set; }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }

        [Required]
        [DisplayName("IGP Refference No")]
        public long? IGPReffNo { get; set; }

        public long? GateTrId { get; set; }
        [DisplayName("Gate Activity")]
        [Required]
        public long GateActivityTypeId { get; set; }
        [DisplayName("Gate Pass Type")]
        
        public long? GatePassTypeId { get; set; }

        public long? OutwardGatePassId { get; set; }
        [DisplayName("Branch Code")]
        public int ? BranchId { get; set; }


        public bool? IsYarn { get; set; }
        public string BillityNo { get; set; }

    }
}
