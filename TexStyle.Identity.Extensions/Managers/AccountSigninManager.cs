using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.Identity.Extensions.Managers {
    public class AccountSigninManager : SignInManager<Account> {
        public AccountSigninManager(UserManager<Account> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<Account> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<Account>> logger, 
            IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes) {
        }

        public override async Task SignInAsync(Account user, bool isPersistent, string authenticationMethod = null) {
            var userId = await UserManager.GetUserIdAsync(user);
            //_mediator.Publish(new UserSignedIn { UserId = long.Parse(userId) });
            await base.SignInAsync(user, isPersistent, authenticationMethod);
        }
    }
}
