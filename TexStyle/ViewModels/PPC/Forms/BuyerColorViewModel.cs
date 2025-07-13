using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class BuyerColorViewModel
    {
        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long BuyerId { get; set; }
    }
}
