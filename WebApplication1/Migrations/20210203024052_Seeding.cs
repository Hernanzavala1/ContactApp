using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactApp.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "personID", "State", "Street", "UserId", "city", "postalCode" },
                values: new object[,]
                {
                    { 1, "ny", "1029 jericho", null, "smithtown", "11787" },
                    { 2, "ny", "1029 jericho", null, "smithtown", "11787" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "firstName", "lastName" },
                values: new object[,]
                {
                    { 1, "hernan", "zavala" },
                    { 2, "ryan", "burk" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "personID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "personID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
