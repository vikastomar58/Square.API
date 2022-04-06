using Microsoft.EntityFrameworkCore.Migrations;

namespace Squares.API.DataLayer.Migrations
{
    public partial class PointTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Points",
                columns: table => new
                {
                    Point_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Points", x => x.Point_Id);
                    table.ForeignKey(
                        name: "FK_tbl_Points_tbl_UserDetails_User_Id",
                        column: x => x.User_Id,
                        principalTable: "tbl_UserDetails",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Points_User_Id",
                table: "tbl_Points",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Points");
        }
    }
}
