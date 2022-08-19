using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WheelsAndGoods.DataAccess.Migrations.Migrations
{
    public partial class AddCreatedAtUtcColumnToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_refresh_tokens_users_user_temp_id",
                table: "refresh_tokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at_utc",
                table: "orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "fk_refresh_tokens_users_user_temp_id1",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_refresh_tokens_users_user_temp_id1",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "created_at_utc",
                table: "orders");

            migrationBuilder.AddForeignKey(
                name: "fk_refresh_tokens_users_user_temp_id",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
