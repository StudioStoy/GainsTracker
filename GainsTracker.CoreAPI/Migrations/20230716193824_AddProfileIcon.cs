using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GainsTracker.CoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "display_name",
                table: "gains_accounts");

            migrationBuilder.RenameColumn(
                name: "picture_url",
                table: "user_profiles",
                newName: "icon_id");

            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "user_profiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "profile_icons",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_profile_id = table.Column<string>(type: "text", nullable: false),
                    picture_color = table.Column<int>(type: "integer", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profile_icons", x => x.id);
                });
            
            //
            // migrationBuilder.CreateIndex(
            //     name: "ix_user_profiles_icon_id",
            //     table: "user_profiles",
            //     column: "icon_id",
            //     unique: true);
            //
            // migrationBuilder.AddForeignKey(
            //     name: "fk_user_profiles_profile_icons_icon_id",
            //     table: "user_profiles",
            //     column: "icon_id",
            //     principalTable: "profile_icons",
            //     principalColumn: "id",
            //     onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_profiles_profile_icons_icon_id1",
                table: "user_profiles");

            migrationBuilder.DropIndex(
                name: "ix_user_profiles_icon_id",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "display_name",
                table: "user_profiles");

            migrationBuilder.RenameColumn(
                name: "icon_id",
                table: "user_profiles",
                newName: "picture_url");

            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "gains_accounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
