using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Premisis;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC
{
    public class ReturnNote : DefaultEntity
    {
        public ReturnNote()
        {
            ReturnNoteDetails = new List<ReturnNoteDetail>();
        }
        public long Id { get; set; }
        public DateTime ReturnDate { get; set; }
        //  public bool Confirmed { get; set; }
        ////  public int ReturnTypeId { get; set; }
        //  [DisplayName("Return From Party")]
        //  public bool IsReprocessed { get; set; }
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



        public ICollection<ReturnNoteDetail> ReturnNoteDetails { get; set; }
        /// <summary>
        /// Party Specific Properties
        /// </summary>
        //   public long? IRNO { get; set; }
        // public bool ReturnFromParty { get; set; }


        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Account Account { get; set; }
        public bool? IsYarn { get; set; }
        public decimal? TotalWeight { get; set; }
        public int? LotNo { get; set; }

        public long? BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }
    }
}
