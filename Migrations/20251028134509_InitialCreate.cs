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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FarmCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FarmerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Kg = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    TSCPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DRCPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    FinishedProductKg = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    CentrifugeProductKg = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    IntakeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubberIntake", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                name: "RubberFarm");

            migrationBuilder.DropTable(
                name: "RubberIntake");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "RubberAgent");
        }
    }
}
