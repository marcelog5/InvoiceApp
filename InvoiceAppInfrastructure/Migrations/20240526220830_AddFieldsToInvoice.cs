using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Invoices");
        }
    }
}
