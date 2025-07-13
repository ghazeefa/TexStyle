using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TexStyle.Infrastructure.Migrations
{
    public partial class ggu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BagMarkings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagMarkings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chemicals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    Selected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConeMarkings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConeMarkings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Costings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MIS = table.Column<double>(nullable: false),
                    Gas = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Electricity = table.Column<double>(nullable: false),
                    SalaryAndWage = table.Column<double>(nullable: false),
                    FurnaceCharges = table.Column<double>(nullable: false),
                    ExportQuantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defect",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defect", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dyes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    Selected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dyes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FabricQualityes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricQualityes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FabricTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GateActivityType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsLoanINActivity = table.Column<bool>(nullable: false),
                    IsPurchaseActivity = table.Column<bool>(nullable: false),
                    IsLoanOutActivity = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GatePassType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GatePassType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "knittingPartyes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_knittingPartyes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Capacity = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderActivityTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SubAccount = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ControlAccount = table.Column<long>(nullable: false),
                    IsHeader = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessTypees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessTypees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeSteps",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sticker",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LPSId = table.Column<long>(nullable: false),
                    Cons = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sticker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreLocations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnManufacturers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnManufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnQualities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnQualities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountRoleClaims_AccountRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoleClaims_AccountRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DefectAnalysis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NoOfDefects = table.Column<string>(nullable: true),
                    AnalysisTypeID = table.Column<long>(nullable: false),
                    DefectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefectAnalysis_AnalysisType_AnalysisTypeID",
                        column: x => x.AnalysisTypeID,
                        principalTable: "AnalysisType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefectAnalysis_Defect_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Chambers = table.Column<int>(nullable: false),
                    ReelSpeed = table.Column<int>(nullable: false),
                    MachineTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubProcessActivityTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProcessActivityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProcessActivityTypes_OrderActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "OrderActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PartyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buyers_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PartyId = table.Column<long>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeFormatHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsYarn = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LiquorRate = table.Column<decimal>(nullable: false),
                    ProcessTypeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeFormatHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeFormatHeaders_ProcessTypees_ProcessTypeId",
                        column: x => x.ProcessTypeId,
                        principalTable: "ProcessTypees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuyerColors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BuyerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerColors_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutwardGatePasses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OgpDate = table.Column<DateTime>(nullable: false),
                    IsReturnToParty = table.Column<bool>(nullable: false),
                    InvoiceNo = table.Column<long>(nullable: true),
                    InvoiceAmount = table.Column<decimal>(nullable: true),
                    InvoiceDescription = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    IsForFinishing = table.Column<bool>(nullable: false),
                    IsAfterFinishing = table.Column<bool>(nullable: false),
                    IsReprocessed = table.Column<bool>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false),
                    IsReWaxRecheck = table.Column<bool>(nullable: false),
                    IsAfterComercialFinishing = table.Column<bool>(nullable: false),
                    ActivityTypeId = table.Column<long>(nullable: true),
                    YarnTypeId = table.Column<long>(nullable: true),
                    ReceivingPerson = table.Column<string>(nullable: true),
                    VehicleNo = table.Column<string>(nullable: true),
                    IDCard = table.Column<string>(nullable: true),
                    IsReCheck = table.Column<bool>(nullable: false),
                    PartyId = table.Column<long>(nullable: true),
                    FabricTypeId = table.Column<long>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true),
                    OGPReffNO = table.Column<long>(nullable: true),
                    BilityNo = table.Column<string>(nullable: true),
                    SerialNo = table.Column<string>(nullable: true),
                    TotalWeight = table.Column<decimal>(nullable: true),
                    LotNo = table.Column<int>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutwardGatePasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutwardGatePasses_OrderActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "OrderActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePasses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePasses_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePasses_FabricTypes_FabricTypeId",
                        column: x => x.FabricTypeId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePasses_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePasses_YarnTypes_YarnTypeId",
                        column: x => x.YarnTypeId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeFormatDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RecipeFormatHeaderId = table.Column<long>(nullable: true),
                    Sno = table.Column<long>(nullable: false),
                    DyeId = table.Column<long>(nullable: true),
                    ChemicalId = table.Column<long>(nullable: true),
                    RecipeStepId = table.Column<long>(nullable: true),
                    Gpl = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeFormatDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeFormatDetails_Chemicals_ChemicalId",
                        column: x => x.ChemicalId,
                        principalTable: "Chemicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeFormatDetails_Dyes_DyeId",
                        column: x => x.DyeId,
                        principalTable: "Dyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeFormatDetails_RecipeFormatHeaders_RecipeFormatHeaderId",
                        column: x => x.RecipeFormatHeaderId,
                        principalTable: "RecipeFormatHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeFormatDetails_RecipeSteps_RecipeStepId",
                        column: x => x.RecipeStepId,
                        principalTable: "RecipeSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BuyerId = table.Column<long>(nullable: true),
                    YarnCountId = table.Column<long>(nullable: true),
                    ColorId = table.Column<long>(nullable: true),
                    Quanitity = table.Column<decimal>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ContractId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDetails_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_BuyerColors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "BuyerColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_YarnTypes_YarnCountId",
                        column: x => x.YarnCountId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FactoryPo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false),
                    BuyerId = table.Column<long>(nullable: true),
                    GSM = table.Column<long>(nullable: true),
                    Po = table.Column<long>(nullable: true),
                    BuyerColorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryPo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactoryPo_BuyerColors_BuyerColorId",
                        column: x => x.BuyerColorId,
                        principalTable: "BuyerColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FactoryPo_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FactoryPoDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Sno = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TearWeightInKg = table.Column<decimal>(nullable: false),
                    NetWeightInKg = table.Column<decimal>(nullable: false),
                    Weight = table.Column<long>(nullable: true),
                    Dia = table.Column<long>(nullable: true),
                    GSM = table.Column<long>(nullable: true),
                    FactoryPoId = table.Column<long>(nullable: true),
                    FabricTypesId = table.Column<long>(nullable: true),
                    FabricQualityId = table.Column<long>(nullable: true),
                    BuyerColorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryPoDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactoryPoDetail_BuyerColors_BuyerColorId",
                        column: x => x.BuyerColorId,
                        principalTable: "BuyerColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FactoryPoDetail_FabricQualityes_FabricQualityId",
                        column: x => x.FabricQualityId,
                        principalTable: "FabricQualityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FactoryPoDetail_FabricTypes_FabricTypesId",
                        column: x => x.FabricTypesId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FactoryPoDetail_FactoryPo_FactoryPoId",
                        column: x => x.FactoryPoId,
                        principalTable: "FactoryPo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true),
                    RoleId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountUserRoles_AccountRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUserRoles_AccountRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AccountUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "IssueNotes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    IRNO = table.Column<int>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    IsReprocessed = table.Column<bool>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueNotes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PPCPlannings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LotNo = table.Column<int>(nullable: false),
                    IssuedDate = table.Column<DateTime>(nullable: false),
                    Cones = table.Column<int>(nullable: false),
                    Kgs = table.Column<decimal>(nullable: false),
                    PartyPONo = table.Column<string>(nullable: true),
                    StyleNo = table.Column<string>(nullable: true),
                    ProductionTypeId = table.Column<long>(nullable: true),
                    PurchaseOrderId = table.Column<long>(nullable: true),
                    PartyId = table.Column<long>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true),
                    BuyerColorId = table.Column<long>(nullable: true),
                    YarnTypeId = table.Column<long>(nullable: true),
                    YarnQualityId = table.Column<long>(nullable: true),
                    YarnManufacturerId = table.Column<long>(nullable: true),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    InwardGatePassDetailId = table.Column<long>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    FabricTypeId = table.Column<long>(nullable: true),
                    FabricQualityId = table.Column<long>(nullable: true),
                    KnitingPartyId = table.Column<long>(nullable: true),
                    Weight = table.Column<long>(nullable: true),
                    GSM = table.Column<long>(nullable: true),
                    Dia = table.Column<long>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true),
                    NoOfRolls = table.Column<decimal>(nullable: true),
                    FabricTypesId = table.Column<long>(nullable: true),
                    IsCancel = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPCPlannings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_BuyerColors_BuyerColorId",
                        column: x => x.BuyerColorId,
                        principalTable: "BuyerColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_FabricQualityes_FabricQualityId",
                        column: x => x.FabricQualityId,
                        principalTable: "FabricQualityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_FabricTypes_FabricTypeId",
                        column: x => x.FabricTypeId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_FabricTypes_FabricTypesId",
                        column: x => x.FabricTypesId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_knittingPartyes_KnitingPartyId",
                        column: x => x.KnitingPartyId,
                        principalTable: "knittingPartyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_ProductionType_ProductionTypeId",
                        column: x => x.ProductionTypeId,
                        principalTable: "ProductionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_YarnManufacturers_YarnManufacturerId",
                        column: x => x.YarnManufacturerId,
                        principalTable: "YarnManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_YarnQualities_YarnQualityId",
                        column: x => x.YarnQualityId,
                        principalTable: "YarnQualities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PPCPlannings_YarnTypes_YarnTypeId",
                        column: x => x.YarnTypeId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LotMarkings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RollNo = table.Column<decimal>(nullable: false),
                    NoOfRolls = table.Column<decimal>(nullable: false),
                    Kgs = table.Column<long>(nullable: false),
                    IssuanceDate = table.Column<DateTime>(nullable: true),
                    IsIssued = table.Column<bool>(nullable: false),
                    PPCPlanningId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotMarkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotMarkings_PPCPlannings_PPCPlanningId",
                        column: x => x.PPCPlanningId,
                        principalTable: "PPCPlannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RollMarkings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NoOfRolls = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PPCPlanningId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollMarkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RollMarkings_PPCPlannings_PPCPlanningId",
                        column: x => x.PPCPlanningId,
                        principalTable: "PPCPlannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RollMarkingDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RollNo = table.Column<decimal>(nullable: false),
                    EcruKgs = table.Column<long>(nullable: false),
                    DyedKgs = table.Column<long>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    RollMarkingId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollMarkingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RollMarkingDetails_RollMarkings_RollMarkingId",
                        column: x => x.RollMarkingId,
                        principalTable: "RollMarkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Po = table.Column<long>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    YarnTypeId = table.Column<long>(nullable: true),
                    YarnQualityId = table.Column<long>(nullable: true),
                    BuyerColorId = table.Column<long>(nullable: true),
                    SeasonId = table.Column<long>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    FabricTypeId = table.Column<long>(nullable: true),
                    FabricQualityId = table.Column<long>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true),
                    PartyId = table.Column<long>(nullable: true),
                    PartyId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_BuyerColors_BuyerColorId",
                        column: x => x.BuyerColorId,
                        principalTable: "BuyerColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_FabricQualityes_FabricQualityId",
                        column: x => x.FabricQualityId,
                        principalTable: "FabricQualityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_FabricTypes_FabricTypeId",
                        column: x => x.FabricTypeId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Parties_PartyId1",
                        column: x => x.PartyId1,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_YarnQualities_YarnQualityId",
                        column: x => x.YarnQualityId,
                        principalTable: "YarnQualities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_YarnTypes_YarnTypeId",
                        column: x => x.YarnTypeId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    No = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    LiquorRate = table.Column<decimal>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    IsReprocessed = table.Column<bool>(nullable: false),
                    LotNo = table.Column<int>(nullable: true),
                    XRefRecipe = table.Column<decimal>(nullable: false),
                    MachineTypeId = table.Column<long>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    PartyId = table.Column<long>(nullable: true),
                    BuyerColorId = table.Column<long>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    RecipeFormatId = table.Column<long>(nullable: true),
                    Weight = table.Column<decimal>(nullable: false),
                    Cones = table.Column<int>(nullable: false),
                    IsYarn = table.Column<bool>(nullable: true),
                    IsWithoutLPS = table.Column<bool>(nullable: true),
                    BranchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_BuyerColors_BuyerColorId",
                        column: x => x.BuyerColorId,
                        principalTable: "BuyerColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_RecipeFormatHeaders_RecipeFormatId",
                        column: x => x.RecipeFormatId,
                        principalTable: "RecipeFormatHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RecipeId = table.Column<long>(nullable: true),
                    Sno = table.Column<long>(nullable: false),
                    DyeId = table.Column<long>(nullable: true),
                    ChemicalId = table.Column<long>(nullable: true),
                    RecipeStepId = table.Column<long>(nullable: true),
                    LotNo = table.Column<int>(nullable: true),
                    Gpl = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18, 6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeDetails_Chemicals_ChemicalId",
                        column: x => x.ChemicalId,
                        principalTable: "Chemicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeDetails_Dyes_DyeId",
                        column: x => x.DyeId,
                        principalTable: "Dyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeDetails_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeDetails_RecipeSteps_RecipeStepId",
                        column: x => x.RecipeStepId,
                        principalTable: "RecipeSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: true),
                    DateTo = table.Column<DateTime>(nullable: true),
                    YarnTypeId = table.Column<long>(nullable: true),
                    YarnQualityId = table.Column<long>(nullable: true),
                    FabricTypeId = table.Column<long>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true),
                    FabricQualityId = table.Column<long>(nullable: true),
                    YarnPartyId = table.Column<long>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    AnalysisTypeId = table.Column<long>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportFilters_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFilters_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFilters_FabricQualityes_FabricQualityId",
                        column: x => x.FabricQualityId,
                        principalTable: "FabricQualityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFilters_FabricTypes_FabricTypeId",
                        column: x => x.FabricTypeId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFilters_Parties_YarnPartyId",
                        column: x => x.YarnPartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFilters_YarnQualities_YarnQualityId",
                        column: x => x.YarnQualityId,
                        principalTable: "YarnQualities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFilters_YarnTypes_YarnTypeId",
                        column: x => x.YarnTypeId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ReportFilterId = table.Column<int>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_ReportFilters_ReportFilterId",
                        column: x => x.ReportFilterId,
                        principalTable: "ReportFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReturnNotes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    IsReprocessed = table.Column<bool>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true),
                    TotalWeight = table.Column<decimal>(nullable: true),
                    LotNo = table.Column<int>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnNotes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnNotes_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnNotes_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InwardGatePassDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Sno = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PartyRefNo = table.Column<string>(nullable: true),
                    LotNo = table.Column<int>(nullable: false),
                    TearWeightInKg = table.Column<decimal>(nullable: false),
                    NetWeightInKg = table.Column<decimal>(nullable: false),
                    IsZeroBalance = table.Column<bool>(nullable: false),
                    StoreLocationId = table.Column<long>(nullable: true),
                    Bags = table.Column<decimal>(nullable: false),
                    BuyerPO = table.Column<string>(nullable: true),
                    Weight = table.Column<long>(nullable: true),
                    GSM = table.Column<long>(nullable: true),
                    Dia = table.Column<long>(nullable: true),
                    YarnTypeId = table.Column<long>(nullable: true),
                    YarnQualityId = table.Column<long>(nullable: true),
                    BagMarkingId = table.Column<long>(nullable: true),
                    ConeMarkingId = table.Column<long>(nullable: true),
                    YarnManufacturerId = table.Column<long>(nullable: true),
                    InwardGatePassId = table.Column<long>(nullable: true),
                    FabricTypesId = table.Column<long>(nullable: true),
                    FabricQualityId = table.Column<long>(nullable: true),
                    RollMarkingId = table.Column<long>(nullable: true),
                    NoOfRolls = table.Column<decimal>(nullable: true),
                    KnitingPartyId = table.Column<long>(nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InwardGatePassDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_OrderActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "OrderActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_BagMarkings_BagMarkingId",
                        column: x => x.BagMarkingId,
                        principalTable: "BagMarkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_ConeMarkings_ConeMarkingId",
                        column: x => x.ConeMarkingId,
                        principalTable: "ConeMarkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_FabricQualityes_FabricQualityId",
                        column: x => x.FabricQualityId,
                        principalTable: "FabricQualityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_FabricTypes_FabricTypesId",
                        column: x => x.FabricTypesId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_knittingPartyes_KnitingPartyId",
                        column: x => x.KnitingPartyId,
                        principalTable: "knittingPartyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_RollMarkings_RollMarkingId",
                        column: x => x.RollMarkingId,
                        principalTable: "RollMarkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_StoreLocations_StoreLocationId",
                        column: x => x.StoreLocationId,
                        principalTable: "StoreLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_YarnManufacturers_YarnManufacturerId",
                        column: x => x.YarnManufacturerId,
                        principalTable: "YarnManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_YarnQualities_YarnQualityId",
                        column: x => x.YarnQualityId,
                        principalTable: "YarnQualities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePassDetails_YarnTypes_YarnTypeId",
                        column: x => x.YarnTypeId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reprocesses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Kgs = table.Column<decimal>(nullable: false),
                    Cones = table.Column<int>(nullable: false),
                    NoOfRolls = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PPCPlanningId = table.Column<long>(nullable: true),
                    InwardGatePassDetailId = table.Column<long>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reprocesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reprocesses_InwardGatePassDetails_InwardGatePassDetailId",
                        column: x => x.InwardGatePassDetailId,
                        principalTable: "InwardGatePassDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reprocesses_PPCPlannings_PPCPlanningId",
                        column: x => x.PPCPlanningId,
                        principalTable: "PPCPlannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueNoteDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Kgs = table.Column<decimal>(nullable: false),
                    Bags = table.Column<decimal>(nullable: false),
                    PPCPlanningId = table.Column<long>(nullable: true),
                    StoreLocationId = table.Column<long>(nullable: true),
                    IssueNoteId = table.Column<long>(nullable: false),
                    ReprocessId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueNoteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueNoteDetails_IssueNotes_IssueNoteId",
                        column: x => x.IssueNoteId,
                        principalTable: "IssueNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueNoteDetails_PPCPlannings_PPCPlanningId",
                        column: x => x.PPCPlanningId,
                        principalTable: "PPCPlannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueNoteDetails_Reprocesses_ReprocessId",
                        column: x => x.ReprocessId,
                        principalTable: "Reprocesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueNoteDetails_StoreLocations_StoreLocationId",
                        column: x => x.StoreLocationId,
                        principalTable: "StoreLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutwardGatePassesDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Kgs = table.Column<decimal>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: true),
                    Bags = table.Column<decimal>(nullable: false),
                    NoOfRolls = table.Column<decimal>(nullable: true),
                    LotNo = table.Column<int>(nullable: true),
                    PPCPlanningId = table.Column<long>(nullable: true),
                    ReprocessId = table.Column<long>(nullable: true),
                    InwardGatePassDetailId = table.Column<long>(nullable: true),
                    YarnTypeId = table.Column<long>(nullable: true),
                    FabricTypesId = table.Column<long>(nullable: true),
                    OutwardGatePassId = table.Column<long>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: true),
                    FactoryPoDetailId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutwardGatePassesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_FabricTypes_FabricTypesId",
                        column: x => x.FabricTypesId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_FactoryPoDetail_FactoryPoDetailId",
                        column: x => x.FactoryPoDetailId,
                        principalTable: "FactoryPoDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_InwardGatePassDetails_InwardGatePassDetailId",
                        column: x => x.InwardGatePassDetailId,
                        principalTable: "InwardGatePassDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_OutwardGatePasses_OutwardGatePassId",
                        column: x => x.OutwardGatePassId,
                        principalTable: "OutwardGatePasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_PPCPlannings_PPCPlanningId",
                        column: x => x.PPCPlanningId,
                        principalTable: "PPCPlannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_Reprocesses_ReprocessId",
                        column: x => x.ReprocessId,
                        principalTable: "Reprocesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutwardGatePassesDetails_YarnTypes_YarnTypeId",
                        column: x => x.YarnTypeId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeLPs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RecipeId = table.Column<long>(nullable: false),
                    LPSId = table.Column<long>(nullable: true),
                    ReprocessId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeLPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeLPs_PPCPlannings_LPSId",
                        column: x => x.LPSId,
                        principalTable: "PPCPlannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeLPs_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeLPs_Reprocesses_ReprocessId",
                        column: x => x.ReprocessId,
                        principalTable: "Reprocesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReturnNoteDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Kgs = table.Column<decimal>(nullable: false),
                    Bags = table.Column<decimal>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    ReturnNoteId = table.Column<long>(nullable: false),
                    PPCPlanningId = table.Column<long>(nullable: true),
                    StoreLocationId = table.Column<long>(nullable: true),
                    ReceivedQualityStatus = table.Column<long>(nullable: true),
                    ReprocessId = table.Column<long>(nullable: true),
                    EcruKgs = table.Column<decimal>(nullable: true),
                    NoOfRolls = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnNoteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnNoteDetails_PPCPlannings_PPCPlanningId",
                        column: x => x.PPCPlanningId,
                        principalTable: "PPCPlannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnNoteDetails_Reprocesses_ReprocessId",
                        column: x => x.ReprocessId,
                        principalTable: "Reprocesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnNoteDetails_ReturnNotes_ReturnNoteId",
                        column: x => x.ReturnNoteId,
                        principalTable: "ReturnNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnNoteDetails_StoreLocations_StoreLocationId",
                        column: x => x.StoreLocationId,
                        principalTable: "StoreLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GateTrs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Sno = table.Column<long>(nullable: false),
                    DriverName = table.Column<string>(nullable: true),
                    VehicleNo = table.Column<string>(nullable: true),
                    NICNo = table.Column<string>(nullable: true),
                    EmpNo = table.Column<string>(nullable: true),
                    IGPReffNo = table.Column<long>(nullable: true),
                    Xref = table.Column<long>(nullable: false),
                    IsReturnFromParty = table.Column<bool>(nullable: false),
                    IsForFinishing = table.Column<bool>(nullable: false),
                    IsAfterFinishing = table.Column<bool>(nullable: false),
                    IsForComercialFinishing = table.Column<bool>(nullable: false),
                    IsAfterComercialFinishing = table.Column<bool>(nullable: false),
                    IsReWaxRecheck = table.Column<bool>(nullable: false),
                    IsReprocessed = table.Column<bool>(nullable: false),
                    IsWithoutOGP = table.Column<bool>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    GateTrId = table.Column<long>(nullable: true),
                    PartyId = table.Column<long>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true),
                    GateActivityTypeId = table.Column<long>(nullable: true),
                    OutwardGatePassId = table.Column<long>(nullable: true),
                    GetDyeChemicalTrId = table.Column<long>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true),
                    BillityNo = table.Column<string>(nullable: true),
                    GatePassTypeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateTrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateTrs_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrs_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrs_GateActivityType_GateActivityTypeId",
                        column: x => x.GateActivityTypeId,
                        principalTable: "GateActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrs_GatePassType_GatePassTypeId",
                        column: x => x.GatePassTypeId,
                        principalTable: "GatePassType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrs_GateTrs_GateTrId",
                        column: x => x.GateTrId,
                        principalTable: "GateTrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrs_OutwardGatePasses_OutwardGatePassId",
                        column: x => x.OutwardGatePassId,
                        principalTable: "OutwardGatePasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrs_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DyeChemicalTrs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Sno = table.Column<long>(nullable: false),
                    TrType = table.Column<long>(nullable: false),
                    InvoiceNo = table.Column<long>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: true),
                    FairPrice = table.Column<decimal>(nullable: true),
                    DTRENo = table.Column<string>(nullable: true),
                    ElectricityCharges = table.Column<decimal>(nullable: true),
                    CableCharges = table.Column<decimal>(nullable: true),
                    DrumCharges = table.Column<decimal>(nullable: true),
                    LabourCharges = table.Column<decimal>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    IGPReffNo = table.Column<long>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    GateTrId = table.Column<long>(nullable: true),
                    DyeChemicalXrefTrId = table.Column<long>(nullable: true),
                    PartyId = table.Column<long>(nullable: true),
                    RecipeId = table.Column<long>(nullable: true),
                    RecipieIssuanceDate = table.Column<DateTime>(nullable: true),
                    IsCompleteIssued = table.Column<bool>(nullable: true),
                    IsCancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeChemicalTrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrs_DyeChemicalTrs_DyeChemicalXrefTrId",
                        column: x => x.DyeChemicalXrefTrId,
                        principalTable: "DyeChemicalTrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrs_GateTrs_GateTrId",
                        column: x => x.GateTrId,
                        principalTable: "GateTrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrs_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrs_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InwardGatePasses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IgpDate = table.Column<DateTime>(nullable: false),
                    BilityNo = table.Column<string>(nullable: true),
                    GateTrId = table.Column<long>(nullable: true),
                    PartyId = table.Column<long>(nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: true),
                    IsReturnfromParty = table.Column<bool>(nullable: false),
                    IsReWaxRecheck = table.Column<bool>(nullable: false),
                    IsReprocessed = table.Column<bool>(nullable: false),
                    IsForFinishing = table.Column<bool>(nullable: false),
                    IsAfterFinishing = table.Column<bool>(nullable: false),
                    IsForComercialFinishing = table.Column<bool>(nullable: false),
                    IsWithoutOGP = table.Column<bool>(nullable: false),
                    IsReturnToParty = table.Column<bool>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    IsYarn = table.Column<bool>(nullable: true),
                    GateReffId = table.Column<long>(nullable: true),
                    BuyerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InwardGatePasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InwardGatePasses_OrderActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "OrderActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePasses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePasses_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePasses_GateTrs_GateTrId",
                        column: x => x.GateTrId,
                        principalTable: "GateTrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InwardGatePasses_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DyeChemicalTrDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QtyDr = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    QtyCr = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    Packet = table.Column<decimal>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    IsDr = table.Column<bool>(nullable: false),
                    ChemicalId = table.Column<long>(nullable: true),
                    DyeChemicalTrId = table.Column<long>(nullable: true),
                    DyeId = table.Column<long>(nullable: true),
                    GateTrDetailId = table.Column<long>(nullable: true),
                    DyeChemicalXrefDetailTrId = table.Column<long>(nullable: true),
                    RecipeDetailId = table.Column<long>(nullable: true),
                    IsIssued = table.Column<bool>(nullable: true),
                    FinalQty = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    FinalAmount = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    DrId = table.Column<long>(nullable: true),
                    DrBalance = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeChemicalTrDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrDetails_Chemicals_ChemicalId",
                        column: x => x.ChemicalId,
                        principalTable: "Chemicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrDetails_DyeChemicalTrs_DyeChemicalTrId",
                        column: x => x.DyeChemicalTrId,
                        principalTable: "DyeChemicalTrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrDetails_DyeChemicalTrDetails_DyeChemicalXrefDetailTrId",
                        column: x => x.DyeChemicalXrefDetailTrId,
                        principalTable: "DyeChemicalTrDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrDetails_Dyes_DyeId",
                        column: x => x.DyeId,
                        principalTable: "Dyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyeChemicalTrDetails_RecipeDetails_RecipeDetailId",
                        column: x => x.RecipeDetailId,
                        principalTable: "RecipeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GateTrDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QtyDr = table.Column<decimal>(nullable: true),
                    Bags = table.Column<decimal>(nullable: true),
                    NoOfRolls = table.Column<decimal>(nullable: true),
                    QtyCr = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GateXrefDetailId = table.Column<long>(nullable: true),
                    Rate = table.Column<decimal>(nullable: true),
                    Packet = table.Column<decimal>(nullable: true),
                    DyeId = table.Column<long>(nullable: true),
                    ChemicalId = table.Column<long>(nullable: true),
                    YarnTypeId = table.Column<long>(nullable: true),
                    FabricTypeId = table.Column<long>(nullable: true),
                    GateTrId = table.Column<long>(nullable: true),
                    OGPGateTrDetailId = table.Column<long>(nullable: true),
                    OutwardGatePassDetailId = table.Column<long>(nullable: true),
                    DyeChemicalTrDetailId = table.Column<long>(nullable: true),
                    BillityNo = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateTrDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_Chemicals_ChemicalId",
                        column: x => x.ChemicalId,
                        principalTable: "Chemicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_DyeChemicalTrDetails_DyeChemicalTrDetailId",
                        column: x => x.DyeChemicalTrDetailId,
                        principalTable: "DyeChemicalTrDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_Dyes_DyeId",
                        column: x => x.DyeId,
                        principalTable: "Dyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_FabricTypes_FabricTypeId",
                        column: x => x.FabricTypeId,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_GateTrs_GateTrId",
                        column: x => x.GateTrId,
                        principalTable: "GateTrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_GateTrDetails_OGPGateTrDetailId",
                        column: x => x.OGPGateTrDetailId,
                        principalTable: "GateTrDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_OutwardGatePassesDetails_OutwardGatePassDetailId",
                        column: x => x.OutwardGatePassDetailId,
                        principalTable: "OutwardGatePassesDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateTrDetails_YarnTypes_YarnTypeId",
                        column: x => x.YarnTypeId,
                        principalTable: "YarnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccountRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 8, "607d0008-9ec6-4cf5-88a3-fbdbc2b12ac4", "Marketing Accounts User", null },
                    { 1, "b753abeb-4778-4f83-9b0e-5fc92f3cde23", "Developer", null },
                    { 2, "71d14195-0124-4286-8779-9595ac6d5c4b", "Administrator", null },
                    { 3, "d781c39d-7e69-4c94-b3f3-fa6b505df6aa", "Manager", null },
                    { 7, "6aa0445f-8f47-43e0-8a26-74021e949072", "Gate User", null },
                    { 5, "a4f17da9-1e6f-4226-b47b-700f9deddff3", "YD User", null },
                    { 6, "01842182-4309-4c95-bdbc-ae87b57b54a1", "CS User", null },
                    { 4, "8c98f02c-ee46-48d0-9e6b-32b4a587bd12", "PPC User", null }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccessFailedCount", "BranchId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsYarn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ReportFilterId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 8, 0, null, "239231d0-7694-4bf2-ae34-40ce6286af0b", "marketingaccountsuser@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "marketingaccountuser" },
                    { 7, 0, null, "11da5042-2b1d-4bcc-94b2-83cecaf20c7e", "gateuser@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "gateuser" },
                    { 6, 0, null, "d27f6906-b0d3-45f2-8c14-d68af23a4bde", "csuser@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "csuser" },
                    { 4, 0, null, "610d817f-6971-4fbc-90ee-738ca8a2a26f", "ppcuser@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "ppcuser" },
                    { 3, 0, null, "bc0c1f79-5e1a-4083-9223-81ef148d7c3f", "admintexstyle@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "admin" },
                    { 2, 0, null, "7381f024-689a-4bf2-97c0-a8765f2653e4", "developer@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "developer" },
                    { 1, 0, null, "b89497e4-d019-4855-b2a4-160f90f745ff", "developer@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "fatima" },
                    { 5, 0, null, "52b91c7e-22c6-4476-aad6-7a4146e07519", "yduser@gmail.com", false, null, false, null, null, null, "AQAAAAEAACcQAAAAECqMQJEOa6rYu/KAFqrTgdlBfVGTalH502NFdybd/AYzSB4UKe2jUQiHh7ExeGJGyg==", null, false, null, "3caaa674-fd7a-41c6-82fe-2e190f51a43f", false, "yduser" }
                });

            migrationBuilder.InsertData(
                table: "BagMarkings",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 191, DateTimeKind.Local).AddTicks(991), false, "Red", null });

            migrationBuilder.InsertData(
                table: "Chemicals",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "Rate", "Selected", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 211, DateTimeKind.Local).AddTicks(5413), false, "D CM", 1m, null, null });

            migrationBuilder.InsertData(
                table: "ConeMarkings",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 195, DateTimeKind.Local).AddTicks(5550), false, "Khaki", null });

            migrationBuilder.InsertData(
                table: "Costings",
                columns: new[] { "Id", "CreatedOn", "Date", "Electricity", "ExportQuantity", "FurnaceCharges", "Gas", "IsDeleted", "MIS", "SalaryAndWage", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 212, DateTimeKind.Local).AddTicks(9671), new DateTime(2022, 6, 11, 1, 9, 20, 211, DateTimeKind.Local).AddTicks(9292), 1.0, 1.0, 1.0, 1.0, false, 1.0, 1.0, null });

            migrationBuilder.InsertData(
                table: "Dyes",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "Rate", "Selected", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 210, DateTimeKind.Local).AddTicks(7703), false, "D D", 1m, null, null });

            migrationBuilder.InsertData(
                table: "GateActivityType",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "IsLoanINActivity", "IsLoanOutActivity", "IsPurchaseActivity", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(1016), false, false, false, false, "IGP Yarn", null },
                    { 3L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(3858), false, false, false, true, "LC IMPORT IN", null },
                    { 5L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(3912), false, true, false, false, "LOAN TAKEN IN", null },
                    { 2L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(3795), false, false, false, false, "OGP Yarn", null },
                    { 6L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(3988), false, false, false, true, "LOCAL PURCHASE IN", null },
                    { 7L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(4016), false, false, false, false, "INTER UNIT OUT", null },
                    { 8L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(4042), false, false, true, false, "LOAN PARTY GIVEN OUT", null },
                    { 9L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(4067), false, false, true, false, "LOAN TAKEN RETURN OUT", null },
                    { 4L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(3886), false, true, false, false, "LOAN PARTY RETURN IN", null }
                });

            migrationBuilder.InsertData(
                table: "GatePassType",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(6743), false, "Regular", null },
                    { 2L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(7166), false, "ReDyeing", null },
                    { 4L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(7239), false, "A", null },
                    { 5L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(7264), false, "B", null },
                    { 3L, new DateTime(2022, 6, 11, 1, 9, 20, 218, DateTimeKind.Local).AddTicks(7211), false, "After Raising", null }
                });

            migrationBuilder.InsertData(
                table: "MachineTypes",
                columns: new[] { "Id", "Capacity", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, null, new DateTime(2022, 6, 11, 1, 9, 20, 208, DateTimeKind.Local).AddTicks(8486), false, "D MT", null });

            migrationBuilder.InsertData(
                table: "OrderActivityTypes",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 203, DateTimeKind.Local).AddTicks(8708), false, "Default order type", null });

            migrationBuilder.InsertData(
                table: "Parties",
                columns: new[] { "Id", "ControlAccount", "CreatedOn", "IsDeleted", "IsHeader", "Name", "SubAccount", "UpdatedOn" },
                values: new object[] { 10009L, 10000L, new DateTime(2022, 6, 11, 1, 9, 20, 201, DateTimeKind.Local).AddTicks(2984), false, false, "It's Party In the House.", 0L, null });

            migrationBuilder.InsertData(
                table: "ProcessTypees",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 208, DateTimeKind.Local).AddTicks(5504), false, "D PT", null });

            migrationBuilder.InsertData(
                table: "ProductionType",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 197, DateTimeKind.Local).AddTicks(5866), false, "Sample", null },
                    { 2L, new DateTime(2022, 6, 11, 1, 9, 20, 198, DateTimeKind.Local).AddTicks(4048), false, "Production", null }
                });

            migrationBuilder.InsertData(
                table: "RecipeSteps",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 210, DateTimeKind.Local).AddTicks(254), false, "D STEP", null });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 202, DateTimeKind.Local).AddTicks(26), false, "Winter", null });

            migrationBuilder.InsertData(
                table: "StoreLocations",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 205, DateTimeKind.Local).AddTicks(3082), false, "R1", null });

            migrationBuilder.InsertData(
                table: "YarnManufacturers",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 196, DateTimeKind.Local).AddTicks(2060), false, "Default Manufacturer", null });

            migrationBuilder.InsertData(
                table: "YarnQualities",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 197, DateTimeKind.Local).AddTicks(2032), false, "Grey", null });

            migrationBuilder.InsertData(
                table: "YarnTypes",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 196, DateTimeKind.Local).AddTicks(6190), false, "12/s", null });

            migrationBuilder.InsertData(
                table: "AccountUserRoles",
                columns: new[] { "UserId", "RoleId", "RoleId1", "UserId1" },
                values: new object[,]
                {
                    { 1, 1, null, null },
                    { 2, 2, null, null },
                    { 3, 3, null, null },
                    { 4, 4, null, null },
                    { 5, 5, null, null },
                    { 6, 6, null, null },
                    { 7, 7, null, null },
                    { 8, 8, null, null }
                });

            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "PartyId", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 200, DateTimeKind.Local).AddTicks(1490), false, "Some Random Textile", 10009L, null });

            migrationBuilder.InsertData(
                table: "InwardGatePasses",
                columns: new[] { "Id", "ActivityTypeId", "BilityNo", "BranchId", "BuyerId", "CreatedOn", "GateReffId", "GateTrId", "IgpDate", "IsAfterFinishing", "IsCancel", "IsConfirm", "IsDeleted", "IsForComercialFinishing", "IsForFinishing", "IsReWaxRecheck", "IsReprocessed", "IsReturnToParty", "IsReturnfromParty", "IsWithoutOGP", "IsYarn", "PartyId", "UpdatedOn" },
                values: new object[] { 1L, 1L, null, null, null, new DateTime(2022, 6, 11, 1, 9, 20, 206, DateTimeKind.Local).AddTicks(7075), null, null, new DateTime(2022, 6, 11, 1, 9, 20, 206, DateTimeKind.Local).AddTicks(1312), false, false, false, false, false, false, true, false, false, false, false, null, 10009L, null });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "Chambers", "CreatedOn", "IsDeleted", "MachineTypeId", "ReelSpeed", "UpdatedOn" },
                values: new object[] { 1L, 1, new DateTime(2022, 6, 11, 1, 9, 20, 209, DateTimeKind.Local).AddTicks(4927), false, 1L, 1, null });

            migrationBuilder.InsertData(
                table: "RecipeFormatHeaders",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "IsYarn", "LiquorRate", "Name", "ProcessTypeId", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2022, 6, 11, 1, 9, 20, 214, DateTimeKind.Local).AddTicks(9318), false, false, 1m, "D F", 1L, null });

            migrationBuilder.InsertData(
                table: "SubProcessActivityTypes",
                columns: new[] { "Id", "ActivityTypeId", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, 1L, new DateTime(2022, 6, 11, 1, 9, 20, 204, DateTimeKind.Local).AddTicks(7026), false, "Default sub process type", null });

            migrationBuilder.InsertData(
                table: "BuyerColors",
                columns: new[] { "Id", "BuyerId", "CreatedOn", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1L, 1L, new DateTime(2022, 6, 11, 1, 9, 20, 198, DateTimeKind.Local).AddTicks(8972), false, "Darker Than Black", null });

            migrationBuilder.InsertData(
                table: "InwardGatePassDetails",
                columns: new[] { "Id", "ActivityTypeId", "BagMarkingId", "Bags", "BuyerPO", "ConeMarkingId", "CreatedOn", "Description", "Dia", "FabricQualityId", "FabricTypesId", "GSM", "InwardGatePassId", "IsDeleted", "IsZeroBalance", "KnitingPartyId", "LotNo", "NetWeightInKg", "NoOfRolls", "PartyRefNo", "RollMarkingId", "Sno", "StoreLocationId", "TearWeightInKg", "UpdatedOn", "Weight", "YarnManufacturerId", "YarnQualityId", "YarnTypeId" },
                values: new object[] { 1L, null, 1L, 9m, null, 1L, null, "Demo Description", null, null, null, null, 1L, false, false, null, 0, 90m, null, null, null, 0, 1L, 90m, null, null, 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "BranchId", "BuyerColorId", "Color", "Cones", "CreatedOn", "Date", "IsConfirmed", "IsDeleted", "IsReprocessed", "IsWithoutLPS", "IsYarn", "LiquorRate", "LotNo", "MachineTypeId", "No", "PartyId", "RecipeFormatId", "UpdatedOn", "UserId", "Weight", "XRefRecipe" },
                values: new object[] { 1L, null, null, null, 0, new DateTime(2022, 6, 11, 1, 9, 20, 215, DateTimeKind.Local).AddTicks(9503), new DateTime(2022, 6, 11, 1, 9, 20, 215, DateTimeKind.Local).AddTicks(8630), false, false, false, null, null, 1m, null, 1L, 0m, null, 1L, null, null, 0m, 0m });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "BranchId", "BuyerColorId", "BuyerId", "CreatedOn", "FabricQualityId", "FabricTypeId", "IsClosed", "IsDeleted", "IsYarn", "PartyId", "PartyId1", "Po", "SeasonId", "UpdatedOn", "UserId", "YarnQualityId", "YarnTypeId" },
                values: new object[] { 1L, null, 1L, null, new DateTime(2022, 6, 11, 1, 9, 20, 203, DateTimeKind.Local).AddTicks(3273), null, null, false, false, null, null, null, 0L, 1L, null, null, 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoleClaims_RoleId",
                table: "AccountRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoleClaims_RoleId1",
                table: "AccountRoleClaims",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AccountRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BranchId",
                table: "Accounts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Accounts",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Accounts",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ReportFilterId",
                table: "Accounts",
                column: "ReportFilterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserClaims_UserId",
                table: "AccountUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserClaims_UserId1",
                table: "AccountUserClaims",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserLogins_UserId",
                table: "AccountUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserLogins_UserId1",
                table: "AccountUserLogins",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserRoles_RoleId",
                table: "AccountUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserRoles_RoleId1",
                table: "AccountUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserRoles_UserId1",
                table: "AccountUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserTokens_UserId1",
                table: "AccountUserTokens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerColors_BuyerId",
                table: "BuyerColors",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_PartyId",
                table: "Buyers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_BuyerId",
                table: "ContractDetails",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_ColorId",
                table: "ContractDetails",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_ContractId",
                table: "ContractDetails",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_YarnCountId",
                table: "ContractDetails",
                column: "YarnCountId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PartyId",
                table: "Contracts",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectAnalysis_AnalysisTypeID",
                table: "DefectAnalysis",
                column: "AnalysisTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DefectAnalysis_DefectId",
                table: "DefectAnalysis",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrDetails_ChemicalId",
                table: "DyeChemicalTrDetails",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrDetails_DyeChemicalTrId",
                table: "DyeChemicalTrDetails",
                column: "DyeChemicalTrId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrDetails_DyeChemicalXrefDetailTrId",
                table: "DyeChemicalTrDetails",
                column: "DyeChemicalXrefDetailTrId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrDetails_DyeId",
                table: "DyeChemicalTrDetails",
                column: "DyeId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrDetails_GateTrDetailId",
                table: "DyeChemicalTrDetails",
                column: "GateTrDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrDetails_RecipeDetailId",
                table: "DyeChemicalTrDetails",
                column: "RecipeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrs_DyeChemicalXrefTrId",
                table: "DyeChemicalTrs",
                column: "DyeChemicalXrefTrId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrs_GateTrId",
                table: "DyeChemicalTrs",
                column: "GateTrId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrs_PartyId",
                table: "DyeChemicalTrs",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeChemicalTrs_RecipeId",
                table: "DyeChemicalTrs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryPo_BuyerColorId",
                table: "FactoryPo",
                column: "BuyerColorId");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryPo_BuyerId",
                table: "FactoryPo",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryPoDetail_BuyerColorId",
                table: "FactoryPoDetail",
                column: "BuyerColorId");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryPoDetail_FabricQualityId",
                table: "FactoryPoDetail",
                column: "FabricQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryPoDetail_FabricTypesId",
                table: "FactoryPoDetail",
                column: "FabricTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryPoDetail_FactoryPoId",
                table: "FactoryPoDetail",
                column: "FactoryPoId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_ChemicalId",
                table: "GateTrDetails",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_DyeChemicalTrDetailId",
                table: "GateTrDetails",
                column: "DyeChemicalTrDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_DyeId",
                table: "GateTrDetails",
                column: "DyeId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_FabricTypeId",
                table: "GateTrDetails",
                column: "FabricTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_GateTrId",
                table: "GateTrDetails",
                column: "GateTrId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_OGPGateTrDetailId",
                table: "GateTrDetails",
                column: "OGPGateTrDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_OutwardGatePassDetailId",
                table: "GateTrDetails",
                column: "OutwardGatePassDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrDetails_YarnTypeId",
                table: "GateTrDetails",
                column: "YarnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_BranchId",
                table: "GateTrs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_BuyerId",
                table: "GateTrs",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_GateActivityTypeId",
                table: "GateTrs",
                column: "GateActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_GatePassTypeId",
                table: "GateTrs",
                column: "GatePassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_GateTrId",
                table: "GateTrs",
                column: "GateTrId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_GetDyeChemicalTrId",
                table: "GateTrs",
                column: "GetDyeChemicalTrId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_OutwardGatePassId",
                table: "GateTrs",
                column: "OutwardGatePassId");

            migrationBuilder.CreateIndex(
                name: "IX_GateTrs_PartyId",
                table: "GateTrs",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_ActivityTypeId",
                table: "InwardGatePassDetails",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_BagMarkingId",
                table: "InwardGatePassDetails",
                column: "BagMarkingId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_ConeMarkingId",
                table: "InwardGatePassDetails",
                column: "ConeMarkingId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_FabricQualityId",
                table: "InwardGatePassDetails",
                column: "FabricQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_FabricTypesId",
                table: "InwardGatePassDetails",
                column: "FabricTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_InwardGatePassId",
                table: "InwardGatePassDetails",
                column: "InwardGatePassId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_KnitingPartyId",
                table: "InwardGatePassDetails",
                column: "KnitingPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_RollMarkingId",
                table: "InwardGatePassDetails",
                column: "RollMarkingId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_StoreLocationId",
                table: "InwardGatePassDetails",
                column: "StoreLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_YarnManufacturerId",
                table: "InwardGatePassDetails",
                column: "YarnManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_YarnQualityId",
                table: "InwardGatePassDetails",
                column: "YarnQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePassDetails_YarnTypeId",
                table: "InwardGatePassDetails",
                column: "YarnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePasses_ActivityTypeId",
                table: "InwardGatePasses",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePasses_BranchId",
                table: "InwardGatePasses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePasses_BuyerId",
                table: "InwardGatePasses",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePasses_GateTrId",
                table: "InwardGatePasses",
                column: "GateTrId");

            migrationBuilder.CreateIndex(
                name: "IX_InwardGatePasses_PartyId",
                table: "InwardGatePasses",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteDetails_IssueNoteId",
                table: "IssueNoteDetails",
                column: "IssueNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteDetails_PPCPlanningId",
                table: "IssueNoteDetails",
                column: "PPCPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteDetails_ReprocessId",
                table: "IssueNoteDetails",
                column: "ReprocessId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteDetails_StoreLocationId",
                table: "IssueNoteDetails",
                column: "StoreLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNotes_BranchId",
                table: "IssueNotes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNotes_UserId",
                table: "IssueNotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LotMarkings_PPCPlanningId",
                table: "LotMarkings",
                column: "PPCPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineTypeId",
                table: "Machines",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePasses_ActivityTypeId",
                table: "OutwardGatePasses",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePasses_BranchId",
                table: "OutwardGatePasses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePasses_BuyerId",
                table: "OutwardGatePasses",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePasses_FabricTypeId",
                table: "OutwardGatePasses",
                column: "FabricTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePasses_PartyId",
                table: "OutwardGatePasses",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePasses_YarnTypeId",
                table: "OutwardGatePasses",
                column: "YarnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_BuyerId",
                table: "OutwardGatePassesDetails",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_FabricTypesId",
                table: "OutwardGatePassesDetails",
                column: "FabricTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_FactoryPoDetailId",
                table: "OutwardGatePassesDetails",
                column: "FactoryPoDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_InwardGatePassDetailId",
                table: "OutwardGatePassesDetails",
                column: "InwardGatePassDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_OutwardGatePassId",
                table: "OutwardGatePassesDetails",
                column: "OutwardGatePassId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_PPCPlanningId",
                table: "OutwardGatePassesDetails",
                column: "PPCPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_ReprocessId",
                table: "OutwardGatePassesDetails",
                column: "ReprocessId");

            migrationBuilder.CreateIndex(
                name: "IX_OutwardGatePassesDetails_YarnTypeId",
                table: "OutwardGatePassesDetails",
                column: "YarnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_BranchId",
                table: "PPCPlannings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_BuyerColorId",
                table: "PPCPlannings",
                column: "BuyerColorId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_BuyerId",
                table: "PPCPlannings",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_FabricQualityId",
                table: "PPCPlannings",
                column: "FabricQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_FabricTypeId",
                table: "PPCPlannings",
                column: "FabricTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_FabricTypesId",
                table: "PPCPlannings",
                column: "FabricTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_InwardGatePassDetailId",
                table: "PPCPlannings",
                column: "InwardGatePassDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_KnitingPartyId",
                table: "PPCPlannings",
                column: "KnitingPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_PartyId",
                table: "PPCPlannings",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_ProductionTypeId",
                table: "PPCPlannings",
                column: "ProductionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_PurchaseOrderId",
                table: "PPCPlannings",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_UserId",
                table: "PPCPlannings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_YarnManufacturerId",
                table: "PPCPlannings",
                column: "YarnManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_YarnQualityId",
                table: "PPCPlannings",
                column: "YarnQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_PPCPlannings_YarnTypeId",
                table: "PPCPlannings",
                column: "YarnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_BranchId",
                table: "PurchaseOrders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_BuyerColorId",
                table: "PurchaseOrders",
                column: "BuyerColorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_BuyerId",
                table: "PurchaseOrders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_FabricQualityId",
                table: "PurchaseOrders",
                column: "FabricQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_FabricTypeId",
                table: "PurchaseOrders",
                column: "FabricTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PartyId",
                table: "PurchaseOrders",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PartyId1",
                table: "PurchaseOrders",
                column: "PartyId1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SeasonId",
                table: "PurchaseOrders",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_UserId",
                table: "PurchaseOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_YarnQualityId",
                table: "PurchaseOrders",
                column: "YarnQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_YarnTypeId",
                table: "PurchaseOrders",
                column: "YarnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_ChemicalId",
                table: "RecipeDetails",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_DyeId",
                table: "RecipeDetails",
                column: "DyeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_RecipeId",
                table: "RecipeDetails",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_RecipeStepId",
                table: "RecipeDetails",
                column: "RecipeStepId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFormatDetails_ChemicalId",
                table: "RecipeFormatDetails",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFormatDetails_DyeId",
                table: "RecipeFormatDetails",
                column: "DyeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFormatDetails_RecipeFormatHeaderId",
                table: "RecipeFormatDetails",
                column: "RecipeFormatHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFormatDetails_RecipeStepId",
                table: "RecipeFormatDetails",
                column: "RecipeStepId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFormatHeaders_ProcessTypeId",
                table: "RecipeFormatHeaders",
                column: "ProcessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeLPs_LPSId",
                table: "RecipeLPs",
                column: "LPSId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeLPs_RecipeId",
                table: "RecipeLPs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeLPs_ReprocessId",
                table: "RecipeLPs",
                column: "ReprocessId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_BranchId",
                table: "Recipes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_BuyerColorId",
                table: "Recipes",
                column: "BuyerColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MachineTypeId",
                table: "Recipes",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_PartyId",
                table: "Recipes",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeFormatId",
                table: "Recipes",
                column: "RecipeFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_BranchId",
                table: "ReportFilters",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_BuyerId",
                table: "ReportFilters",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_FabricQualityId",
                table: "ReportFilters",
                column: "FabricQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_FabricTypeId",
                table: "ReportFilters",
                column: "FabricTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_UserId",
                table: "ReportFilters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_YarnPartyId",
                table: "ReportFilters",
                column: "YarnPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_YarnQualityId",
                table: "ReportFilters",
                column: "YarnQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_YarnTypeId",
                table: "ReportFilters",
                column: "YarnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reprocesses_InwardGatePassDetailId",
                table: "Reprocesses",
                column: "InwardGatePassDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Reprocesses_PPCPlanningId",
                table: "Reprocesses",
                column: "PPCPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnNoteDetails_PPCPlanningId",
                table: "ReturnNoteDetails",
                column: "PPCPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnNoteDetails_ReprocessId",
                table: "ReturnNoteDetails",
                column: "ReprocessId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnNoteDetails_ReturnNoteId",
                table: "ReturnNoteDetails",
                column: "ReturnNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnNoteDetails_StoreLocationId",
                table: "ReturnNoteDetails",
                column: "StoreLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnNotes_BranchId",
                table: "ReturnNotes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnNotes_BuyerId",
                table: "ReturnNotes",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnNotes_UserId",
                table: "ReturnNotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RollMarkingDetails_RollMarkingId",
                table: "RollMarkingDetails",
                column: "RollMarkingId");

            migrationBuilder.CreateIndex(
                name: "IX_RollMarkings_PPCPlanningId",
                table: "RollMarkings",
                column: "PPCPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProcessActivityTypes_ActivityTypeId",
                table: "SubProcessActivityTypes",
                column: "ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserRoles_Accounts_UserId",
                table: "AccountUserRoles",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserRoles_Accounts_UserId1",
                table: "AccountUserRoles",
                column: "UserId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserClaims_Accounts_UserId",
                table: "AccountUserClaims",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserClaims_Accounts_UserId1",
                table: "AccountUserClaims",
                column: "UserId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserLogins_Accounts_UserId",
                table: "AccountUserLogins",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserLogins_Accounts_UserId1",
                table: "AccountUserLogins",
                column: "UserId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserTokens_Accounts_UserId",
                table: "AccountUserTokens",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserTokens_Accounts_UserId1",
                table: "AccountUserTokens",
                column: "UserId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueNotes_Accounts_UserId",
                table: "IssueNotes",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PPCPlannings_Accounts_UserId",
                table: "PPCPlannings",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PPCPlannings_InwardGatePassDetails_InwardGatePassDetailId",
                table: "PPCPlannings",
                column: "InwardGatePassDetailId",
                principalTable: "InwardGatePassDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PPCPlannings_PurchaseOrders_PurchaseOrderId",
                table: "PPCPlannings",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Accounts_UserId",
                table: "PurchaseOrders",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Accounts_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFilters_Accounts_UserId",
                table: "ReportFilters",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InwardGatePassDetails_InwardGatePasses_InwardGatePassId",
                table: "InwardGatePassDetails",
                column: "InwardGatePassId",
                principalTable: "InwardGatePasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GateTrs_DyeChemicalTrs_GetDyeChemicalTrId",
                table: "GateTrs",
                column: "GetDyeChemicalTrId",
                principalTable: "DyeChemicalTrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DyeChemicalTrDetails_GateTrDetails_GateTrDetailId",
                table: "DyeChemicalTrDetails",
                column: "GateTrDetailId",
                principalTable: "GateTrDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Branches_BranchId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_GateTrs_Branches_BranchId",
                table: "GateTrs");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePasses_Branches_BranchId",
                table: "InwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_OutwardGatePasses_Branches_BranchId",
                table: "OutwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_Branches_BranchId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Branches_BranchId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportFilters_Branches_BranchId",
                table: "ReportFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_ReportFilters_ReportFilterId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_Accounts_UserId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Accounts_UserId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Accounts_UserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyerColors_Buyers_BuyerId",
                table: "BuyerColors");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryPo_Buyers_BuyerId",
                table: "FactoryPo");

            migrationBuilder.DropForeignKey(
                name: "FK_GateTrs_Buyers_BuyerId",
                table: "GateTrs");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePasses_Buyers_BuyerId",
                table: "InwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_OutwardGatePasses_Buyers_BuyerId",
                table: "OutwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_OutwardGatePassesDetails_Buyers_BuyerId",
                table: "OutwardGatePassesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_Buyers_BuyerId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Buyers_BuyerId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DyeChemicalTrs_Parties_PartyId",
                table: "DyeChemicalTrs");

            migrationBuilder.DropForeignKey(
                name: "FK_GateTrs_Parties_PartyId",
                table: "GateTrs");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePasses_Parties_PartyId",
                table: "InwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_OutwardGatePasses_Parties_PartyId",
                table: "OutwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_Parties_PartyId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Parties_PartyId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Parties_PartyId1",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Parties_PartyId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryPo_BuyerColors_BuyerColorId",
                table: "FactoryPo");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryPoDetail_BuyerColors_BuyerColorId",
                table: "FactoryPoDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_BuyerColors_BuyerColorId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_BuyerColors_BuyerColorId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_BuyerColors_BuyerColorId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_GateTrDetails_YarnTypes_YarnTypeId",
                table: "GateTrDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_YarnTypes_YarnTypeId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OutwardGatePasses_YarnTypes_YarnTypeId",
                table: "OutwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_OutwardGatePassesDetails_YarnTypes_YarnTypeId",
                table: "OutwardGatePassesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_YarnTypes_YarnTypeId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_YarnTypes_YarnTypeId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DyeChemicalTrDetails_Chemicals_ChemicalId",
                table: "DyeChemicalTrDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GateTrDetails_Chemicals_ChemicalId",
                table: "GateTrDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_Chemicals_ChemicalId",
                table: "RecipeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DyeChemicalTrDetails_DyeChemicalTrs_DyeChemicalTrId",
                table: "DyeChemicalTrDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GateTrs_DyeChemicalTrs_GetDyeChemicalTrId",
                table: "GateTrs");

            migrationBuilder.DropForeignKey(
                name: "FK_DyeChemicalTrDetails_Dyes_DyeId",
                table: "DyeChemicalTrDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GateTrDetails_Dyes_DyeId",
                table: "GateTrDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_Dyes_DyeId",
                table: "RecipeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DyeChemicalTrDetails_GateTrDetails_GateTrDetailId",
                table: "DyeChemicalTrDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePasses_GateTrs_GateTrId",
                table: "InwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_FabricQualityes_FabricQualityId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_FabricQualityes_FabricQualityId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_FabricQualityes_FabricQualityId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_FabricTypes_FabricTypesId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_FabricTypes_FabricTypeId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_FabricTypes_FabricTypesId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_FabricTypes_FabricTypeId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_OrderActivityTypes_ActivityTypeId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePasses_OrderActivityTypes_ActivityTypeId",
                table: "InwardGatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_BagMarkings_BagMarkingId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_ConeMarkings_ConeMarkingId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_InwardGatePasses_InwardGatePassId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_knittingPartyes_KnitingPartyId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PPCPlannings_knittingPartyes_KnitingPartyId",
                table: "PPCPlannings");

            migrationBuilder.DropForeignKey(
                name: "FK_InwardGatePassDetails_RollMarkings_RollMarkingId",
                table: "InwardGatePassDetails");

            migrationBuilder.DropTable(
                name: "AccountRoleClaims");

            migrationBuilder.DropTable(
                name: "AccountUserClaims");

            migrationBuilder.DropTable(
                name: "AccountUserLogins");

            migrationBuilder.DropTable(
                name: "AccountUserRoles");

            migrationBuilder.DropTable(
                name: "AccountUserTokens");

            migrationBuilder.DropTable(
                name: "ContractDetails");

            migrationBuilder.DropTable(
                name: "Costings");

            migrationBuilder.DropTable(
                name: "DefectAnalysis");

            migrationBuilder.DropTable(
                name: "IssueNoteDetails");

            migrationBuilder.DropTable(
                name: "LotMarkings");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "RecipeFormatDetails");

            migrationBuilder.DropTable(
                name: "RecipeLPs");

            migrationBuilder.DropTable(
                name: "ReturnNoteDetails");

            migrationBuilder.DropTable(
                name: "RollMarkingDetails");

            migrationBuilder.DropTable(
                name: "Sticker");

            migrationBuilder.DropTable(
                name: "SubProcessActivityTypes");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "AnalysisType");

            migrationBuilder.DropTable(
                name: "Defect");

            migrationBuilder.DropTable(
                name: "IssueNotes");

            migrationBuilder.DropTable(
                name: "ReturnNotes");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "ReportFilters");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "BuyerColors");

            migrationBuilder.DropTable(
                name: "YarnTypes");

            migrationBuilder.DropTable(
                name: "Chemicals");

            migrationBuilder.DropTable(
                name: "DyeChemicalTrs");

            migrationBuilder.DropTable(
                name: "Dyes");

            migrationBuilder.DropTable(
                name: "GateTrDetails");

            migrationBuilder.DropTable(
                name: "DyeChemicalTrDetails");

            migrationBuilder.DropTable(
                name: "OutwardGatePassesDetails");

            migrationBuilder.DropTable(
                name: "RecipeDetails");

            migrationBuilder.DropTable(
                name: "FactoryPoDetail");

            migrationBuilder.DropTable(
                name: "Reprocesses");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeSteps");

            migrationBuilder.DropTable(
                name: "FactoryPo");

            migrationBuilder.DropTable(
                name: "MachineTypes");

            migrationBuilder.DropTable(
                name: "RecipeFormatHeaders");

            migrationBuilder.DropTable(
                name: "ProcessTypees");

            migrationBuilder.DropTable(
                name: "GateTrs");

            migrationBuilder.DropTable(
                name: "GateActivityType");

            migrationBuilder.DropTable(
                name: "GatePassType");

            migrationBuilder.DropTable(
                name: "OutwardGatePasses");

            migrationBuilder.DropTable(
                name: "FabricQualityes");

            migrationBuilder.DropTable(
                name: "FabricTypes");

            migrationBuilder.DropTable(
                name: "OrderActivityTypes");

            migrationBuilder.DropTable(
                name: "BagMarkings");

            migrationBuilder.DropTable(
                name: "ConeMarkings");

            migrationBuilder.DropTable(
                name: "InwardGatePasses");

            migrationBuilder.DropTable(
                name: "knittingPartyes");

            migrationBuilder.DropTable(
                name: "RollMarkings");

            migrationBuilder.DropTable(
                name: "PPCPlannings");

            migrationBuilder.DropTable(
                name: "InwardGatePassDetails");

            migrationBuilder.DropTable(
                name: "ProductionType");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "StoreLocations");

            migrationBuilder.DropTable(
                name: "YarnManufacturers");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "YarnQualities");
        }
    }
}
