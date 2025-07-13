using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class PartyViewModel
    {
        public long? Id { get; set; }
        public long SubAccount { get; set; }
        [Required]
        public string Name { get; set; }
        public long ControlAccount { get; set; }
        public bool IsHeader { get; set; }
    }
}
