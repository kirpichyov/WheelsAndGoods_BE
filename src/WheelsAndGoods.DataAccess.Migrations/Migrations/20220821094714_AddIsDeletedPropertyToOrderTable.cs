using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WheelsAndGoods.DataAccess.Migrations.Migrations
{
    public partial class AddIsDeletedPropertyToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "orders");
        }
    }
}
