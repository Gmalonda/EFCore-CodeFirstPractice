using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCore_CodeFirstPracticePersistenceLayer.Migrations
{
    public partial class ChangeTableAndFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "CustomerWithOrder");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "CustomerWithOrder",
                newName: "CustomerFirstName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerWithOrder",
                table: "CustomerWithOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerWithOrder_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "CustomerWithOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CustomerWithOrder_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerWithOrder",
                table: "CustomerWithOrder");

            migrationBuilder.RenameTable(
                name: "CustomerWithOrder",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerFirstName",
                table: "Customers",
                newName: "FirstName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
