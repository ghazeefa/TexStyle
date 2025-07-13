using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.PPC {
    public class IGPViewModel {
 

        [DisplayName("Sno No")]
        public long? Id { get; set; }
        [DisplayName("IGP Date")]// this is issue date
        [Required]
        public DateTime IgpDate { get; set; }
        [DisplayName("Bility No")]
        public string BilityNo { get; set; }
        //  public int IgpNo { get; set; }
        [DisplayName("Process Activity Type")]
        [Required]
        public long? ActivityTypeId { get; set; }
        [DisplayName("Party")]
        public long? PartyId { get; set; }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }


        //public bool IsReprocessed { get; set; }
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }


        [DisplayName("Return From Party")]
        public bool IsReturnfromParty { get; set; }
        [DisplayName("ReWax/ReCheck?")]
        public bool IsReWaxRecheck { get; set; }
        [DisplayName("ReProcess?")]
        public bool IsReprocessed { get; set; }
        [DisplayName("Return To Party")]
        public bool IsReturnToParty { get; set; }
        [DisplayName("Return Finishing?")]
        public bool IsAfterFinishing { get; set; }

        [DisplayName("ForFinishing ? ")]
        public bool IsForFinishing { get; set; }

        [DisplayName("Comercial Finishing?")]

        public bool IsForComercialFinishing { get; set; }

        [DisplayName("Confirm?")]
        public bool IsConfirm { get; set; }


        [DisplayName("Cancel?")]
        public bool IsCancel { get; set; }
        [DisplayName("Without OGP?")]
        public bool IsWithoutOGP { get; set; }
        public int? BranchId { get; set; }

        public bool? IsYarn { get; set; }
        public long? GateReffId { get; set; }


    }
}
