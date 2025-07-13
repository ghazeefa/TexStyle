using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TexStyle.ApplicationServices.Implementation.Accounts;
using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.ApplicationServices.Implementation;
using TexStyle.ApplicationServices.Implementation.PPC;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.DomainServices;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.Identity.Extensions.Managers;
using System.Reflection;
using System.Linq;

namespace TexStyle.ApplicationServices {
    public static class ApplicationServiceRegistration {

        public static IServiceProvider GlobalServiceProvider { get; private set; }
        public static IServiceCollection RegisterAppServices(this IServiceCollection services) {
            DomainServiceRegistration.RegisterRepositories(services);

            var allServiceTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains("ApplicationServices"));

            foreach (var intfc in allServiceTypes.Where(t => t.IsInterface)) {
                var impl = allServiceTypes.FirstOrDefault(c => c.IsClass && intfc.Name.Substring(1) == c.Name);
                if (impl != null) services.AddScoped(intfc, impl);
            }


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            GlobalServiceProvider = services.BuildServiceProvider();
            // Identity Services Configuration
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IAccountRoleService, AccountRoleService>();
            //services.AddScoped<IAccountLoginService, AccountLoginService>();
            //services.AddScoped<IAccountClaimService, AccountClaimService>();
            //services.AddScoped<IAccountRoleClaimService, AccountRoleClaimService>();
            //services.AddScoped<IAccountUserRoleService, AccountUserRoleService>();
            //services.AddScoped<IAccountUserClaimService, AccountUserClaimService>();
            //services.AddScoped<IAccountUserLoginService, AccountUserLoginService>();
            //services.AddScoped<IAccountUserTokenService, AccountUserTokenService>();

            //services.AddScoped<IYarnTypeService, YarnTypeService>();
            //services.AddScoped<IYarnQualityService, YarnQualityService>();
            //services.AddScoped<IStoreLocationService, StoreLocationService>();
            //services.AddScoped<IBagMarkingService, BagMarkingService>();
            //services.AddScoped<IConeMarkingService, ConeMarkingService>();
            //services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            //services.AddScoped<IPartyService, PartyService>();
            //services.AddScoped<IBuyerService, BuyerService>();
            //services.AddScoped<IBuyerColorService, BuyerColorService>();

            //services.AddScoped<IShadeService, ShadeService>();
            //services.AddScoped<IOrderActivityTypeService, OrderActivityTypeService>();
            //services.AddScoped<IYarnManufacturerService, YarnManufacturerService>();
            //services.AddScoped<ISeasonService, SeasonService>();

            //services.AddScoped<IOGPService, OGPService>();

            //services.AddScoped<IIGPService, IGPService>();
            //services.AddScoped<IIGPDetailService, IGPDetailService>();

            //services.AddScoped<IPPCPlanningService, PPCPlanningService>();

            //services.AddScoped<IIssueNoteService, IssueNoteService>();
            //services.AddScoped<IIssueNoteDetailService, IssueNoteDetailService>();

            return services;
        }
    }
}
