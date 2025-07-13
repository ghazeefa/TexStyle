using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
using TexStyle.Core.Premisis;
using TexStyle.Core.YD;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC
{
    class MachineInventory : DefaultEntity
    {
        [Key]
        public long Id { get; set; } 
        public int  Quantity { get; set; }
        //public DateTime IssuedDate { get; set; }
        public string Description { get; set; }
        public int? MachineTypeId { get; set; }
        [ForeignKey(nameof(MachineTypeId))]
        public virtual MachineType MachineType { get; set; }
    }
}
