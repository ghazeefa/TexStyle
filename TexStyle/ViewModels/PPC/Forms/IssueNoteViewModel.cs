using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class IssueNoteViewModel
    {
        public long? Id { get; set; }

        public DateTime IssueDate { get; set; }
        public int IRNO { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsReprocessed { get; set; }
        public int IssueTypeId { get; set; }
        public int? BranchId { get; set; }
        public int? UserId { get; set; }
    }
}
