using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.Common;
using TexStyle.Extensions;
using TexStyle.Helpers;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.ViewModels;

namespace TexStyle.Areas.Admin.Controllers {
    [Area(AreaConstants.ADMIN.Name)]
    public class ManageAccountController : Controller {
        private readonly IAccountService _accountService;
        private readonly IAccountRoleService _accountRoleService;
        private readonly IPasswordHasher<Account> _passwordHasher;
        public ManageAccountController(IAccountService accountService,
            IAccountRoleService accountRoleService,
            IPasswordHasher<Account> passwordHasher) {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
            _accountRoleService = accountRoleService;
        }

        [HttpGet]
        public ActionResult Index() {
            return View(_accountService.GetAll());
        }

        [HttpGet]
        public async Task<ActionResult> AddOrUpdate(int? id) {
            Account account = null;
            //List<SelectListItem> claimsList = null;
            List<ClaimModuleViewModel> permissionsModuleList = ClaimPermissionHelper.GetClaimModuleViewModelList();
            List<SelectListItem> rolesList = (await _accountRoleService.GetAll()).ToSelectList();
            if (id.HasValue && id != 0) {
                account = await _accountService.GetById(id.Value);

                permissionsModuleList.ForEach(module =>
                    module.ClaimsSelectList.ForEach(permission =>
                            permission.Selected = account.Claims.Any(x => x.ClaimValue == permission.Text)
                    )
                );

                if (account.UserRoles.Count > 0)
                    rolesList.Find(x => Convert.ToInt32(x.Value) == account.UserRoles.FirstOrDefault().RoleId).Selected = true;
            }

            ViewBag.RoleList = rolesList;
            ViewBag.permissionsLST = permissionsModuleList;
            return View(account);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(int id, IFormCollection col) {
            try {
                //var hasher = new PasswordHasher<Account>();
                Account acc = new Account() {
                    Id = id!=0 ? id : 0,
                    UserName = col["UserName"],
                    Email = col["Email"],
                    EmailConfirmed = $"{col["EmailConfirmed"]}".Equals("on") ? true : false,
                };

                var pass = col["Password"];
                var cpass = col["ConfirmPassword"];
                var role = col["Role"];

                if (!string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(cpass) && pass.Equals(cpass)) {
                    acc.PasswordHash = _passwordHasher.HashPassword(acc, pass);
                }

                if (!string.IsNullOrEmpty(role)) {
                    acc.UserRoles.Add(new AccountUserRole { RoleId = Convert.ToInt32(role) });
                }

                if (id == null) {
                    acc.LockoutEnabled = false;
                    acc.SecurityStamp = Guid.NewGuid().ToString();
                    col.Keys.Where(x => x.Contains("check_")).ToList().ForEach(checkBox => {
                        var claim = checkBox.Replace("check_", "");
                        var type = claim.Contains(AccountClaimTypes.PERMISSION) ? AccountClaimTypes.PERMISSION : AccountClaimTypes.ROLE;
                        acc.Claims.Add(new AccountUserClaim {
                            ClaimType = AccountClaimTypes.PERMISSION,
                            ClaimValue = claim,
                        });
                    });
                    await _accountService.Create(acc);

                } else {
                    col.Keys.Where(x => x.Contains("check_")).ToList().ForEach(checkBox => {
                        var claim = checkBox.Replace("check_", "");
                        var type = claim.Contains(AccountClaimTypes.PERMISSION) ? AccountClaimTypes.PERMISSION : AccountClaimTypes.ROLE;
                        acc.Claims.Add(new AccountUserClaim {
                            UserId = id,
                            ClaimType = AccountClaimTypes.PERMISSION,
                            ClaimValue = claim
                        });
                    });
                    await _accountService.Update(acc);
                }

                return RedirectToAction(nameof(AddOrUpdate), nameof(ManageAccountController).Replace("Controller", ""), new { area = nameof(TexStyle.Areas.Admin), id = acc.Id });
            } catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id, IFormCollection col) {
            if (id != null) {
                await _accountService.Delete(await _accountService.GetById(id.Value));

                return new StatusCodeResult(200);
            }
            return new StatusCodeResult(400);
        }
    }
}