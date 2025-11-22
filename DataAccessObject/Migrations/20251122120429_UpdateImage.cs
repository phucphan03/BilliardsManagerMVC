using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "CueSticks");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductImageID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CueStickImageID",
                table: "CueSticks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CueStickID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductImageID",
                table: "Products",
                column: "ProductImageID");

            migrationBuilder.CreateIndex(
                name: "IX_CueSticks_CueStickImageID",
                table: "CueSticks",
                column: "CueStickImageID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CueSticks_Image_CueStickImageID",
                table: "CueSticks");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_ProductImageID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductImageID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_CueSticks_CueStickImageID",
                table: "CueSticks");

            migrationBuilder.DropColumn(
                name: "ProductImageID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CueStickImageID",
                table: "CueSticks");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "CueSticks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
