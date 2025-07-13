using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.Core.PPC;
using TexStyle.Core.Premisis;

namespace TexStyle.Core.Gate
{
   public class GateTr:DefaultEntity
    {
        public GateTr()
        {

            GateTrDetails = new List<GateTrDetail>();
        }


        public long Id { get; set; }
        [DisplayName("IGP Date")]
        public DateTime Date { get; set; }
        [DisplayName("IGP No")]
        public long Sno { get; set; }
        //[DisplayName("Description")]
        //public long Description { get; set; }
        [DisplayName("Driver Name")]
        public string DriverName { get; set; }
        [DisplayName("Vehicle No")]
        public string VehicleNo { get; set; }
        [DisplayName("CNIC No")]
        public string NICNo { get; set; }
        [DisplayName("EmpNo No")]
        public string EmpNo { get; set; }
        public long? IGPReffNo { get; set; }
        public long Xref { get; set; }
        [DisplayName("Return From Party?")]

        private Boolean _IsReturnFromParty = false;
        public Boolean IsReturnFromParty
        {
            get
            {
                return _IsReturnFromParty;
            }
            set
            {
                _IsReturnFromParty = value;
            }
        }
        

       [DisplayName("ReprocessFinishing?")]

        private Boolean _IsForFinishing = false;
        public Boolean IsForFinishing
        {
            get
            {
                return _IsForFinishing;
            }
            set
            {
                _IsForFinishing = value;
            }
        }
        [DisplayName("ReturnFinishing?")]

        private Boolean _IsAfterFinishing = false;
        public Boolean IsAfterFinishing
        {
            get
            {
                return _IsAfterFinishing;
            }
            set
            {
                _IsAfterFinishing = value;
            }
        }

        [DisplayName("Comercial Finishing?")]

        private Boolean _IsForComercialFinishing = false;
        public Boolean IsForComercialFinishing
        {
            get
            {
                return _IsForComercialFinishing;
            }
            set
            {
                _IsForComercialFinishing = value;
            }
        }


        [DisplayName("Comercial Finishing Dispatch?")]

        private Boolean _IsAfterComercialFinishing = false;
        public Boolean IsAfterComercialFinishing
        {
            get
            {
                return _IsAfterComercialFinishing;
            }
            set
            {
                _IsAfterComercialFinishing = value;
            }
        }


        private Boolean _IsReWaxRecheck = false;
        [DisplayName("ReWax/ReCheck?")]
        public Boolean IsReWaxRecheck
        {
            get
            {
                return _IsReWaxRecheck;
            }
            set
            {
                _IsReWaxRecheck = value;
            }
        }

        private Boolean _IsReprocess = false;
        [DisplayName("ReProcess?")]
        public Boolean IsReprocessed
        {
            get
            {
                return _IsReprocess;
            }
            set
            {
                _IsReprocess = value;
            }
        }



        [DisplayName("Without OGP?")]
        private Boolean _IsWithoutOGP = false;
        public Boolean IsWithoutOGP
        {
            get
            {
                return _IsWithoutOGP;
            }
            set
            {
                _IsWithoutOGP = value;
            }
        }

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
        //Gate Tr for OGP 
        [DisplayName("OGP No")]
        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTrs { get; set; }


        [DisplayName("Party")]
        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }

        [DisplayName("Gate Activity")]
        public long? GateActivityTypeId { get; set; }
        [ForeignKey(nameof(GateActivityTypeId))]
        public virtual GateActivityType GateActivityType { get; set; }

        public long? OutwardGatePassId { get; set; }
        [ForeignKey(nameof(OutwardGatePassId))]
        public virtual OutwardGatePass OutwardGatePass { get; set; }

        //public long? LoanTakenReturnOutTrId { get; set; }
        //[ForeignKey(nameof(LoanTakenReturnOutTrId))]
        //public virtual LoanTakenReturnOutTr LoanTakenReturnOutTr { get; set; }

        //public long? LoanPartyGivenOutTrId { get; set; }
        //[ForeignKey(nameof(LoanPartyGivenOutTrId))]
        //public virtual LoanPartyGivenOutTr LoanPartyGivenOutTr { get; set; }

        public long? GetDyeChemicalTrId { get; set; }
        [ForeignKey(nameof(GetDyeChemicalTrId))]
        public virtual DyeChemicalTr GetDyeChemicalTr { get; set; }

        public virtual ICollection<GateTrDetail> GateTrDetails { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }
        public bool? IsYarn { get; set; }
        public string BillityNo { get; set; }
        [DisplayName("Gate Activity")]
        public long? GatePassTypeId { get; set; }
        [ForeignKey(nameof(GatePassTypeId))]
        public virtual GatePassType GatePassType { get; set; }

    }
}
