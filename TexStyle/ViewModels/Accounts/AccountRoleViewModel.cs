using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.Accounts {
    public class AccountRoleViewModel {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<string> Claims { get; set; }
    }
}
