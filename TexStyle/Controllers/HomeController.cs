using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.Common;
using TexStyle.Helpers;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.Identity.Extensions.Managers;
using TexStyle.Identity.Extensions.Services;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Accounts;

namespace TexStyle.Controllers {
    public class HomeController : Controller {
        [ViewData]
        public string AreaName { get; set; }

        private readonly AccountManager _accountManager;
        private readonly SignInManager<Account> _accountSigninManager;
        private readonly RoleManager<AccountRole> _accountRoleManager;
        private readonly EmailService _emailService;
        private readonly IAccountService _accountService;
        private readonly IHostingEnvironment _env;
        public HomeController(
            SignInManager<Account> accountSigninManager,
            RoleManager<AccountRole> accountRoleManager, AccountManager accountManager,
            IAccountService accountService,
            IHostingEnvironment env) {
            AreaName = "Main Dash";
            _accountService = accountService;
            _accountManager = accountManager;
            _accountRoleManager = accountRoleManager;
            _accountSigninManager = accountSigninManager;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index() {
            return RedirectToAction(nameof(Login));
            //return View();
        }

        [HttpGet]
        public IActionResult Login() {    
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            await _accountSigninManager.SignOutAsync();
            if (!ModelState.IsValid) {
                return View(model);
            }
            var user = await _accountService.GetByUserName(model.UserName);
            var result = await _accountSigninManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            
             if (result.Succeeded) {
                //var area = user.Roles.ToList().Any(x => x.AccountRole.Name == AccountRoleKeys.PPC_USER) ? AreaConstants.PRODUCTION_PLANING_CONTROL.Abriviation :
                //    user.Roles.ToList().Any(x => x.AccountRole.Name == AccountRoleKeys.CHEMICAL_STORE_USER) ? AreaConstants.CHEMICAL_STORE.Abriviation :
                //    user.Roles.ToList().Any(x => x.AccountRole.Name == AccountRoleKeys.YARN_DYING_USER) ? AreaConstants.YARN_DYEING.Abriviation :
                //    user.Roles.ToList().Any(x => x.AccountRole.Name == AccountRoleKeys.ADMIN) ? AreaConstants.ADMIN.Abriviation : AreaConstants.ADMIN.Abriviation;
                var role = user.UserRoles.ToList().FirstOrDefault();

                if (role != null) {
                    switch (role.Role.Name) {
                        case AccountRoleKeys.PPC_USER:
                            return RedirectToAction("Index", "Home", new { area = AreaConstants.PRODUCTION_PLANING_CONTROL.Name });
                        case AccountRoleKeys.YARN_DYING_USER:
                            return RedirectToAction("Index", "Home", new { area = AreaConstants.YARN_DYEING.Name });
                        case AccountRoleKeys.CHEMICAL_STORE_USER:
                            return RedirectToAction("Index", "Home", new { area = AreaConstants.CHEMICAL_STORE.Name });
                        case AccountRoleKeys.GATE_USER:
                            return RedirectToAction("Index", "Home", new { area = AreaConstants.GATE.Name });
                        case AccountRoleKeys.ANALYSIS:
                            return RedirectToAction("Index", "Home", new { area = AreaConstants.Analysis.Name });

                        default:
                            return RedirectToAction("Index", "Home", new { area = AreaConstants.PRODUCTION_PLANING_CONTROL.Name });
                    }
                }

                return RedirectToAction("Index","Home",
                    new { area = AreaConstants.PRODUCTION_PLANING_CONTROL.Name });
            }

            //switch (result) {
            //    case SignInResult.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //        //case SignInStatus.Failure:
            //        // 
            //}
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await _accountSigninManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult AccessDenied() {
            return View();
        }
        [HttpGet]
        public IActionResult Privacy() {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Sad() {
            // Use ProcessStartInfo class.
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}sad.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo)) {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex) {
                // Log error.
            }
            return Content("Fucking Complete");
        }
    }
}
