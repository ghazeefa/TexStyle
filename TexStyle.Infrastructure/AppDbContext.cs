using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Core.Analysis;
using TexStyle.Core.Premisis;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Core.ReportsViewModel.PPC;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.Core.ReportsViewModel;
//using TexStyle.Core.Identity.DTO;

namespace TexStyle.Infrastructure {
  public  sealed class DBStringConsts {
        public const string DOC_Local = "AppDb_Conn_Doc";
        public const string LEE_Local = "AppDb_Conn_Lee";
        public const string GEAR_HOST_LIVE = "AppDb_Conn_cloud";
        public const string TEXSTYLE_LIVE = "Textyle_Live_Db";
        public const string TEXSTYLE_DEV = "Textyle_Dev_Db";
        public const string TEXSTYLE_DEV_SP = "Textyle_Live_SP";
        public const string SQL_SERVER_CONNECTION = "AppDb_Conn_Lee_SS";
        public const string LocalDB = "Local_Db";       
        public const string Payroll_Live_Db =     "Payroll_Live_Db";
    }

    public class AppDbContext : IdentityDbContext<Account, AccountRole, int, AccountUserClaim, AccountUserRole, AccountUserLogin, AccountRoleClaim, AccountUserToken> {
       public static string APP_CON_NAME = DBStringConsts.DOC_Local;    
        //public static string APP_CON_NAME = DBStringConsts.Payroll_Live_Db;

        private readonly string _connectionString;
        internal static string ConString { get; private set; } // for migrations

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            //build configurations 
            var path = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);
            // build root
            var root = configBuilder.Build();
            _connectionString = root.GetConnectionString(APP_CON_NAME);
            ConString = _connectionString;

