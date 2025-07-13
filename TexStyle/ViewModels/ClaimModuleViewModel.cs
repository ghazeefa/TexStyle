using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels {
    public class ClaimModuleViewModel {
        public ClaimModuleViewModel() {
            ClaimsList = new List<ClaimsPermissionViewModel>();
        }
        public long Id { get; set; }
        public string ModuleName { get; set; }
        public List<ClaimsPermissionViewModel> ClaimsList { get; set; }
        public List<SelectListItem> ClaimsSelectList { get; set; }
    }
}
