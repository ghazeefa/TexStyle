using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TexStyle.Extensions {
    internal static class ClaimExtensions {
        public static string GetUserId(this IIdentity identity) {
            if (identity == null)
                return null;

            var id = (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.NameIdentifier);
            return id;
        }

        public static bool? IsTypeOf(this IIdentity identity, string role) {
            if (identity == null)
                return null;
            ////// create service collection
            //var services = new ServiceCollection();
            ////services.AddLogging();
            //// get the provider
            //var provider = UOWRegisteration.RegisterAll(services).BuildServiceProvider();
            //// get all the services you wanna use
            bool? isTypeOf = null;
            //var provider = Startup.ServiceProvider;
            //var _uow = provider.GetService<IUnitOfWork>();
            //var id = (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.NameIdentifier);

            //var acc = _uow.AccountService.GetById(id, true);
            //if (acc != null) {
            //    isTypeOf = acc.IsTypeof(role);
            //}
            return isTypeOf;
        }

        internal static string FirstOrNull(this ClaimsIdentity identity, string claimType) {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }
    }
}
