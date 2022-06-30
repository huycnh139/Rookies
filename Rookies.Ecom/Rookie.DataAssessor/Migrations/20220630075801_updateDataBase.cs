using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rookie.DataAccessor.Migrations
{
    public partial class updateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImageSize",
                table: "ProductImages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefualt",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImgaeUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44aedb87-3a2e-4ec4-aa46-85e3f332a796"),
                column: "ConcurrencyStamp",
                value: "9e234145-a47b-41c8-8c59-040328464b97");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c8dcb1fd-a46c-4068-b700-54adc575660c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1d21e5e4-b5ba-436e-bf20-163a53ba9e59", "AQAAAAEAACcQAAAAEHkJhVYWDsnaZAGJkNJpNLQgXgqUBdRabjZP1bE8W8SvML8zZbiu4pMT8hFDKXklRg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ImageSize",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "IsDefualt",
                table: "ProductImages");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ImgaeUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44aedb87-3a2e-4ec4-aa46-85e3f332a796"),
                column: "ConcurrencyStamp",
                value: "e6781a8e-e3d8-4580-8424-5781b802877b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c8dcb1fd-a46c-4068-b700-54adc575660c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2ad67c33-3072-4965-a2cf-823b1e8faa17", "AQAAAAEAACcQAAAAEDYedyfgUI2T3EtfC/XP8xhA88oftrrpOc9XwChu/+lME46g+iTy/HQz1LnQRXjmsA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
