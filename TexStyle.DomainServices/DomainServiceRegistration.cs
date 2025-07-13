using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TexStyle.DomainServices.Implementation;
using TexStyle.DomainServices.Interfaces;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.Identity.Extensions.Managers;
using TexStyle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.DomainServices.Implementation.PPC;
using TexStyle.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.DomainServices.Implementation.Accounts;
using System.Reflection;
using System.Linq;

namespace TexStyle.DomainServices {
    public static class DomainServiceRegistration {
        public static IServiceProvider GlobalServiceProvider { get; private set; }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services) {
            GlobalServiceProvider = services.BuildServiceProvider();
            /// uncomment to use on console applications
            //
            //  var builder = new DbContextOptionsBuilder<AppDbContext>();
            //  build configurations
            //  var configBuilder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false);
            //   build root
            //   var root = configBuilder.Build();
            //   var _connectionString = root.GetConnectionString(AppDbContext.APP_CON_NAME);
            //   services.AddEntityFrameworkSqlServer()
            //    .AddDbContext<AppDbContext>(options => options
            //    .UseSqlServer(_connectionString), ServiceLifetime.Transient);
            //   this is to run service from console type project
            //  services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);

            // register database
            services.AddDbContext<AppDbContext>();

            // for web
            services.AddScoped<AppDbContext>();

            // Identity Configuration
            services.AddIdentity<Account, AccountRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
            services.AddScoped<AccountManager>();
            services.AddScoped<AccountSigninManager>();
            // override UserClaimsPrincipalFactory (to remove role claims from cookie )
            //services.AddScoped<IUserClaimsPrincipalFactory<Account>, AppClaimsPrincipalFactory>();

            var allRepoTypes = Assembly.GetExecutingAssembly()
            .GetTypes().Where(t => t.Namespace != null);

            foreach (var intfc in allRepoTypes.Where(t => t.IsInterface && t.Namespace.Contains("DomainServices"))) {
                var impl = allRepoTypes.FirstOrDefault(c => c.IsClass && intfc.Name.Substring(1) == c.Name);
                if (impl != null) services.AddScoped(intfc, impl);
            }


            //// Identity Repository Configuration
            //services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            //services.AddScoped<IAccountLoginRepository, AccountLoginRepository>();
            //services.AddScoped<IAccountClaimRepository, AccountClaimRepository>();
            //services.AddScoped<IAccountUserRoleRepository, AccountUserRoleRepository>();
            //services.AddScoped<IAccountRoleClaimRepository, AccountRoleClaimRepository>();
            //services.AddScoped<IAccountUserClaimRepository, AccountUserClaimRepository>();
            //services.AddScoped<IAccountUserLoginRepository, AccountUserLoginRepository>();
            //services.AddScoped<IAccountUserTokenRepository, AccountUserTokenRepository>();

            //// App Repositories
            //services.AddScoped<IYarnTypeRepository, YarnTypeRepository>();
            //services.AddScoped<IYarnQualityRepository, YarnQualityRepository>();
            //services.AddScoped<IBagMarkingRepository, BagMarkingRepository>();
            //services.AddScoped<IConeMarkingRepository, ConemarkingRepository>();
            //services.AddScoped<IStoreLocationRepository, StoreLocationRepository>();
            //services.AddScoped<IPartyRepository, PartyRepository>();

            //services.AddScoped<IBuyerRepository,BuyerRepository>();
            //services.AddScoped<IBuyerColorRepository, BuyerColorRepository>();

            //services.AddScoped<IShadeRepository, ShadeRepository>();
            //services.AddScoped<IOrderActivityTypeRepository, OrderActivityTypeRepository>();
            //services.AddScoped<IYarnManufacturerRepository, YarnManufacturerRepository>();
            //services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

            //services.AddScoped<IOGPRepository, OGPRepository>();

            //services.AddScoped<IIGPRepository, IGPRepository>();
            //services.AddScoped<IIGPDetailRepository, IGPDetailRepository>();

            //services.AddScoped<IReturnNoteRepository, ReturnNoteRepository>();
            //services.AddScoped<IReturnNoteDetailRepository, ReturnNoteDetailRepository>();

            //services.AddScoped<ISeasonRepository, SeasonRepository>();
            //services.AddScoped<IPPCPlanningRepository, PPCPlanningRepository>();


            //services.AddScoped<IIssueNoteRepository, IssueNoteRepository>();
            //services.AddScoped<IIssueNoteDetailRepository, IssueNoteDetailRepository>();

            //services.RegisterRepositories();

            return services;
        }
    }
}
