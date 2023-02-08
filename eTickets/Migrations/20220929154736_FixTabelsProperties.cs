using Microsoft.EntityFrameworkCore.Migrations;

namespace eTickets.Migrations
{
    public partial class FixTabelsProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "Producers",
                newName: "ProfilePictureURL");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Movies",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Movies",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "Actors",
                newName: "ProfilePictureURL");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "Producers",
                newName: "ProfilePictureUrl");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Movies",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Movies",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "Actors",
                newName: "ProfilePictureUrl");
        }
    }
}
