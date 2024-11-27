using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeChatApp.Migrations
{
    public partial class chatIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add temporary columns with UUID type
            migrationBuilder.AddColumn<Guid>(
                name: "TempUser2Id",
                table: "Chats",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TempUser1Id",
                table: "Chats",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TempId",
                table: "Chats",
                type: "uuid",
                nullable: true);

            // Step 2: Copy data from old columns to new temporary columns
            migrationBuilder.Sql(@"
                UPDATE ""Chats""
                SET ""TempUser2Id"" = uuid_generate_v4() -- Use `uuid_generate_v4()` or meaningful mapping logic
            ");

            migrationBuilder.Sql(@"
                UPDATE ""Chats""
                SET ""TempUser1Id"" = uuid_generate_v4() -- Use `uuid_generate_v4()` or meaningful mapping logic
            ");

            migrationBuilder.Sql(@"
                UPDATE ""Chats""
                SET ""TempId"" = uuid_generate_v4() -- Use `uuid_generate_v4()` or meaningful mapping logic
            ");

            // Step 3: Remove old columns
            migrationBuilder.DropColumn(
                name: "user2_id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "user1_id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Chats");

            // Step 4: Rename new temporary columns to original column names
            migrationBuilder.RenameColumn(
                name: "TempUser2Id",
                table: "Chats",
                newName: "user2_id");

            migrationBuilder.RenameColumn(
                name: "TempUser1Id",
                table: "Chats",
                newName: "user1_id");

            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "Chats",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the changes for the Down method

            // Step 1: Add old columns with integer type
            migrationBuilder.AddColumn<int>(
                name: "user2_id",
                table: "Chats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user1_id",
                table: "Chats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Chats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Step 2: Copy data from UUID columns back to old integer columns
            // Note: This will lose original integer values unless stored somewhere else
            migrationBuilder.Sql(@"
                UPDATE ""Chats""
                SET ""user2_id"" = 0; -- Replace with appropriate logic
            ");

            migrationBuilder.Sql(@"
                UPDATE ""Chats""
                SET ""user1_id"" = 0; -- Replace with appropriate logic
            ");

            migrationBuilder.Sql(@"
                UPDATE ""Chats""
                SET ""Id"" = 0; -- Replace with appropriate logic
            ");

            // Step 3: Drop UUID columns
            migrationBuilder.DropColumn(
                name: "user2_id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "user1_id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Chats");
        }
    }
}
