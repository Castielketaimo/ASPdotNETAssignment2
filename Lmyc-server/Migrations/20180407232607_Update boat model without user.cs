using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lmyc_server.Migrations
{
    public partial class Updateboatmodelwithoutuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boat_AspNetUsers_CreatedBy",
                table: "Boat");

            migrationBuilder.DropIndex(
                name: "IX_Boat_CreatedBy",
                table: "Boat");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Boat",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Boat",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boat_CreatedBy",
                table: "Boat",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Boat_AspNetUsers_CreatedBy",
                table: "Boat",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
