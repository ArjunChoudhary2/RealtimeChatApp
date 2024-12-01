using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeChatApp.Migrations
{
    /// <inheritdoc />
    public partial class SenderRecevierToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add new UUID columns
            migrationBuilder.AddColumn<Guid>(
                name: "sender_id_new",
                table: "Messages",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddColumn<Guid>(
                name: "receiver_id_new",
                table: "Messages",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            // Update the new columns with converted values from the existing columns
            migrationBuilder.Sql(
                @"
        UPDATE ""Messages""
        SET sender_id_new = md5(sender_id::text || 'sender_salt')::uuid,
            receiver_id_new = md5(recevier_id::text || 'receiver_salt')::uuid
        ");

            // Drop old columns
            migrationBuilder.DropColumn(name: "sender_id", table: "Messages");
            migrationBuilder.DropColumn(name: "recevier_id", table: "Messages");

            // Rename new columns to match old names
            migrationBuilder.RenameColumn(
                name: "sender_id_new",
                table: "Messages",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "receiver_id_new",
                table: "Messages",
                newName: "recevier_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add back old integer columns
            migrationBuilder.AddColumn<int>(
                name: "sender_id",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "receiver_id",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Step 2: Remove the new UUID columns
            migrationBuilder.DropColumn(name: "sender_id", table: "Messages");
            migrationBuilder.DropColumn(name: "receiver_id", table: "Messages");
        }
    }
}
