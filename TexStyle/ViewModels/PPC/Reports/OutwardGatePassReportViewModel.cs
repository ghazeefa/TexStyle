using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.Core.Premisis;

namespace TexStyle.ViewModels.PPC.Reports 
{
   public class OutwardGatePassReportViewModel : DefaultViewModel
    {
        public OutwardGatePassReportViewModel()
        {
            OutwardGatePassDetails = new List<OutwardGatePassDetail>();
        }
        public long Id { get; set; }
        public DateTime OgpDate { get; set; }

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


        public long? InvoiceNo { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public string InvoiceDescription { get; set; }

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
                _isConfirm = value;
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
        [DisplayName("AfterFinishing?")]

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


        [DisplayName("ComercialFinishingDispatch?")]

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
        public long? ActivityTypeId { get; set; }

        [ForeignKey(nameof(ActivityTypeId))]
        public virtual ActivityType ActivityType { get; set; }

        public long? YarnTypeId { get; set; }
        [ForeignKey(nameof(YarnTypeId))]
        public virtual YarnType YarnType { get; set; }

        public string ReceivingPerson { get; set; }
        public string VehicleNo { get; set; }
        public string IDCard { get; set; }

        public bool IsReCheck { get; set; }

        public long? PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }

        public ICollection<OutwardGatePassDetail> OutwardGatePassDetails { get; set; }



        public long? FabricTypeId { get; set; }
        [ForeignKey(nameof(FabricTypeId))]
        public virtual FabricTypes FabricTypes { get; set; }
        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }
        public bool? IsYarn { get; set; }


        public long? OGPReffNO { get; set; }
        public string BilityNo { get; set; }
        public string SerialNo { get; set; }
        public decimal? TotalWeight { get; set; }
        public int? LotNo { get; set; }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }

    }
}
