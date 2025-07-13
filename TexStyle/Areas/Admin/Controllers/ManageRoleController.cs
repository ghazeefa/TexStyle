using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.Common;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Areas.Admin.Controllers {
    [Area(AreaConstants.ADMIN.Name)]
    public class ManageRoleController : Controller {

        private readonly IAccountRoleService _accountRoleSrvice;
        private readonly IAccountService _accountService;
        private string _ViewPath = "/Areas/Admin/Views/ManageRole/";
        public ManageRoleController(IAccountRoleService accountRoleSrvice, IAccountService accountService) {
            _accountRoleSrvice = accountRoleSrvice;
            _accountService = accountService;
        }
        public async Task<ActionResult> Index() {
            var roles = await _accountRoleSrvice.GetAll();
            return View(roles);
        }


        [HttpGet]

        public async Task<ActionResult> AddOrUpdate(long? id) {
            AccountRole m = null;
            if (id != null) {
                m = await _accountRoleSrvice.GetById(id.Value);
            }
            return PartialView($"{_ViewPath}{nameof(AddOrUpdate)}.cshtml", m);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(int id, IFormCollection col) {
            try {
                AccountRole m = new AccountRole {
                    Id = id,
                    Name = col["Name"]
                };

                if (id == 0) {
                    await _accountRoleSrvice.Create(m);
                } else {
                    await _accountRoleSrvice.Update(m);
                }
                return RedirectToAction("Index");
            } catch (Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long? id, IFormCollection col) {
            if (id != null) {
                await _accountRoleSrvice.Delete(await _accountRoleSrvice.GetById(id.Value));

                return new StatusCodeResult(200);
            }
            return new StatusCodeResult(400);
        }
    }
}