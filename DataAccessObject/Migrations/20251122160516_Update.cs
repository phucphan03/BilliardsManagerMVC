using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObject.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CueSticks_Image_CueStickImageID",
                table: "CueSticks");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_ProductImageID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_CueSticks_Images_CueStickImageID",
                table: "CueSticks",
                column: "CueStickImageID",
                principalTable: "Images",
                principalColumn: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ProductImageID",
                table: "Products",
                column: "ProductImageID",
                principalTable: "Images",
                principalColumn: "ImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CueSticks_Images_CueStickImageID",
                table: "CueSticks");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ProductImageID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_CueSticks_Image_CueStickImageID",
                table: "CueSticks",
                column: "CueStickImageID",
                principalTable: "Image",
                principalColumn: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Image_ProductImageID",
                table: "Products",
                column: "ProductImageID",
                principalTable: "Image",
                principalColumn: "ImageID");
        }
    }
}
