using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.CS.Forms
{
    public class TrViewModel
    {

        public long? Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("TR No")]
        public long? Sno { get; set; }
        [DisplayName("Tr Type")]
        public long? TrType { get; set; }
        [DisplayName("Invoice No")]
        public long? InvoiceNo { get; set; }
        [DisplayName("Invoice Date")]
        public DateTime? InvoiceDate { get; set; }
        [DisplayName("Fair Price")]
        public decimal? FairPrice { get; set; }
        [DisplayName("DTRE No")]
        public string DTRENo { get; set; }
        [DisplayName("Electricity Charges")]
        public decimal? ElectricityCharges { get; set; }
        [DisplayName("Cable Charges")]
        public decimal? CableCharges { get; set; }
        [DisplayName("Drum Charges")]
        public decimal? DrumCharges { get; set; }
        [DisplayName("Labour Charges")]
        public decimal? LabourCharges { get; set; }

        [DisplayName("Confirm?")]
        private bool _isConfirm { get; set; }
        [DisplayName("IGP Refference No")]
        public long? IGPReffNo { get; set; }
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTr { get; set; }
        [DisplayName("Party")]
        //[Required]
        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
        public long? DyeChemicalXrefTrId { get; set; }
        public virtual DyeChemicalTr DyeChemicalXrefTr { get; set; }
        public List<GateTrDetail> GateTrDetails { get; set; }

        [DisplayName("Cancel?")]
        public bool IsCancel { get; set; }
        //[Range(1 , 2343233)]

        public long Rate { get; set; }


    }
}
