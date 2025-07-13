using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Premisis;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC
{
   public class IssueNote :DefaultEntity
    {
        [DisplayName("Issue No")]
        public long Id { get; set; }

        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }

        //public int IssueTypeId { get; set; }

        [DisplayName("IR No/OGP No")]
        public int IRNO { get; set; }

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



        public ICollection<IssueNoteDetail> IssueNoteDetail { get; set; }


        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Account Account { get; set; }

    }
}
