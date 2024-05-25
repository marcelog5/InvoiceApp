using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentInEntityBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_contracts_ContractId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contracts",
                table: "contracts");

            migrationBuilder.RenameTable(
                name: "contracts",
                newName: "Contracts");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Payments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Invoices",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Contracts",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contracts_ContractId",
                table: "Payments",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contracts_ContractId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "contracts");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Payments",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Invoices",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "contracts",
                newName: "DateTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contracts",
                table: "contracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_contracts_ContractId",
                table: "Payments",
                column: "ContractId",
                principalTable: "contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
