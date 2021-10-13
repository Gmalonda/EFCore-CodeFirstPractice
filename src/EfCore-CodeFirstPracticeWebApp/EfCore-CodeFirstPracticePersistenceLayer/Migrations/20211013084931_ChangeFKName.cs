using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCore_CodeFirstPracticePersistenceLayer.Migrations
{
    public partial class ChangeFKName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CustomerWithOrder_CustomerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "FK_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_FK_CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerWithOrder_FK_CustomerID",
                table: "Orders",
                column: "FK_CustomerID",
                principalTable: "CustomerWithOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CustomerWithOrder_FK_CustomerID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "FK_CustomerID",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_FK_CustomerID",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerWithOrder_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "CustomerWithOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
