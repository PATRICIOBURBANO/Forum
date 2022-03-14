using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    public partial class correctModelsProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTag_AspNetUsers_UserId1",
                table: "UserTag");

            migrationBuilder.DropIndex(
                name: "IX_UserTag_UserId1",
                table: "UserTag");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserTag");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTag",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTag_TagId",
                table: "UserTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTag_UserId",
                table: "UserTag",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTag_AspNetUsers_UserId",
                table: "UserTag",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTag_Tag_TagId",
                table: "UserTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTag_AspNetUsers_UserId",
                table: "UserTag");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTag_Tag_TagId",
                table: "UserTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_UserTag_TagId",
                table: "UserTag");

            migrationBuilder.DropIndex(
                name: "IX_UserTag_UserId",
                table: "UserTag");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserTag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserTag",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTag_UserId1",
                table: "UserTag",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTag_AspNetUsers_UserId1",
                table: "UserTag",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
