using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TexStyle.ApplicationServices;
using TexStyle.Identity.Extensions;
using TexStyle.Infrastructure;
using TexStyle.Common;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using TexStyle.Helpers;
using Microsoft.AspNetCore.Http.Features;
using Rotativa.AspNetCore;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            #region Cookie Authentication Configuration
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(opts => {
                opts.LoginPath = "/Home/Login";
                opts.AccessDeniedPath = "/Home/AccessDenied";
            });
            #endregion

            #region Boiler Plate
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion

            #region Auto Mapper
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new AutoMapperMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region Application Services Configuration
            ApplicationServiceRegistration.RegisterAppServices(services);
            #endregion

            #region Authorization Configuration
            // Claims Policy Registration
            services.AddAuthorization(options => {
                typeof(AccountClaimKeys).GetFields().ToList().ForEach(f => {
                    options.AddPolicy($"{f.GetValue(f)}", policy => policy.RequireClaim(AccountClaimTypes.PERMISSION, $"{f.GetValue(f)}"));
                });
            });
            #endregion

            #region Company Settings
            services.Configure<CompanySettings>(Configuration.GetSection("CompanySettings"));
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                ClaimPermissionHelper.GenerateOrUpdateClaimKeys();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseHttpSys(options => {
            //    options.MaxRequestBodySize = 100_000_000;
            //});

            //app.Run(async context => {
            //    context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 100_000_000;
            //});


            //app.use(options => {
            //     options.MaxRequestBodySize = 100_000_000;
            // });
                 app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default_Route",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: $"{AreaConstants.ADMIN.Name}_Route",
                    areaName: $"{AreaConstants.ADMIN.Name}",
                    template: $"{AreaConstants.ADMIN.Name}" + "/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: $"{AreaConstants.YARN_DYEING.Name}_Route",
                    areaName: $"{AreaConstants.YARN_DYEING.Name}",
                    template: $"{AreaConstants.YARN_DYEING.Abriviation}" + "/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: $"{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}_Route",
                    areaName: $"{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}",
                    template: $"{AreaConstants.PRODUCTION_PLANING_CONTROL.Abriviation}" + "/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: $"{AreaConstants.CHEMICAL_STORE.Name}_Route",
                    areaName: $"{AreaConstants.CHEMICAL_STORE.Name}",
                    template: $"{AreaConstants.CHEMICAL_STORE.Abriviation}" + "/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                name: $"{AreaConstants.GATE.Name}_Route",
                areaName: $"{AreaConstants.GATE.Name}",
                template: $"{AreaConstants.GATE.Abriviation}" + "/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                name: $"{AreaConstants.Analysis.Name}_Route",
                areaName: $"{AreaConstants.Analysis.Name}",
                template: $"{AreaConstants.Analysis.Abriviation}" + "/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                name: $"{AreaConstants.MarketingAccounts.Name}_Route",
                areaName: $"{AreaConstants.MarketingAccounts.Name}",
                template: $"{AreaConstants.MarketingAccounts.Abriviation}" + "/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: $"{AreaConstants.USER_MANAGEMENT.Name}_Route",
                    areaName: $"{AreaConstants.USER_MANAGEMENT.Name}",
                    template: $"{AreaConstants.USER_MANAGEMENT.Abriviation}" + "/{controller=Home}/{action=Index}/{id?}");
              
                RotativaConfiguration.Setup(env);


                //routes.MapAreaRoute(
                //  name: "Admin",
                //  areaName: $"Admin",
                //  template: "Admin/{controller=Home}/{action=Index}/{id?}"
                //);
            });
        }
    }
}