            //(this as IObjectContextAdapter)
            //Database.SetCommandTimeout(TimeSpan.FromMinutes(2));
            // default db configuration options
            //Database.Migrate();
            //Database.EnsureCreated();
        }

        public AppDbContext() {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            #region Reconfigure tables
            modelBuilder.Entity<Account>(b => {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<AccountRole>(b => {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
            #endregion

            #region Rename Tables
            modelBuilder.Entity<Account>(b => {
                // Primary key
                b.HasKey(u => u.Id);
                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                //// Maps to the AspNetUsers table
                //b.ToTable("AspNetUsers");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);


                // Each User can have many UserClaims
                b.HasMany<AccountUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<AccountUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<AccountUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<AccountUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

                // One user can have one branch
                //b.HasOne<Branch>().WithMany().HasForeignKey(br => br.BranchId);

                b.ToTable("Accounts");
            });

            modelBuilder.Entity<AccountUserClaim>(b => {
                // Primary key
                b.HasKey(uc => uc.Id);
                b.ToTable("AccountUserClaims");
            });

            modelBuilder.Entity<AccountUserLogin>(b => {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);
                b.ToTable("AccountUserLogins");
            });

            modelBuilder.Entity<AccountUserToken>(b => {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(t => t.LoginProvider).HasMaxLength(500);
                b.Property(t => t.Name).HasMaxLength(500);

                b.ToTable("AccountUserTokens");
            });

            modelBuilder.Entity<AccountRole>(b => {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                // Each Role can have many entries in the UserRole join table
                b.HasMany<AccountUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<AccountRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

                b.ToTable("AccountRoles");
            });

            modelBuilder.Entity<AccountRoleClaim>(b => {
                // Primary key
                b.HasKey(rc => rc.Id);

                b.ToTable("AccountRoleClaims");
            });

            modelBuilder.Entity<AccountUserRole>(b => {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });
                //b.HasKey(r => r.Id);

                b.ToTable("AccountUserRoles");
            });
            #endregion

            #region Seed PPC
            // modelBuilder.Entity<RecipeFormatHeader>().HasData(new RecipeFormatHeader { Id = 1, Name = "Red", LiquorRate = 1, ProcessTypeId = 1 });
            modelBuilder.Entity<BagMarking>().HasData(new BagMarking { Id = 1, Name = "Red", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<ConeMarking>().HasData(new ConeMarking { Id = 1, Name = "Khaki", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<YarnManufacturer>().HasData(new YarnManufacturer { Id = 1, Name = "Default Manufacturer", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<YarnType>().HasData(new YarnType { Id = 1, Name = "12/s", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<YarnQuality>().HasData(new YarnQuality { Id = 1, Name = "Grey", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<ProductionType>().HasData(new ProductionType { Id = 1, Name = "Sample", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<ProductionType>().HasData(new ProductionType { Id = 2, Name = "Production", CreatedOn = DateTime.Now, IsDeleted = false });
            
            modelBuilder.Entity<BuyerColor>().HasData(new BuyerColor {
                Id = 1,
                Name = "Darker Than Black",
                BuyerId = 1,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            });
            modelBuilder.Entity<Buyer>().HasData(new Buyer {
                Id = 1,
                PartyId = 10009,
                Name = "Some Random Textile",
                CreatedOn = DateTime.Now,
                IsDeleted = false
            });
            modelBuilder.Entity<Party>().HasData(new Party {
                Id = 10009,
                Name = "It's Party In the House.",
                CreatedOn = DateTime.Now,
                ControlAccount = 10000,
                IsDeleted = false
            });
            modelBuilder.Entity<Season>().HasData(new Season { Id = 1, Name = "Winter", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<PurchaseOrder>().HasData(new PurchaseOrder { Id = 1, BuyerColorId = 1, SeasonId = 1, YarnQualityId = 1, YarnTypeId = 1, IsClosed = false, CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<ActivityType>().HasData(new ActivityType { Id = 1, Name = "Default order type", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<SubActivityType>().HasData(new SubActivityType { Id = 1, ActivityTypeId = 1, Name = "Default sub process type", CreatedOn = DateTime.Now, IsDeleted = false });
            //modelBuilder.Entity<InwardGatePassDetail>().HasKey(c => new { c.Id, c.Sno });
            modelBuilder.Entity<StoreLocation>().HasData(new StoreLocation { Id = 1, Name = "R1", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<InwardGatePass>().HasData(new InwardGatePass { Id = 1, IgpDate = DateTime.Now, ActivityTypeId = 1, IsReWaxRecheck = true, PartyId = 10009, IsReturnfromParty = false, CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<InwardGatePassDetail>().HasData(new InwardGatePassDetail {
                Id = 1,
              //  BilityNo = "9j",
                Bags = 9,
                Description = "Demo Description",
               // Difference = 0,
                NetWeightInKg = 90,
                TearWeightInKg = 90,
                IsZeroBalance = false,
                StoreLocationId = 1,
                YarnManufacturerId = 1,
                BagMarkingId = 1,
                ConeMarkingId = 1,
                YarnTypeId = 1,
                YarnQualityId = 1,
                InwardGatePassId = 1
            });
            #endregion

            #region Seed YD
            modelBuilder.Entity<ProcessType>().HasData(new ProcessType { Id = 1, Name = "D PT", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<MachineType>().HasData(new MachineType { Id = 1, Name = "D MT", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Machine>().HasData(new Machine { Id = 1, MachineTypeId = 1, Chambers = 1, ReelSpeed = 1, CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<RecipeStep>().HasData(new RecipeStep { Id = 1, Name = "D STEP", CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Dye>().HasData(new Dye { Id = 1, Name = "D D", Rate = 1, CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Chemical>().HasData(new Chemical { Id = 1, Name = "D CM", Rate = 1, CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Costing>().HasData(new Costing { Id = 1, Date = DateTime.Now, Gas = 1, MIS = 1, Electricity = 1, ExportQuantity = 1, FurnaceCharges = 1, SalaryAndWage = 1, CreatedOn = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<RecipeFormatHeader>().HasData(new RecipeFormatHeader { Id = 1, Name = "D F", LiquorRate = 1, ProcessTypeId = 1, CreatedOn = DateTime.Now });
            modelBuilder.Entity<Recipe>().HasData(new Recipe { Id = 1, LiquorRate = 1, MachineTypeId = 1, No = 0, RecipeFormatId = 1, Date = DateTime.Now, CreatedOn = DateTime.Now, IsDeleted = false });
            #endregion

            #region Seed Accounts
            //var devRoleGui = Guid.NewGuid().ToString();
            //var adminRoleGui = Guid.NewGuid().ToString();
            //var managerRoleGui = Guid.NewGuid().ToString();
            //var ppcRoleGui = Guid.NewGuid().ToString();
            //var ydRoleGui = Guid.NewGuid().ToString();
            //var csRoleGui = Guid.NewGuid().ToString();
            //var gateRoleGui = Guid.NewGuid().ToString();

            //modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = devRoleGui, Name = "Developer" });
            //modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = adminRoleGui, Name = "Administrator" });
            //modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = managerRoleGui, Name = "Manager" });
            //modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = ppcRoleGui, Name = "PPC User" });
            //modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = ydRoleGui, Name = "YD User" });
            //modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = csRoleGui, Name = "CS User" });
            //modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = gateRoleGui, Name = "Gate User" });

            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 1, Name = "Developer" });
            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 2, Name = "Administrator" });
            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 3, Name = "Manager" });
            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 4, Name = "PPC User" });
            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 5, Name = "YD User" });
            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 6, Name = "CS User" });
            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 7, Name = "Gate User" });
            modelBuilder.Entity<AccountRole>().HasData(new AccountRole { Id = 8, Name = "Marketing Accounts User" });

            // account 
            //var leeGui = Guid.NewGuid().ToString();
            //var fatimaGui = Guid.NewGuid().ToString();
            //var adminGui = Guid.NewGuid().ToString();
            //var ppcGui = Guid.NewGuid().ToString();
            //var ydGui = Guid.NewGuid().ToString();
            //var csGui = Guid.NewGuid().ToString();
            //var gateGui = Guid.NewGuid().ToString();

            //modelBuilder.Entity<Account>().HasData(new Account { Id = leeGui, Email = "leehaisen01@gmail.com", UserName = "leehaisen", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            //modelBuilder.Entity<Account>().HasData(new Account { Id = fatimaGui, Email = "fatima@gmail.com", UserName = "fatima", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            //modelBuilder.Entity<Account>().HasData(new Account { Id = adminGui, Email = "admintexstyle@gmail.com", UserName = "admin", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            //modelBuilder.Entity<Account>().HasData(new Account { Id = ppcGui, Email = "ppcuser@gmail.com", UserName = "ppcuser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            //modelBuilder.Entity<Account>().HasData(new Account { Id = ydGui, Email = "yduser@gmail.com", UserName = "yduser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            //modelBuilder.Entity<Account>().HasData(new Account { Id = csGui, Email = "csuser@gmail.com", UserName = "csuser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            //modelBuilder.Entity<Account>().HasData(new Account { Id = gateGui, Email = "gateuser@gmail.com", UserName = "gateuser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });

            // int 
            modelBuilder.Entity<Account>().HasData(new Account { Id = 1, Email = "developer@gmail.com", UserName = "fatima", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            modelBuilder.Entity<Account>().HasData(new Account { Id = 2, Email = "developer@gmail.com", UserName = "developer", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            modelBuilder.Entity<Account>().HasData(new Account { Id = 3, Email = "admintexstyle@gmail.com", UserName = "admin", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            modelBuilder.Entity<Account>().HasData(new Account { Id = 4, Email = "ppcuser@gmail.com", UserName = "ppcuser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            modelBuilder.Entity<Account>().HasData(new Account { Id = 5, Email = "yduser@gmail.com", UserName = "yduser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            modelBuilder.Entity<Account>().HasData(new Account { Id = 6, Email = "csuser@gmail.com", UserName = "csuser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            modelBuilder.Entity<Account>().HasData(new Account { Id = 7, Email = "gateuser@gmail.com", UserName = "gateuser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });
            modelBuilder.Entity<Account>().HasData(new Account { Id = 8, Email = "marketingaccountsuser@gmail.com", UserName = "marketingaccountuser", PasswordHash = "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", SecurityStamp = "3caaa674-fd7a-41c6-82fe-2e190f51a43f" });

            // account Roles
            //modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { Id = Guid.NewGuid().ToString(), UserId = leeGui, RoleId = devRoleGui });
            //modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { Id = Guid.NewGuid().ToString(), UserId = fatimaGui, RoleId = devRoleGui });
            //modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { Id = Guid.NewGuid().ToString(), UserId = adminGui, RoleId = adminRoleGui });
            //modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { Id = Guid.NewGuid().ToString(), UserId = ppcGui, RoleId = ppcRoleGui });
            //modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { Id = Guid.NewGuid().ToString(), UserId = csGui, RoleId = csRoleGui });
            //modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { Id = Guid.NewGuid().ToString(), UserId = ydGui, RoleId = ydRoleGui });
            //modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { Id = Guid.NewGuid().ToString(), UserId = gateGui, RoleId = gateRoleGui });

            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 1, RoleId = 1 });
            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 2, RoleId = 2 });
            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 3, RoleId = 3 });
            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 4, RoleId = 4 });
            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 5, RoleId = 5 });
            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 6, RoleId = 6 });
            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 7, RoleId = 7 });
            modelBuilder.Entity<AccountUserRole>().HasData(new AccountUserRole { UserId = 8, RoleId = 8 });

            #endregion

            #region Gate
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 1, Name = "IGP Yarn", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = false, IsLoanOutActivity = false, IsPurchaseActivity = false });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 2, Name = "OGP Yarn", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = false, IsLoanOutActivity = false, IsPurchaseActivity = false });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 3, Name = "LC IMPORT IN", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = false, IsLoanOutActivity = false, IsPurchaseActivity = true });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 4, Name = "LOAN PARTY RETURN IN", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = true, IsLoanOutActivity = false, IsPurchaseActivity = false });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 5, Name = "LOAN TAKEN IN", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = true, IsLoanOutActivity = false, IsPurchaseActivity = false });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 6, Name = "LOCAL PURCHASE IN", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = false, IsLoanOutActivity = false, IsPurchaseActivity = true });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 7, Name = "INTER UNIT OUT", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = false, IsLoanOutActivity = false, IsPurchaseActivity = false });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 8, Name = "LOAN PARTY GIVEN OUT", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = false, IsLoanOutActivity = true, IsPurchaseActivity = false });
            modelBuilder.Entity<GateActivityType>().HasData(new GateActivityType { Id = 9, Name = "LOAN TAKEN RETURN OUT", CreatedOn = DateTime.Now, IsDeleted = false, IsLoanINActivity = false, IsLoanOutActivity = true, IsPurchaseActivity = false });
            modelBuilder.Entity<GatePassType>().HasData(new GatePassType { Id = 1, Name = "Regular", CreatedOn = DateTime.Now, IsDeleted = false});
            modelBuilder.Entity<GatePassType>().HasData(new GatePassType { Id = 2, Name = "ReDyeing", CreatedOn = DateTime.Now, IsDeleted = false});
            modelBuilder.Entity<GatePassType>().HasData(new GatePassType { Id = 3, Name = "After Raising", CreatedOn = DateTime.Now, IsDeleted = false});
            modelBuilder.Entity<GatePassType>().HasData(new GatePassType { Id = 4, Name = "A", CreatedOn = DateTime.Now, IsDeleted = false});
            modelBuilder.Entity<GatePassType>().HasData(new GatePassType { Id = 5, Name = "B", CreatedOn = DateTime.Now, IsDeleted = false});

            #endregion

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //build configurations
            var path = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false);
            // build root
            var root = configBuilder.Build();
            ConString = root.GetConnectionString(APP_CON_NAME);
            optionsBuilder.UseSqlServer(ConString, sqlOptions =>
            {
                sqlOptions.CommandTimeout(200);
            });
            base.OnConfiguring(optionsBuilder);
        }

        // Db Contexts
        #region PPC Planing

        public DbSet<YarnType> YarnTypes { get; set; }
        public DbSet<knittingParty> knittingPartyes { get; set; }
        public DbSet<RollMarking> RollMarkings { get; set; }
        public DbSet<RollMarkingDetail> RollMarkingDetails { get; set; }
        public DbSet<FabricQuality> FabricQualityes { get; set; }
        public DbSet<FabricTypes> FabricTypes { get; set; }
        public DbSet<LotMarking> LotMarkings { get; set; }

        public DbSet<YarnQuality> YarnQualities { get; set; }
        public DbSet<BagMarking> BagMarkings { get; set; }
        public DbSet<ConeMarking> ConeMarkings { get; set; }
        public DbSet<StoreLocation> StoreLocations { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<BuyerColor> BuyerColors { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<ActivityType> OrderActivityTypes { get; set; }
        public DbSet<SubActivityType> SubProcessActivityTypes { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<YarnManufacturer> YarnManufacturers { get; set; }
        public DbSet<InwardGatePass> InwardGatePasses { get; set; }
        public DbSet<InwardGatePassDetail> InwardGatePassDetails { get; set; }
        public DbSet<OutwardGatePass> OutwardGatePasses { get; set; }
        public DbSet<OutwardGatePassDetail> OutwardGatePassesDetails { get; set; }
        public DbSet<PPCPlanning> PPCPlannings { get; set; }
        public DbSet<IssueNote> IssueNotes { get; set; }
        public DbSet<IssueNoteDetail> IssueNoteDetails { get; set; }
        public DbSet<ReturnNote> ReturnNotes { get; set; }
        public DbSet<ReturnNoteDetail> ReturnNoteDetails { get; set; }
        public DbSet<ReportFilter> ReportFilters { get; set; }
        public DbSet<Reprocess> Reprocesses { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<FactoryPo> FactoryPo { get; set; }
        public DbSet<FactoryPoDetail> FactoryPoDetail { get; set; }
       
        public DbSet<ContractDetail> ContractDetails { get; set; }
        public virtual DbQuery<EcruYarnReceiveRepository_P1ViewModel> EcruYarnReceiveRepository_P1ViewModels { get; set; }
        public virtual DbQuery<EcruYarnConsumptionRepository_P2ViewModel> EcruYarnConsumptionRepository_P2ViewModels { get; set; }
        public virtual DbQuery<DailyProductionRepository_P3ViewModel> DailyProductionRepository_P3ViewModels { get; set; }
        public virtual DbQuery<DyedYarnDespatchedRepository_P5ViewModel> DyedYarnDespatchedRepository_P5ViewModels { get; set; }
        public virtual DbQuery<DyedStockRepository_P4ViewModel> DyedStockRepository_P4ViewModels { get; set; }
        public virtual DbQuery<DyedStockRepository1_P4ViewModel> DyedStockRepository1_P4ViewModels { get; set; }
        public virtual DbQuery<EcruYarnStockRepository_P7ViewModel> EcruYarnStockRepository_P7ViewModels { get; set; }
        public virtual DbQuery<EcruYarnStockRepository_P7ViewModel> EcruYarnStockRepository1_P7ViewModels { get; set; }
        public virtual DbQuery<EcruYarnStockRepository_P7ViewModel> EcruYarnStockRepository11_P7ViewModels { get; set; }
        public virtual DbQuery<GetbteweenRange_OGPRepositoryViewModel_P8> GetbteweenRange_OGPRepositoryViewModel_P8 { get; set; }
        public virtual DbQuery<IGPRecordFabric_P8ViewModel> IGPRecordFabric_P8ViewModel { get; set; }
        public virtual DbQuery<PPCPlaningAddData> PPCPlaningAddData { get; set; }
        public virtual DbQuery<IssuanceRecordRepository_P9ViewModel> IssuanceRecordRepository_P9ViewModel { get; set; }
        public virtual DbQuery<IssuanceRecordRepository_P9ViewModel> IssuanceRecord1Repository_P9ViewModel { get; set; }
        public virtual DbQuery<LPSIssuanceRepository_P8ViewModel> LPSIssuanceRepository_P8ViewModel { get; set; }
        public virtual DbQuery<EcruStockSummary_P10RepositoryViewModal> EcruStockSummary_P10RepositoryViewModal { get; set; }
        public virtual DbQuery<DyedStockSummary_P11RepositoryViewModal> DyedStockSummary_P11RepositoryViewModal { get; set; }
        public virtual DbQuery<FactoryPoKgRepository_P12ViewModel> FactoryPoKgRepository_P12ViewModel { get; set; }
        public virtual DbQuery<DyeingLossLotWiseViewModel> DyeingLossLotWiseViewModel { get; set; }
        public virtual DbQuery<DyeingLossPOWiseColorWiseViewModel> DyeingLossPOWiseColorWiseViewModel { get; set; }
        public virtual DbQuery<EcruFabricPOWiseKnitterWiseViewModel> EcruFabricPOWiseKnitterWiseViewModel { get; set; }
        public virtual DbQuery<EcruFabricIssuenceViewModel> EcruFabricIssuenceViewModel { get; set; }
        public virtual DbQuery<GetFabricDetailByLot> GetFabricDetailByLot { get; set; }
        public virtual DbQuery<EcruStockSummary_YarnCountGsm_RepositoryViewModal> EcruStockSummary_YarnCountGsm_RepositoryViewModal { get; set; }

        //public DbSet<ReturnToWinding> ReturnToWindings { get; set; }
        //public DbSet<ReturnToWindingDetail> ReturnToWindingDetails { get; set; }
        //public DbSet<ReturnToParty> ReturnToParties { get; set; }
        //public DbSet<ReturnToPartyDetail> ReturnToPartyDetails { get; set; }
        //public DbSet<Shade> Shades { get; set; }
        #endregion

        #region Yarn Dyeing

        public DbSet<MachineType> MachineTypes { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<ProcessType> ProcessTypees { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<Costing> Costings { get; set; }
        public DbSet<Dye> Dyes { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeDetail> RecipeDetails { get; set; }
        public DbSet<RecipeLPS> RecipeLPs { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
        public DbSet<RecipeFormatHeader> RecipeFormatHeaders { get; set; }
        public DbSet<RecipeFormatDetail> RecipeFormatDetails { get; set; }   
        public DbSet<Sticker> Sticker { get; set; }
        public virtual DbQuery<DyeingConsupmtionSummaryRepository_D1ViewModel> DyeingConsupmtionSummaryRepository_D1ViewModels { get; set; }
        public virtual DbQuery<DyeingRecipieRepository_D2ViewModel> DyeingRecipieRepository_D2ViewModels { get; set; }
        public virtual DbQuery<RecipeIssuedRepository_D3ViewModel> RecipeIssuedRepository_D3ViewModel { get; set; }
        public virtual DbQuery<StickerRepository_D4ViewModel> StickerRepository_D4ViewModel { get; set; }
        public virtual DbQuery<WithoutlpsRecipieRepository_D5ViewModel> WithoutlpsRecipieRepository_D5ViewModel { get; set; }
        public virtual DbQuery<Vw_RecipeNo_YarnDyingViewModel> Vw_RecipeNo_YarnDying { get; set; }
        public virtual DbQuery<CKL1POViewModelRepository> CKL1POViewModelRepository { get; set; }
        public virtual DbQuery<RecipeHeaderViewModelRepository> RecipeHeaderViewModelRepository { get; set; }
        public virtual DbQuery<RecipeLPSViewModelRepository> RecipeLPSViewModelRepository { get; set; }
        public virtual DbQuery<RecipeDetailViewModelRepository> RecipeDetailViewModelRepository { get; set; }
        public virtual DbQuery<RecipeIndexViewModelRepository> RecipeIndexViewModelRepository { get; set; }
        public virtual DbQuery<Dy> DyeingDetailConsumptionRepository_ViewModel { get; set; }
        public virtual DbQuery<DyeingRecipeReprocess_ViewModel> DyeingRecipeReprocess_ViewModel { get; set; }
        public virtual DbQuery<ChemicalIssuanceDetail_ViewModel> ChemicalIssuanceDetail_ViewModel { get; set; }
        public virtual DbQuery<FabricDyeingWeighDetail_ViewModel> FabricDyeingWeighDetail_ViewModel { get; set; }
        public virtual DbQuery<TotalRecipes_ViewModel> TotalRecipes_ViewModel { get; set; }

        #endregion

        #region Chemical Store
        public DbSet<Supplier> Suppliers { get; set; }
        //public DbSet<LCImportInTr> LCImportInTrs { get; set; }
        //public DbSet<LCImportInTrDetail> LCImportInTrDetails { get; set; }
        //public DbSet<ChemicalDilutionTr> ChemicalDilutionTrs { get; set; }
        //public DbSet<ChemicalDilutionTrDetail> ChemicalDilutionTrDetails { get; set; }
        //public DbSet<InHouseIssue> InHouseIssues { get; set; }
        //public DbSet<InHouseIssueDetail> InHouseIssueDetails { get; set; }
        //public DbSet<InterUnitOutTr> InterUnitOutTrs { get; set; }
        //public DbSet<InterUnitOutTrDetail> InterUnitOutTrDetails { get; set; }
        //public DbSet<LoanPartyGivenOutTr> LoanPartyGivenOutTrs { get; set; }
        //public DbSet<LoanPartyGivenOutTrDetail> LoanPartyGivenOutTrDetails { get; set; }
        //public DbSet<LoanPartyReturnInTr> LoanPartyReturnInTrs { get; set; }
        //public DbSet<LoanPartyReturnInTrDetail> LoanPartyReturnInTrDetails { get; set; }
        //public DbSet<LoanTakenInTr> LoanTakenInTrs { get; set; }
        //public DbSet<LoanTakenInTrDetail> LoanTakenInTrDetails { get; set; }
        //public DbSet<LoanTakenReturnOutTr> LoanTakenReturnOutTrs { get; set; }
        //public DbSet<LoanTakenReturnOutTrDetail> LoanTakenReturnOutTrDetails { get; set; }
        //public DbSet<LocalPurchaseInTr> LocalPurchaseInTrs { get; set; }
        //public DbSet<LocalPurchaseInTrDetail> LocalPurchaseInTrDetails { get; set; }
        //public DbSet<ChemicalIssuanceRecipeTr> ChemicalIssuanceRecipeTrs { get; set; }
        //public DbSet<ChemicalIssuanceRecipeTrDetail> ChemicalIssuanceRecipeTrDetails { get; set; }
        //public DbSet<DyesChemicalOpenning> DyesChemicalOpennings { get; set; }
        //public DbSet<DyesChemicalOpenningDetail> DyesChemicalOpenningDetails { get; set; }
        //public DbSet<StoreReturnNote> StoreReturnNotes { get; set; }
        //public DbSet<StoreReturnNoteDetail> StoreReturnNoteDetails { get; set; }
        //public DbSet<TrLinkerMaster>TrLinkerMasters { get; set; }
        public DbSet<DyeChemicalTr> DyeChemicalTrs{ get; set; }
        public DbSet<DyeChemicalTrDetail>DyeChemicalTrDetails{ get; set; }
        public virtual DbQuery<LoanTakenInOutRepository_C4ViewModel>  loanTakenInOutC_4ViewModels { get; set; }
        public virtual DbQuery<DyeSummaryRepository_C1ViewModel> DyeSummaryRepository_C1ViewModels { get; set; }
        public virtual DbQuery<StockDetailOutRepository_C3ViewModel> StockDetailOutRepository_C3ViewModels { get; set; }
        public virtual DbQuery<StockDetailInRepository_C2ViewModel> StockDetailInRepository_C2ViewModels{ get; set; }
        public virtual DbQuery<LoanPartyInOUTRepository_C5ViewModel> LoanPartyInOUTRepository_C5ViewModels { get; set; }
        public virtual DbQuery<NullRateQtyRepository_C6ViewModel> NullRateQtyRepository_C6ViewModels { get; set; }
        public virtual DbQuery<DyeChemicalDetailRepository_C7ViewModel> DyeChemicalDetailRepository_C7ViewModels { get; set; }
        public virtual DbQuery<StoreRecipeSortReportReporistory_ViewModel> StoreRecipeSortReportReporistory_ViewModel { get; set; }
        public virtual DbQuery<StoreRecipeChemicalConsumptionReportRepository_ViewModel> StoreRecipeChemicalConsumptionReportRepository_ViewModel { get; set; }
        public virtual DbQuery<StockOutSummaryReportRepository_ViewModel> StockOutSummaryReportRepository_ViewModel { get; set; }
        public virtual DbQuery<StockOutSummaryReportRepository_ViewModel1> StockOutSummaryReportRepository_ViewModel1 { get; set; }
        public virtual DbQuery<StockOutSummaryReportRepository_ViewModel2> StockOutSummaryReportRepository_ViewModel2 { get; set; }
        public virtual DbQuery<DyeChemicalDetailsTrTypeWiseRepository_ViewModel> DyeChemicalDetailsTrTypeWiseRepository_ViewModels { get; set; }
        public virtual DbQuery<DetailOpenStockReportRepository_C11ViewModel> DetailOpenStockReportRepository_C11ViewModels { get; set; }
        public virtual DbQuery<DyeChemicalTotal_Balance_ViewModel> dyeChemicalTotal_Balances { get; set; }
        public virtual DbQuery<DyeTotal_Balance_ViewModel> dyeTotal_Balances { get; set; }
        //public virtual DbQuery<ChemicalDyeIdSelectionData> ChemicalDyeIdSelectionDatas { get; set; }
        public virtual DbQuery<ChemicalStoreLedgerRepository_ViewModel> ChemicalStoreLedgerRepository_ViewModels { get; set; }

        public virtual DbQuery<Vw_Chemical_UncopiedGatePasINViewModel> Vw_Chemical_UncopiedGatePasIN { get; set; }

        public virtual DbQuery<ManualRateUpdate> ManualRateUpdate { get; set; }
        public virtual DbQuery<DyeChemicalLoanReturnOut_RemainingBlncViewModel> DyeChemicalLoanReturnOut_RemainingBlncViewModels { get; set; }
        public virtual DbQuery<ReturnedIdViewModel> ReturnedIdViewModels { get; set; }
        public virtual DbQuery<Vw_LoanReturnOut_PartiallyDispatchedViewModel> Vw_LoanReturnOut_PartiallyDispatched { get; set; }


        public virtual DbQuery<LeatestRateDyesandChemicalsRepository_C14ViewModel> LeatestRateDyesandChemicalsRepository_C14 { get; set; }
        public virtual DbQuery<DyeingProductionAndCosting_ViewModel> DyeingProductionAndCosting { get; set; }
        public virtual DbQuery<DyesAndChemicalConsumption_ViewModel> DyesAndChemicalConsumption_ViewModel { get; set; }
        public DbSet<DyeingEnergyConsumption> DyeingEnergyConsumptions { get; set; }
        public virtual DbQuery<DyesChemicalAndEnergyConsumption_ViewModel> DyesChemicalAndEnergyConsumption_ViewModel { get; set; }
        public virtual DbQuery<Vw_RecipeLpsDetail> Vw_RecipeLpsDetail { get; set; }
        //public virtual DbQuery<void> NullRateOrQty { get; set; }
        #endregion

        #region Gate 
        //public DbSet<GateIgp> GateIgps { get; set; }
        //public DbSet<GateIgpDetail> GateIgpDetails { get; set; }
        //public DbSet<GateOgp> GateOgps { get; set; }
        //public DbSet<GateOgpDetail> GateOgpDetails { get; set; }
        public DbSet<GateTr> GateTrs { get; set; }
        public DbSet<GateTrDetail> GateTrDetails { get; set; }
        public DbSet<GatePassType> GatePassType { get; set; }
        //public virtual DbQuery<AccountUserAuthentication> AccountUserAuthentications { get; set; }
        #endregion

        #region Premisis
        public DbSet<Branches> Branches { get; set; }
        #endregion

        #region Analysis
        public DbSet<AnalysisType> AnalysisType { get; set; }
        public DbSet<Defect> Defect { get; set; }
        public DbSet<DefectAnalysis> DefectAnalysis { get; set; }
      
        #endregion
    }

    //internal class DbMigrationsFactory : IDesignTimeDbContextFactory<AppDbContext> {
    //    private string _connectionString = "";
    //    public AppDbContext CreateDbContext(string[] args) {
    //        var builder = new DbContextOptionsBuilder<AppDbContext>();
    //        // build configurations
    //        var configBuilder = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json", false);
    //        // build root
    //        var root = configBuilder.Build();
    //        _connectionString = root.GetConnectionString(AppDbContext.APP_CON_NAME);
    //        builder.UseSqlServer(_connectionString);
    //        return new AppDbContext(options: builder.Options);
    //    }
    //}
}
