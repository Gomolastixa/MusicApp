using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MusicRecord",
                columns: new[] { "Id", "Artist", "Genre", "Name", "Year" },
                values: new object[,]
                {
                    { 8, "Tragedy", "Hardcore Punk", "Tragedy", 2000 },
                    { 9, "Tragedy", "Hardcore Punk", "Nerve Damage", 2006 },
                    { 10, "Hello Skinny", "Experimental", "Hello Skinny", 2012 }
                });

            migrationBuilder.InsertData(
                table: "Musicians",
                columns: new[] { "Id", "FullName", "Instrument" },
                values: new object[,]
                {
                    { 6, "Todd Burdette", "Guitar, Vocals" },
                    { 7, "Paul Burdette", "Drums" },
                    { 8, "Billy Davis", "Bass" },
                    { 9, "Yannick Lorrain", "Guitar" }
                });

            migrationBuilder.InsertData(
                table: "RecordMembers",
                columns: new[] { "Id", "MusicRecordId", "MusicianId" },
                values: new object[,]
                {
                    { 28, 8, 6 },
                    { 29, 8, 7 },
                    { 30, 8, 8 },
                    { 31, 8, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MusicRecord",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MusicRecord",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RecordMembers",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RecordMembers",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RecordMembers",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RecordMembers",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MusicRecord",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
