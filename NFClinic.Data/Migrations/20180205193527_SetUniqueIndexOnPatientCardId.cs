using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NFClinic.Data.Migrations
{
    public partial class SetUniqueIndexOnPatientCardId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_CardId",
                table: "Patients",
                column: "CardId",
                unique: true,
                filter: "[CardId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_CardId",
                table: "Patients");
        }
    }
}
