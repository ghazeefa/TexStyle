using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Extensions;
using TexStyle.Helpers;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Accounts;

namespace TexStyle.Areas.UserManagement.Controllers {
    [Area(AreaConstants.USER_MANAGEMENT.Name)]
    public class AccountController : Controller {
        private readonly TempDataViewModel _tempData;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Account> _passwordHasher;
        public AccountController(IUnitOfWork uow, IPasswordHasher<Account> passwordHasher, IMapper mapper) {
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }


        [HttpGet]
        public async Task<ActionResult> Index() {
            return View(await _uow.AccountService.GetAll());
        }

        [HttpGet]
        public async Task<ActionResult> AddOrUpdate(long? id) {
            //List<SelectListItem> claimsList = null;
            AccountViewModel accountVm = null;
            //List<ClaimModuleViewModel> permissionsModuleList = ClaimPermissionHelper.GetClaimModulesListVM();
            List<SelectListItem> rolesList = (await _uow.AccountRoleService.GetUserRoles()).ToSelectList();
            var roles = new List<AccountRole>();
            if (User.IsInRole(AccountRoleKeys.DEVELOPER)) {
                rolesList = (await _uow.AccountRoleService.GetAll()).ToSelectList();
            } 
            else 
            {
                rolesList =  (await _uow.AccountRoleService.GetUserRoles()).ToSelectList();
            }
            if (id.HasValue && id != 0) {
                var account = await _uow.AccountService.GetById(id.Value);
                accountVm =  _mapper.Map<AccountViewModel>(account);

                //permissionsModuleList.ForEach(module =>
                //    module.ClaimsSelectList.ForEach(permission =>
                //            permission.Selected = account.Claims.Any(x => x.ClaimValue == permission.Text)
                //    )
                //);

                if (account.UserRoles.Count > 0)
                    rolesList.Find(x => Convert.ToInt32(x.Value) == account.UserRoles.FirstOrDefault().RoleId).Selected = true;
            }

            ViewBag.RoleList = rolesList;
            //ViewBag.permissionsLST = permissionsModuleList;
            return View(accountVm);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(int? id, IFormCollection col) {
            try {
                Account acc = new Account() {
                    Id = id.HasValue ? id.Value : 0,
                    UserName = col["UserName"],
                    Email = col["Email"],
                    EmailConfirmed = $"{col["EmailConfirmed"]}".Equals("on") ? true : false,
                    IsYarn = $"{col["IsYarn"]}".Equals("on") ? true : false,
                    UserRoles = new List<AccountUserRole>(),
                    Claims = new List<AccountUserClaim>()
                };

                var pass = col["Password"];
                var cpass = col["ConfirmPassword"];
                var role = col["RoleId"];

                if (!string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(cpass) && pass.Equals(cpass)) {
                    acc.PasswordHash = _passwordHasher.HashPassword(acc, pass);
                }

                if (!string.IsNullOrEmpty(role)) {
                    acc.UserRoles.Add(new AccountUserRole { RoleId = Convert.ToInt32(role), UserId = acc.Id == 0 ? 0 : acc.Id });
                    acc.UserRoles.ToList().ForEach(async x => {
                        var fRole = await _uow.AccountRoleService.GetById(Convert.ToInt32(role));
                        if (fRole.RoleClaims.Count > 0) {
                            fRole.RoleClaims.ToList().ForEach(claim => {
                                acc.Claims.Add(new AccountUserClaim {
                                    UserId = id.HasValue ? id.Value : 0,
                                    ClaimType = claim.ClaimType,
                                    ClaimValue = claim.ClaimValue
                                });
                            });
                        }
                    });
                }

                if (id == null) {
                    acc.LockoutEnabled = false;
                    acc.SecurityStamp = Guid.NewGuid().ToString();
                    //col.Keys.Where(x => x.Contains("check_")).ToList().ForEach(checkBox => {
                    //    var claim = checkBox.Replace("check_", "");
                    //    var type = claim.Contains(AccountClaimTypes.PERMISSION) ? AccountClaimTypes.PERMISSION : AccountClaimTypes.ROLE;
                    //    acc.Claims.Add(new AccountUserClaim {
                    //        ClaimType = AccountClaimTypes.PERMISSION,
                    //        ClaimValue = claim,
                    //    });
                    //});
                    await _uow.AccountService.Create(acc);

                } else {
                    //col.Keys.Where(x => x.Contains("check_")).ToList().ForEach(checkBox => {
                    //    var claim = checkBox.Replace("check_", "");
                    //    var type = claim.Contains(AccountClaimTypes.PERMISSION) ? AccountClaimTypes.PERMISSION : AccountClaimTypes.ROLE;
                    //    acc.Claims.Add(new AccountUserClaim {
                    //        UserId = id.HasValue? id.Value : 0,
                    //        ClaimType = AccountClaimTypes.PERMISSION,
                    //        ClaimValue = claim
                    //    });
                    //});
                    await _uow.AccountService.Update(acc);
                }

                return RedirectToAction(nameof(AddOrUpdate), nameof(AccountController).Replace("Controller", ""), new { area = nameof(TexStyle.Areas.UserManagement), id = acc.Id });
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long? id, IFormCollection col) {
            if (id != null) {
                await _uow.AccountService.Delete(await _uow.AccountService.GetById(id.Value));
                return new StatusCodeResult(200);
            }
            return new StatusCodeResult(400);
        }
    }
}