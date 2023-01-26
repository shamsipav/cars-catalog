using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class minorfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestInfos_Response_ResponseId",
                table: "RequestInfos");

            migrationBuilder.RenameColumn(
                name: "tBody",
                table: "Response",
                newName: "Body");

            migrationBuilder.AlterColumn<int>(
                name: "ResponseId",
                table: "RequestInfos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestInfos_Response_ResponseId",
                table: "RequestInfos",
                column: "ResponseId",
                principalTable: "Response",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestInfos_Response_ResponseId",
                table: "RequestInfos");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Response",
                newName: "tBody");

            migrationBuilder.AlterColumn<int>(
                name: "ResponseId",
                table: "RequestInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestInfos_Response_ResponseId",
                table: "RequestInfos",
                column: "ResponseId",
                principalTable: "Response",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
