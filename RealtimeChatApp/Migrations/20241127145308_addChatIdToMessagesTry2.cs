using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeChatApp.Migrations
{
    /// <inheritdoc />
    public partial class addChatIdToMessagesTry2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ensure the Id column in the Chats table is a Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            // Add the ChatId column to the Messages table
            migrationBuilder.AddColumn<Guid>(
                name: "ChatId",
                table: "Messages",
                nullable: false,
                defaultValue: Guid.NewGuid());

            // Create an index on ChatId for performance
            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            // Add the foreign key constraint between Messages and Chats
            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            // Drop the index on ChatId
            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatId",
                table: "Messages");

            // Drop the ChatId column from Messages
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Messages");

            // Optionally remove the primary key constraint (if needed)
            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");
        }
    }
}
