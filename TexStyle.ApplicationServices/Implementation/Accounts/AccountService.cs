using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountService : IAccountService {
        private readonly IAccountRepository _repo;
        private readonly IAccountUserClaimRepository _userClaimRepo;
        private readonly IAccountUserRoleRepository _userRoleRepo;
        private readonly IAccountRoleRepository _roleRepo;
        public AccountService(IAccountRepository repo, IAccountUserClaimRepository userClaimRepo, IAccountUserRoleRepository userRoleRepo,
            IAccountRoleRepository roleRepo) {
            _repo = repo;
            _roleRepo = roleRepo;
            _userClaimRepo = userClaimRepo;
            _userRoleRepo = userRoleRepo;
        }
        public async Task<Account> Create(Account o) {
            try {
                await _repo.Add(o);

                if (o.Claims.Count > 0) {
                    o.Claims.ToList().ForEach(c => c.UserId = o.Id);
                    await _userClaimRepo.Add(o.Claims.ToArray());
                }

                if (o.UserRoles.Count > 0) {
                    o.UserRoles.ToList().ForEach(x => { x.UserId = o.Id; x.UserId = o.Id; x.RoleId = x.RoleId; });
                    await _userRoleRepo.Add(o.UserRoles.ToArray());
                }
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Account> Delete(Account o) {
            throw new NotImplementedException();
            //try {
            //    _repo.Update(o);
            //    return o;
            //} catch (Exception ex) {
            //    throw ex;
            //}
        }

        public async Task<List<Account>> GetAll() {
            try {
                var list = await _repo.GetAll();
                return list.ToList();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Account>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<Account> GetById(long id) {
            try {
                var acc = await _repo.GetSingle(x => x.Id == id);

                var roles = await _userRoleRepo.GetAll(x => x.User, x => x.Role);
                var accRoles =  roles.Where(x => x.UserId == acc.Id).ToList();

                var userTolesData = await _userRoleRepo.GetAll(x => x.User, x => x.Role);
                acc.UserRoles = userTolesData.Where(x => x.UserId == id).ToList();
                var claimsData = await _userClaimRepo.GetList(x => x.UserId == id);
                acc.Claims = claimsData.ToList();

                return acc;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Account> GetByUserName(string username)
        {
            try
            {
                var acc = await _repo.GetSingle(x => x.UserName.Equals(username));

                if (acc != null)
                {
                    var userRolesData = await _userRoleRepo.GetList(x => x.UserId == acc.Id);
                    var userRoles = userRolesData.ToList();

                    await Task.WhenAll(userRoles.Select(async x =>
                    {
                        x.User = acc;
                        x.Role = await _roleRepo.GetSingle(ro => ro.Id == x.RoleId);
                    }));

                    acc.UserRoles = userRoles;
                    acc.Claims = (await _userClaimRepo.GetList(x => x.UserId == acc.Id)).ToList();
                }
                return acc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Account> Update(Account o) {
            try {
                var f = await GetById(o.Id);
                f.Email = o.Email;
                f.EmailConfirmed = o.EmailConfirmed;
                f.UserName = o.UserName;
                f.IsYarn = o.IsYarn;

                if (o.PasswordHash != null) {
                    f.PasswordHash = o.PasswordHash;
                }

                if (f.UserRoles.Count > 0) {

                    await _userRoleRepo.Remove(f.UserRoles.ToArray());

                    if (o.UserRoles.Count > 0) {
                      await  _userRoleRepo.Add(o.UserRoles.ToArray());
                    }
                } else {
                   await _userRoleRepo.Add(o.UserRoles.ToArray());
                }


                if (f.Claims.Count > 0) {
                   await _userClaimRepo.Remove(f.Claims.ToArray());

                    if (o.Claims.Count > 0) {
                       await _userClaimRepo.Add(o.Claims.ToArray());
                    }
                } else {
                    await _userClaimRepo.Add(o.Claims.ToArray());
                }

                await _repo.Update(f);
                return f;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
