using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RubberAgent",
                columns: table => new
                {
                    AgentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AgentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubberAgent", x => x.AgentId);
                    table.UniqueConstraint("AK_RubberAgent_AgentCode", x => x.AgentCode);
                });

            migrationBuilder.CreateTable(
                name: "RubberIntake",
                columns: table => new
                {
                    IntakeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FarmerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RubberKg = table.Column<decimal>(type: "decimal(12,3)", precision: 12, scale: 3, nullable: true),
                    TSCPercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    DRCPercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    FinishedProductKg = table.Column<decimal>(type: "decimal(12,3)", precision: 12, scale: 3, nullable: true),
                    CentrifugeProductKg = table.Column<decimal>(type: "decimal(12,3)", precision: 12, scale: 3, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisterPerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatePerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubberIntake", x => x.IntakeId);
                });

            migrationBuilder.CreateTable(
                name: "RubberOrderSummary",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AgentId = table.Column<long>(type: "bigint", nullable: false),
                    AgentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FarmId = table.Column<long>(type: "bigint", nullable: false),
                    FarmCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FarmerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IntakeId = table.Column<long>(type: "bigint", nullable: false),
                    TotalWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubberOrderSummary", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "USER_ACCOUNT",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogInUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogOutUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ACCOUNT", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AcceptTerms = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false),
                    LogIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogOut = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RubberFarm",
                columns: table => new
                {
                    FarmId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AgentCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FarmerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FarmerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FarmerAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FarmerMap = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Certificates = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalAreaHa = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: true),
                    RubberAreaHa = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: true),
                    TotalExploit = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PolygonMap = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubberFarm", x => x.FarmId);
                    table.ForeignKey(
                        name: "FK_RubberFarm_RubberAgent_AgentCode",
                        column: x => x.AgentCode,
                        principalTable: "RubberAgent",
                        principalColumn: "AgentCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RubberPallets",
                columns: table => new
                {
                    PalletId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    PalletCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PalletNo = table.Column<int>(type: "int", nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(12,3)", precision: 12, scale: 3, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubberPallets", x => x.PalletId);
                    table.ForeignKey(
                        name: "FK_RubberPallets_RubberOrderSummary_OrderId",
                        column: x => x.OrderId,
                        principalTable: "RubberOrderSummary",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_CLAIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_CLAIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_CLAIM_USER_ACCOUNT_UserId",
                        column: x => x.UserId,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_LOGIN",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_LOGIN", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_USER_LOGIN_USER_ACCOUNT_UserId",
                        column: x => x.UserId,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_TOKEN",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_TOKEN", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_USER_TOKEN_USER_ACCOUNT_UserId",
                        column: x => x.UserId,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_CLAIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_CLAIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ROLE_CLAIM_USER_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "USER_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_IN_ROLE",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_IN_ROLE", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_USER_IN_ROLE_USER_ACCOUNT_UserId",
                        column: x => x.UserId,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_IN_ROLE_USER_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "USER_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_CLAIM_RoleId",
                table: "ROLE_CLAIM",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RubberAgent_AgentCode",
                table: "RubberAgent",
                column: "AgentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RubberAgent_TaxCode",
                table: "RubberAgent",
                column: "TaxCode",
                unique: true,
                filter: "[TaxCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RubberFarm_AgentCode",
                table: "RubberFarm",
                column: "AgentCode");

            migrationBuilder.CreateIndex(
                name: "IX_RubberFarm_FarmCode",
                table: "RubberFarm",
                column: "FarmCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RubberIntake_FarmCode",
                table: "RubberIntake",
                column: "FarmCode");

            migrationBuilder.CreateIndex(
                name: "IX_RubberIntake_FarmCode_RegisterDate",
                table: "RubberIntake",
                columns: new[] { "FarmCode", "RegisterDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RubberPallets_OrderId_PalletNo",
                table: "RubberPallets",
                columns: new[] { "OrderId", "PalletNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "USER_ACCOUNT",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "USER_ACCOUNT",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_CLAIM_UserId",
                table: "USER_CLAIM",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_IN_ROLE_RoleId",
                table: "USER_IN_ROLE",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LOGIN_UserId",
                table: "USER_LOGIN",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "USER_ROLE",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Email",
                table: "UserAccount",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_UserName",
                table: "UserAccount",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ROLE_CLAIM");

            migrationBuilder.DropTable(
                name: "RubberFarm");

            migrationBuilder.DropTable(
                name: "RubberIntake");

            migrationBuilder.DropTable(
                name: "RubberPallets");

            migrationBuilder.DropTable(
                name: "USER_CLAIM");

            migrationBuilder.DropTable(
                name: "USER_IN_ROLE");

            migrationBuilder.DropTable(
                name: "USER_LOGIN");

            migrationBuilder.DropTable(
                name: "USER_TOKEN");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "RubberAgent");

            migrationBuilder.DropTable(
                name: "RubberOrderSummary");

            migrationBuilder.DropTable(
                name: "USER_ROLE");

            migrationBuilder.DropTable(
                name: "USER_ACCOUNT");
        }
    }
}
