using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioCabs.Migrations
{
    /// <inheritdoc />
    public partial class addColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyRegistrations_Registrations_RegistrationId",
                table: "CompanyRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_CompanyRegistrations_RegistrationId",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "CompanyRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CompanyRegistrations",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "CompanyRegistrations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "CompanyRegistrations",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CompanyRegistrations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "CompanyRegistrations",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "CompanyRegistrations",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "CompanyRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "CompanyRegistrations",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "CompanyRegistrations");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "CompanyRegistrations");

            migrationBuilder.AddColumn<int>(
                name: "RegistrationId",
                table: "CompanyRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegistrations_RegistrationId",
                table: "CompanyRegistrations",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyRegistrations_Registrations_RegistrationId",
                table: "CompanyRegistrations",
                column: "RegistrationId",
                principalTable: "Registrations",
                principalColumn: "RegistrationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
