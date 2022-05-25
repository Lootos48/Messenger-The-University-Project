using Microsoft.EntityFrameworkCore.Migrations;

namespace MessengerServer.Migrations
{
    public partial class AddUniqueconstrainttoChattitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Chats_Title",
                table: "Chats",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chats_Title",
                table: "Chats");
        }
    }
}
