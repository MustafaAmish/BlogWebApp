using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class AddToPostCategorys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategorys_Categories_CategoryId",
                table: "PostCategorys");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategorys_Posts_PostId",
                table: "PostCategorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategorys",
                table: "PostCategorys");

            migrationBuilder.RenameTable(
                name: "PostCategorys",
                newName: "PostCategoryses");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategorys_PostId",
                table: "PostCategoryses",
                newName: "IX_PostCategoryses_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategoryses",
                table: "PostCategoryses",
                columns: new[] { "CategoryId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategoryses_Categories_CategoryId",
                table: "PostCategoryses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategoryses_Posts_PostId",
                table: "PostCategoryses",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategoryses_Categories_CategoryId",
                table: "PostCategoryses");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategoryses_Posts_PostId",
                table: "PostCategoryses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategoryses",
                table: "PostCategoryses");

            migrationBuilder.RenameTable(
                name: "PostCategoryses",
                newName: "PostCategorys");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategoryses_PostId",
                table: "PostCategorys",
                newName: "IX_PostCategorys_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategorys",
                table: "PostCategorys",
                columns: new[] { "CategoryId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategorys_Categories_CategoryId",
                table: "PostCategorys",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategorys_Posts_PostId",
                table: "PostCategorys",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
