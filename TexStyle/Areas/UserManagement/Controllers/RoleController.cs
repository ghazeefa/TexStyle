using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Extensions;
using TexStyle.Helpers;
//using TexStyle.Helpers;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Accounts;

namespace TexStyle.Areas.UserManagement.Controllers {
    [Area(AreaConstants.USER_MANAGEMENT.Name)]
    public class RoleController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private string _ViewPath = "/Areas/Admin/Views/ManageRole/";
        public RoleController(IUnitOfWork uow, IMapper mapper) {
            _uow = uow;
            _mapper = mapper;
        }
        [Authorize(Policy = AccountClaimKeys.ROLE_VIEW)]
        public async Task<ActionResult> Index() {
            var roles = new List<AccountRole>();
            if (User.IsInRole(AccountRoleKeys.DEVELOPER)) {
                roles = await _uow.AccountRoleService.GetAll();
            }else {
                roles = await _uow.AccountRoleService.GetUserRoles();
            }
            return View(roles);
        }


        [HttpGet]

        public async Task<ActionResult> AddOrUpdate(int? id) {
            AccountRole role = null;
            List<ClaimModuleViewModel> permissionsModuleList = ClaimPermissionHelper.GetClaimModuleViewModelList();
            List<SelectListItem> rolesList = (await _uow.AccountRoleService.GetAll()).ToSelectList();
            if (id.HasValue && id != 0) {
                role = await _uow.AccountRoleService.GetById(id.Value);

                permissionsModuleList.ForEach(module =>
                    module.ClaimsSelectList.ForEach(permission => {
                        if (role.RoleClaims != null) {

                            permission.Selected = role.RoleClaims.Any(x => x.ClaimValue == permission.Text);
                        }
                    })
                );
            }

            ViewBag.RoleList = rolesList;
            ViewBag.permissionsLST = permissionsModuleList;
            //$"{_ViewPath}{nameof(AddOrUpdate)}.cshtml",
            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(int id, IFormCollection col) {
            try {
                AccountRole role = new AccountRole {
                    Id = id,
                    Name = col["Name"],
                    RoleClaims = new List<AccountRoleClaim>(),
                };

                if (id == 0) {
                    col.Keys.Where(x => x.Contains("check_")).ToList().ForEach(checkBox => {
                        var claim = checkBox.Replace("check_", "");
                        var type = claim.Contains(AccountClaimTypes.PERMISSION) ? AccountClaimTypes.PERMISSION : AccountClaimTypes.ROLE;
                        role.RoleClaims.Add(new AccountRoleClaim {
                            ClaimType = AccountClaimTypes.PERMISSION,
                            ClaimValue = claim,
                        });
                    });
                    await _uow.AccountRoleService.Create(role);
                } else {
                    col.Keys.Where(x => x.Contains("check_")).ToList().ForEach(checkBox => {
                        var claim = checkBox.Replace("check_", "");
                        var type = claim.Contains(AccountClaimTypes.PERMISSION) ? AccountClaimTypes.PERMISSION : AccountClaimTypes.ROLE;
                        role.RoleClaims.Add(new AccountRoleClaim {
                            RoleId = id,
                            ClaimType = AccountClaimTypes.PERMISSION,
                            ClaimValue = claim,
                        });
                    });
                    await _uow.AccountRoleService.Update(role);
                }
                return RedirectToAction(nameof(AddOrUpdate), nameof(RoleController).Replace("Controller", ""), new { area = nameof(TexStyle.Areas.UserManagement), id = role.Id });

            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long? id, IFormCollection col) {
            if (id != null) {
                await _uow.AccountRoleService.Delete(await _uow.AccountRoleService.GetById(id.Value));

                return new StatusCodeResult(200);
            }
            return new StatusCodeResult(400);
        }

    }
}