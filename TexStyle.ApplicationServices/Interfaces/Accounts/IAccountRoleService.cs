using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Interfaces.Accounts {
    public interface IAccountRoleService : IDefaultService<AccountRole> {
        Task<List<AccountRole>> GetUserRoles();
    }
}
