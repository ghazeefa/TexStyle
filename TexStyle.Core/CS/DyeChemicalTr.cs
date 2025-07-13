using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
    public class DyeChemicalTr:DefaultEntity
    {
        public DyeChemicalTr()
        {
            DyeChemicalTrDetails = new List<DyeChemicalTrDetail>();
        }

        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("TR No")]
        public long Sno { get; set; }
        [DisplayName("Tr Type")]
        public long TrType { get; set; }
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
        public string Remarks { get; set; }
        [DisplayName("IGP Reff No")]
        public long? IGPReffNo { get; set; }
        //[DisplayName("Description")]
        //public string Description { get; set; }
        [DisplayName("Confirm?")]
        private Boolean _isConfirm = false;
        public Boolean IsConfirm
        {
            get
            {
                return _isConfirm;
            }
            set
            {
                _isConfirm = value;
            }
        }
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTr { get; set; }

        [DisplayName("Xref No")]
        public long? DyeChemicalXrefTrId { get; set; }
        [ForeignKey(nameof(DyeChemicalXrefTrId))]
        public virtual DyeChemicalTr DyeChemicalXrefTr { get; set; }

        [DisplayName("Party")]
        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }

        public long? RecipeId { get; set; }
        public DateTime? RecipieIssuanceDate { get; set; }
       // public DateTime IssuanceDate { get; set; }
        public bool? IsCompleteIssued { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public virtual Recipe Recipe { get; set; }
     
        public ICollection<DyeChemicalTrDetail> DyeChemicalTrDetails { get; set; }

        private Boolean _isCancel = false;
        [DisplayName(" Is Cancle?")]
        public Boolean IsCancel
        {
            get
            {
                return _isCancel;
            }
            set
            {
                _isCancel = value;
            }
        }

        //[DisplayName("Electricity Charges")]
        //public decimal? ElectricityCharges { get; set; }
        //[DisplayName("Cable Charges")]
        //public decimal? CableCharges { get; set; }
        //[DisplayName("Drum Charges")]
        //public decimal? DrumCharges { get; set; }
        //[DisplayName("Labour Charges")]
        //public decimal? LabourCharges { get; set; }

    }
}
