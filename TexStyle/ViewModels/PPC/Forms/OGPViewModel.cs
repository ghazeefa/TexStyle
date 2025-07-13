using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class OGPViewModel
    {
        [DisplayName("OGP Id")]
        public int Id { get; set; }
        [DisplayName("OGP Date")]
        public DateTime OGPDate { get; set; }
        [DisplayName("Yarn Type")]
        public int? YarnTypeId { get; set; }
        [Required]
        public string ReceivingPerson { get; set; }
        [Required]
        public string VehicleNo { get; set; }
        [Required]
        public string IDCard { get; set; }
        [Required]
        [DisplayName("Process Activity")]
        public int ActivityTypeId { get; set; }
        public bool IsReCheck { get; set; }
        [DisplayName("Party")]
        public long? PartyId { get; set; }
        [DisplayName("Invoice No")]
        public long? InvoiceNo { get; set; }
        [DisplayName("Invoice Amount")]
        public decimal? InvoiceAmount { get; set; }
        [DisplayName("Invoice Description")]
        public string InvoiceDescription { get; set; }
        [DisplayName("Return To Party")]
        public bool IsReturnToParty { get; set; }
        public bool IsConfirm { get; set; }
        //public bool IsConfirmed { get; set; }
        public bool IsReprocessed { get; set; } 
        public bool IsCancled{ get; set; } // phir yaha
        public long? FabricTypeId { get; set; }
        public long? OGPReffNO { get; set; }

        public string BilityNo { get; set; }

        [DisplayName("Comercial Finishing Dispatch?")]
        public bool IsAfterComercialFinishing { get; set; }
        [DisplayName("After Finishing?")]
        public bool IsAfterFinishing { get; set; }

        [DisplayName("For Finishing?")]
        public bool IsForFinishing { get; set; }

        [DisplayName("Total Weight")]
        public decimal? TotalWeight { get; set; }

        [DisplayName("Lot No")]
        public int? LotNo { get; set; }

        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }


        [DisplayName("SerialNo")]
        public string SerialNo { get; set; }

    }
}
