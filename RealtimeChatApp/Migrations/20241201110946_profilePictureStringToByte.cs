using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeChatApp.Migrations
{
    public partial class profilePictureStringToByte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Explicitly cast the profile_pic column to bytea
            migrationBuilder.Sql(
                "ALTER TABLE users ALTER COLUMN profile_pic TYPE bytea USING profile_pic::bytea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert profile_pic column to text
            migrationBuilder.Sql(
                "ALTER TABLE users ALTER COLUMN profile_pic TYPE text USING profile_pic::text");
        }
    }
}
