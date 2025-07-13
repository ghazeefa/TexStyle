using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.Premisis;

namespace TexStyle.Core.PPC
{
    public class InwardGatePass : DefaultEntity
    {

        public InwardGatePass()
        {
            InwardGatePassDetails = new List<InwardGatePassDetail>();
        }


        [DisplayName("Sno")]
        public long Id { get; set; }
        [DisplayName("IGP Date")]// this is issue date
        public DateTime IgpDate { get; set; }
        [DisplayName("Bility No")]
        public string BilityNo { get; set; }
        //  public int IgpNo { get; set; }



        //public bool IsReprocessed { get; set; }
        [DisplayName("IGP No")]
        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTr { get; set; }

        [DisplayName("Party")]
        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }

        [DisplayName("Process Activity Type")]
        public long? ActivityTypeId { get; set; }
        [ForeignKey(nameof(ActivityTypeId))]
        public virtual ActivityType ActivityType { get; set; }




        private Boolean _IsReturnfromParty = false;
        [DisplayName("Return From Party")]
        public Boolean IsReturnfromParty
        {
            get
            {
                return _IsReturnfromParty;
            }
            set
            {
                _IsReturnfromParty = value;
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
        [DisplayName("Return Finishing?")]

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


        private Boolean _IsReturnToParty = false;
        [DisplayName("Return To Party")]
        public Boolean IsReturnToParty
        {
            get
            {
                return _IsReturnToParty;
            }
            set
            {
                _IsReturnToParty = value;
            }
        }



        private Boolean _isConfirm = false;
        [DisplayName("Confirm?")]
        public Boolean IsConfirm
        {
            get
            {
                return _isConfirm;
            }
            set
            {
                _isConfirm = true;
            }
        }

        private Boolean _isCancel = false;
        [DisplayName("Confirm?")]
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

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

        //public long? GateOGPId { get; set; }
        //[ForeignKey(nameof(GateOGPId))]
        //public virtual GateOgp GateOgp { get; set; }

        public ICollection<InwardGatePassDetail> InwardGatePassDetails { get; set; }
        public bool ? IsYarn { get; set; }

        public long? GateReffId { get; set; }

        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }

    }
}







