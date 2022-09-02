using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermissionManagerAPI.Migrations
{
    public partial class RemoveRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionTypes_Permissions_PermissionId",
                table: "PermissionTypes");

            migrationBuilder.DropIndex(
                name: "IX_PermissionTypes_PermissionId",
                table: "PermissionTypes");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "PermissionTypes");

            migrationBuilder.AddColumn<int>(
                name: "PermissionTypeId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId",
                principalTable: "PermissionTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionTypeId",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "PermissionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionTypes_PermissionId",
                table: "PermissionTypes",
                column: "PermissionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionTypes_Permissions_PermissionId",
                table: "PermissionTypes",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
