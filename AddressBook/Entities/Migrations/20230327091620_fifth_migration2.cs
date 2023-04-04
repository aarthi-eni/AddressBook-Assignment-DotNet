using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class fifth_migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AddressBooks_AddressBooksId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AddressBooks_AddressBooksId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_AddressBooks_AddressBooksId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_AddressBooks_AddressBooksId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_AddressBooksId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Emails_AddressBooksId",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AddressBooksId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AddressBooksId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressBooksId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "AddressBooksId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "AddressBooksId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AddressBooksId",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressBooksId",
                table: "Phones",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressBooksId",
                table: "Emails",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressBooksId",
                table: "Assets",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressBooksId",
                table: "Addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_AddressBooksId",
                table: "Phones",
                column: "AddressBooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_AddressBooksId",
                table: "Emails",
                column: "AddressBooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AddressBooksId",
                table: "Assets",
                column: "AddressBooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressBooksId",
                table: "Addresses",
                column: "AddressBooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AddressBooks_AddressBooksId",
                table: "Addresses",
                column: "AddressBooksId",
                principalTable: "AddressBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AddressBooks_AddressBooksId",
                table: "Assets",
                column: "AddressBooksId",
                principalTable: "AddressBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_AddressBooks_AddressBooksId",
                table: "Emails",
                column: "AddressBooksId",
                principalTable: "AddressBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_AddressBooks_AddressBooksId",
                table: "Phones",
                column: "AddressBooksId",
                principalTable: "AddressBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
