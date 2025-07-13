using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TexStyle.Helpers;

namespace TexStyle {
    public class Program {
        public static void Main(string[] args) {
            // var host = new WebHostBuilder()
            //.UseKestrel()
            //.UseContentRoot(Directory.GetCurrentDirectory())
            //.UseIISIntegration()
            //.UseStartup<Startup>()
            //.Build();
            // host.Run();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
            //.UseHttpSys(options => {
            //    options.MaxRequestBodySize = 1000_000;
            //});
            //.UseKestrel(options => {
            //    //options.Limits.MaxRequestBodySize = null;
            //    options.Limits.MaxRequestHeadersTotalSize = 102400;
            //});
    }
}
