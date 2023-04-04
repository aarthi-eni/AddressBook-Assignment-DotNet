using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class fifth_migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset",
                table: "Asset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phone",
                table: "Phone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Email",
                table: "Email");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressBook",
                table: "AddressBook");

            migrationBuilder.RenameTable(
                name: "Asset",
                newName: "asset");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "address");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "user_login");

            migrationBuilder.RenameTable(
                name: "Phone",
                newName: "phone_number");

            migrationBuilder.RenameTable(
                name: "Email",
                newName: "email_address");

            migrationBuilder.RenameTable(
                name: "AddressBook",
                newName: "address_book");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asset",
                table: "asset",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_address",
                table: "address",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_login",
                table: "user_login",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_phone_number",
                table: "phone_number",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_email_address",
                table: "email_address",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_address_book",
                table: "address_book",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_asset",
                table: "asset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_address",
                table: "address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_login",
                table: "user_login");

            migrationBuilder.DropPrimaryKey(
                name: "PK_phone_number",
                table: "phone_number");

            migrationBuilder.DropPrimaryKey(
                name: "PK_email_address",
                table: "email_address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_address_book",
                table: "address_book");

            migrationBuilder.RenameTable(
                name: "asset",
                newName: "Asset");

            migrationBuilder.RenameTable(
                name: "address",
                newName: "Address");

            migrationBuilder.RenameTable(
                name: "user_login",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "phone_number",
                newName: "Phone");

            migrationBuilder.RenameTable(
                name: "email_address",
                newName: "Email");

            migrationBuilder.RenameTable(
                name: "address_book",
                newName: "AddressBook");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset",
                table: "Asset",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phone",
                table: "Phone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Email",
                table: "Email",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressBook",
                table: "AddressBook",
                column: "Id");
        }
    }
}
