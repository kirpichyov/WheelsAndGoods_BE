using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WheelsAndGoods.DataAccess.Migrations.Migrations
{
    public partial class AddUserAvatarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "avatar_blob_name",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "avatar_url",
                table: "users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar_blob_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "avatar_url",
                table: "users");
        }
    }
}
