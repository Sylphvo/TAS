using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAS.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    DealerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.DealerId);
                });

            migrationBuilder.CreateTable(
                name: "USER_ACCOUNT",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HashAlgo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashIter = table.Column<int>(type: "int", nullable: false),
                    FailedAccessCount = table.Column<int>(type: "int", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TwoFactorSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(450)", nullable: true, computedColumnSql: "UPPER([Email])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ACCOUNT", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Gardens",
                columns: table => new
                {
                    GardenId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerPhone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    OwnerIdNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AreaHa = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    TreesCount = table.Column<int>(type: "int", nullable: true),
                    PlantedYear = table.Column<short>(type: "smallint", nullable: true),
                    TapStatus = table.Column<byte>(type: "tinyint", nullable: true),
                    AvgLatexKgDay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: true),
                    IrrigationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gardens", x => x.GardenId);
                    table.ForeignKey(
                        name: "FK_Gardens_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "DealerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_Code",
                table: "Dealers",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_Name",
                table: "Dealers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_Province_District",
                table: "Dealers",
                columns: new[] { "Province", "District" });

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_DealerId",
                table: "Gardens",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_DealerId_Code",
                table: "Gardens",
                columns: new[] { "DealerId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_OwnerPhone",
                table: "Gardens",
                column: "OwnerPhone");

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_Province_District",
                table: "Gardens",
                columns: new[] { "Province", "District" });

            migrationBuilder.CreateIndex(
                name: "IX_USER_ACCOUNT_NormalizedEmail",
                table: "USER_ACCOUNT",
                column: "NormalizedEmail",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gardens");

            migrationBuilder.DropTable(
                name: "USER_ACCOUNT");

            migrationBuilder.DropTable(
                name: "Dealers");
        }
    }
}
