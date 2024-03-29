using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GainsTracker.CoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class _02_TimeToSeconds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "1121bbb9-d84a-4785-a82d-cc8524e05ded");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "39e1e8e9-cf52-43cf-b36d-6e236781fdf7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "45af9e1d-918d-4bac-91be-5a3dafd3632c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "46e5fe55-7f52-44b1-b6a7-8c73ccf90fd8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "52838699-5ea6-48ad-a0d8-c04863f8ae69");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "7786cd53-fe22-4759-96bd-6dd54f39e314");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "85c64060-ebc1-4018-adea-851d60cf9ab5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "8c6ddacf-85fc-4592-aa30-b2094e2ede57");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "a2293bab-21af-4a81-845f-24fb512d6570");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "bef5387a-2d5d-4cbd-9e87-e8b4ba85220a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "ca246d77-91c3-4b9b-b309-652aa4b0781b");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "3eb74cb9-bb32-46af-9585-d83c87266290");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "51bd0982-c371-4ff3-8ab3-346559f94512");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "5fbbf621-204c-45fc-b0ad-57d1273e1002");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "72d8301a-5cb4-43e3-b2b4-b39b0cd99e25");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "7be89c4e-7961-405c-8ca1-dd8bee491bbc");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "8aa9b0a7-d13f-4001-9a98-a3042b6e3628");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "9f56933b-82ed-40fa-aecb-e540d9b52d0e");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "e7dcb8f2-d2d1-4c59-9907-4fe7ad00d3b4");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "f95493bf-b281-419c-9ab0-3329de898c70");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "fc20ac78-1171-4498-a2fc-a050c7d8c755");

            migrationBuilder.DeleteData(
                table: "profile_icons",
                keyColumn: "id",
                keyValue: "fe1657d3-3cfb-4cc4-b84c-aaed8c9baefb");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "08a458ec-2d03-4b24-b7df-3e37758ba558");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "324333e7-7b11-49ac-bef1-34d1a1c6a1a2");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "425c6cc9-0ec6-4f26-8181-b5d88dcd7a83");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "5b20ae1d-5d24-46ac-ba22-c17a98b7bea6");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "64a8f4dd-2fc0-4df4-98ce-778f5400a228");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "69b15a32-3435-40a9-9c27-f07757194ca7");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "8c46367d-5a76-49e6-90f9-0a4d340e3b9d");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "a62bda19-021b-40bd-9cf4-25be19aa43d1");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "ce467f81-649d-4137-ba49-94196cc87c28");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "d035ed6f-89dd-4649-8c1c-495faf5ca40e");

            migrationBuilder.DeleteData(
                table: "gains_accounts",
                keyColumn: "id",
                keyValue: "e89d2092-2bb5-450e-a421-3708f8306d0c");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "143eccc3-ff5e-4c46-9d59-c0e411b2410b");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "44bd9c73-fc6e-4749-a0a2-4f8479e33996");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "4866c9a8-4214-400f-a619-b77fd231afef");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "5d8d530c-284f-430e-92a7-f4ec84cf0550");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "63fda16d-8aff-45d5-a241-91ce02a78240");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "7f154b7f-bb4c-4125-88bb-14f358e242d3");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "93701009-7bc3-49a0-9ca3-5b04eb13bd36");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "9a6cce86-4e44-4087-b09a-e66a35fdc128");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "a43419bd-bbee-4c58-9766-4739837354cd");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "b06572a6-8128-4024-bf6f-3cc7e1c456f2");

            migrationBuilder.DeleteData(
                table: "user_profiles",
                keyColumn: "id",
                keyValue: "f8b589bd-b91a-4704-9371-73b94b07319f");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "user_profiles",
                columns: new[] { "id", "description", "display_name", "gains_account_id" },
                values: new object[,]
                {
                    { "143eccc3-ff5e-4c46-9d59-c0e411b2410b", "", "", "64a8f4dd-2fc0-4df4-98ce-778f5400a228" },
                    { "44bd9c73-fc6e-4749-a0a2-4f8479e33996", "", "", "ce467f81-649d-4137-ba49-94196cc87c28" },
                    { "4866c9a8-4214-400f-a619-b77fd231afef", "", "", "425c6cc9-0ec6-4f26-8181-b5d88dcd7a83" },
                    { "5d8d530c-284f-430e-92a7-f4ec84cf0550", "", "", "69b15a32-3435-40a9-9c27-f07757194ca7" },
                    { "63fda16d-8aff-45d5-a241-91ce02a78240", "", "", "e89d2092-2bb5-450e-a421-3708f8306d0c" },
                    { "7f154b7f-bb4c-4125-88bb-14f358e242d3", "", "", "324333e7-7b11-49ac-bef1-34d1a1c6a1a2" },
                    { "93701009-7bc3-49a0-9ca3-5b04eb13bd36", "", "", "a62bda19-021b-40bd-9cf4-25be19aa43d1" },
                    { "9a6cce86-4e44-4087-b09a-e66a35fdc128", "", "", "8c46367d-5a76-49e6-90f9-0a4d340e3b9d" },
                    { "a43419bd-bbee-4c58-9766-4739837354cd", "", "", "5b20ae1d-5d24-46ac-ba22-c17a98b7bea6" },
                    { "b06572a6-8128-4024-bf6f-3cc7e1c456f2", "", "", "d035ed6f-89dd-4649-8c1c-495faf5ca40e" },
                    { "f8b589bd-b91a-4704-9371-73b94b07319f", "", "", "08a458ec-2d03-4b24-b7df-3e37758ba558" }
                });

            migrationBuilder.InsertData(
                table: "gains_accounts",
                columns: new[] { "id", "user_handle", "user_id", "user_profile_id" },
                values: new object[,]
                {
                    { "08a458ec-2d03-4b24-b7df-3e37758ba558", "bino", "ca246d77-91c3-4b9b-b309-652aa4b0781b", "f8b589bd-b91a-4704-9371-73b94b07319f" },
                    { "324333e7-7b11-49ac-bef1-34d1a1c6a1a2", "naoh", "a2293bab-21af-4a81-845f-24fb512d6570", "7f154b7f-bb4c-4125-88bb-14f358e242d3" },
                    { "425c6cc9-0ec6-4f26-8181-b5d88dcd7a83", "dyllo", "1121bbb9-d84a-4785-a82d-cc8524e05ded", "4866c9a8-4214-400f-a619-b77fd231afef" },
                    { "5b20ae1d-5d24-46ac-ba22-c17a98b7bea6", "japser", "8c6ddacf-85fc-4592-aa30-b2094e2ede57", "a43419bd-bbee-4c58-9766-4739837354cd" },
                    { "64a8f4dd-2fc0-4df4-98ce-778f5400a228", "arv", "52838699-5ea6-48ad-a0d8-c04863f8ae69", "143eccc3-ff5e-4c46-9d59-c0e411b2410b" },
                    { "69b15a32-3435-40a9-9c27-f07757194ca7", "soep", "85c64060-ebc1-4018-adea-851d60cf9ab5", "5d8d530c-284f-430e-92a7-f4ec84cf0550" },
                    { "8c46367d-5a76-49e6-90f9-0a4d340e3b9d", "jordt", "7786cd53-fe22-4759-96bd-6dd54f39e314", "9a6cce86-4e44-4087-b09a-e66a35fdc128" },
                    { "a62bda19-021b-40bd-9cf4-25be19aa43d1", "joyo", "bef5387a-2d5d-4cbd-9e87-e8b4ba85220a", "93701009-7bc3-49a0-9ca3-5b04eb13bd36" },
                    { "ce467f81-649d-4137-ba49-94196cc87c28", "sanda", "45af9e1d-918d-4bac-91be-5a3dafd3632c", "44bd9c73-fc6e-4749-a0a2-4f8479e33996" },
                    { "d035ed6f-89dd-4649-8c1c-495faf5ca40e", "eef", "46e5fe55-7f52-44b1-b6a7-8c73ccf90fd8", "b06572a6-8128-4024-bf6f-3cc7e1c456f2" },
                    { "e89d2092-2bb5-450e-a421-3708f8306d0c", "stije", "39e1e8e9-cf52-43cf-b36d-6e236781fdf7", "63fda16d-8aff-45d5-a241-91ce02a78240" }
                });

            migrationBuilder.InsertData(
                table: "profile_icons",
                columns: new[] { "id", "picture_color", "url", "user_profile_id" },
                values: new object[,]
                {
                    { "3eb74cb9-bb32-46af-9585-d83c87266290", -3234621, "", "f8b589bd-b91a-4704-9371-73b94b07319f" },
                    { "51bd0982-c371-4ff3-8ab3-346559f94512", -11446385, "", "44bd9c73-fc6e-4749-a0a2-4f8479e33996" },
                    { "5fbbf621-204c-45fc-b0ad-57d1273e1002", -10057921, "", "143eccc3-ff5e-4c46-9d59-c0e411b2410b" },
                    { "72d8301a-5cb4-43e3-b2b4-b39b0cd99e25", -3963880, "", "b06572a6-8128-4024-bf6f-3cc7e1c456f2" },
                    { "7be89c4e-7961-405c-8ca1-dd8bee491bbc", -8387499, "", "a43419bd-bbee-4c58-9766-4739837354cd" },
                    { "8aa9b0a7-d13f-4001-9a98-a3042b6e3628", -10101145, "", "7f154b7f-bb4c-4125-88bb-14f358e242d3" },
                    { "9f56933b-82ed-40fa-aecb-e540d9b52d0e", -5299264, "", "9a6cce86-4e44-4087-b09a-e66a35fdc128" },
                    { "e7dcb8f2-d2d1-4c59-9907-4fe7ad00d3b4", -9672630, "", "93701009-7bc3-49a0-9ca3-5b04eb13bd36" },
                    { "f95493bf-b281-419c-9ab0-3329de898c70", -6386155, "", "5d8d530c-284f-430e-92a7-f4ec84cf0550" },
                    { "fc20ac78-1171-4498-a2fc-a050c7d8c755", -14655722, "", "63fda16d-8aff-45d5-a241-91ce02a78240" },
                    { "fe1657d3-3cfb-4cc4-b84c-aaed8c9baefb", -2188640, "", "4866c9a8-4214-400f-a619-b77fd231afef" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "gains_account_id", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[,]
                {
                    { "1121bbb9-d84a-4785-a82d-cc8524e05ded", 0, "a22fe884-b01d-4c1a-a42d-28606fd35adc", "dyllo@gainstracker.nl", true, "425c6cc9-0ec6-4f26-8181-b5d88dcd7a83", false, null, "DYLLO@GAINSTRACKER.NL", "DYLLO", "AQAAAAIAAYagAAAAEBTXV5xAWwn5tU+7ijReNJtH/i5t4yfynZeH971+H5md76xWcfTMUovpb0tlb0enKg==", null, false, "f74ea4b7-6dfb-4623-bec0-23cafa45b524", false, "dyllo" },
                    { "39e1e8e9-cf52-43cf-b36d-6e236781fdf7", 0, "484087b0-47b7-4f47-8d6a-b42a34dd5f63", "stije@studiostoy.nl", true, "e89d2092-2bb5-450e-a421-3708f8306d0c", false, null, "STIJE@STUDIOSTOY.NL", "STIJE", "AQAAAAIAAYagAAAAEFgwS7CJlhBI/QGk852qGLmmSD0/wlKgqtUlk8PwaiRU/h39RB5pdA1Zkg/CeoGVsw==", null, false, "079e0735-8857-41ef-a8b1-1fbb5cb976a6", false, "stije" },
                    { "45af9e1d-918d-4bac-91be-5a3dafd3632c", 0, "eb8f135e-6a66-4c3d-b1de-69d505fd823c", "sanda@gainstracker.nl", true, "ce467f81-649d-4137-ba49-94196cc87c28", false, null, "SANDA@GAINSTRACKER.NL", "SANDA", "AQAAAAIAAYagAAAAEAwqWgs9q6UqiRnkzebxBoqIkWQWIaFyMuTo08zKraxTXmBQ5i7iEA+f9Js+0VhvLQ==", null, false, "9cf64b81-e1b6-4dd0-bf4f-e56c379c59ff", false, "sanda" },
                    { "46e5fe55-7f52-44b1-b6a7-8c73ccf90fd8", 0, "39e63650-0fdd-4bb8-8edf-1175f674e340", "eef@gainstracker.nl", true, "d035ed6f-89dd-4649-8c1c-495faf5ca40e", false, null, "EEF@GAINSTRACKER.NL", "EEF", "AQAAAAIAAYagAAAAEEYv+GtnWsIqYCV7T14izbhzHs9uUERL9CKdQCma8Ds3j5MYWI5YWU6lFBjfox7ZGA==", null, false, "c6120ec5-ad2c-418f-99b2-c7b4a23f0848", false, "eef" },
                    { "52838699-5ea6-48ad-a0d8-c04863f8ae69", 0, "ae898818-17d4-4399-9215-093507170a02", "arv@gainstracker.nl", true, "64a8f4dd-2fc0-4df4-98ce-778f5400a228", false, null, "ARV@GAINSTRACKER.NL", "ARV", "AQAAAAIAAYagAAAAEIp3D9giPC7/ks9LK5FLgpwt1p9owZ8LTjthpBLOIg0rbBROUwn6DxbCMrXJGv8wBQ==", null, false, "fc4a96a9-8a01-4b98-a1bf-c9c325637747", false, "arv" },
                    { "7786cd53-fe22-4759-96bd-6dd54f39e314", 0, "71b3a2e5-41cc-43c4-826a-373ae340e406", "jordt@gainstracker.nl", true, "8c46367d-5a76-49e6-90f9-0a4d340e3b9d", false, null, "JORDT@GAINSTRACKER.NL", "JORDT", "AQAAAAIAAYagAAAAEB4TzlWFH07frZNCcBmH9gkrhyh69U2hmPe1x9lS5FBFwX8YJAiojHxduarv4bXFpA==", null, false, "c1e74c19-4a82-4397-9bbb-829ed1e960b2", false, "jordt" },
                    { "85c64060-ebc1-4018-adea-851d60cf9ab5", 0, "ee62945c-e6b8-4f36-994d-fb724ff05d64", "soep@gainstracker.nl", true, "69b15a32-3435-40a9-9c27-f07757194ca7", false, null, "SOEP@GAINSTRACKER.NL", "SOEP", "AQAAAAIAAYagAAAAEOa6gc+794FgLGjPRFWreK4jIFsAU2KdwCfXrHnHx5k+OY8uGVeXA9Z1YJHt6rr5cA==", null, false, "b259b2c0-157c-4bce-a190-a1e796da5355", false, "soep" },
                    { "8c6ddacf-85fc-4592-aa30-b2094e2ede57", 0, "95b7db6b-3b49-4c58-85a3-d2337e0078cd", "japser@gainstracker.nl", true, "5b20ae1d-5d24-46ac-ba22-c17a98b7bea6", false, null, "JAPSER@GAINSTRACKER.NL", "JAPSER", "AQAAAAIAAYagAAAAEAv5lLjm+hD6/Iq5Q87LY2men7qWMTG5W5UY/C6Y9W13WR/Dsf7KEBikUCDFjELstQ==", null, false, "0ac532de-78cd-4fdb-931c-ca96367e0e3f", false, "japser" },
                    { "a2293bab-21af-4a81-845f-24fb512d6570", 0, "f19ebe67-bc53-408c-ba6c-5904fba5e6ba", "naoh@gainstracker.nl", true, "324333e7-7b11-49ac-bef1-34d1a1c6a1a2", false, null, "NAOH@GAINSTRACKER.NL", "NAOH", "AQAAAAIAAYagAAAAEDTwmX8mZXzyA+4p9km9/NuAh4l5biHvv7jf0scHADJnPaKhn0EWWxW6bKFhmffeAg==", null, false, "543b4c81-0e28-433f-8b4c-2a0fb396f052", false, "naoh" },
                    { "bef5387a-2d5d-4cbd-9e87-e8b4ba85220a", 0, "6a6bfa02-b984-4eb1-a574-9376dc145acd", "joyo@gainstracker.nl", true, "a62bda19-021b-40bd-9cf4-25be19aa43d1", false, null, "JOYO@GAINSTRACKER.NL", "JOYO", "AQAAAAIAAYagAAAAEBCRK0DXBDyoHBTHUCNUEvoE+l8FbjSjFr6WwBg73h84kOyE4uZCGA+VfzGITTGXdw==", null, false, "74ed3f9a-af82-4861-8c20-9dc78ca7c4bf", false, "joyo" },
                    { "ca246d77-91c3-4b9b-b309-652aa4b0781b", 0, "0b979e94-3e25-491d-b5c0-755430bb68dc", "bino@gainstracker.nl", true, "08a458ec-2d03-4b24-b7df-3e37758ba558", false, null, "BINO@GAINSTRACKER.NL", "BINO", "AQAAAAIAAYagAAAAENgjXcc9a8vxaxPG3ITnPx7iux64u8jvfUksj1tnBTFTvCpaOpVG9IOYNJnf7sHgZQ==", null, false, "2ba688cd-f8a3-4ab9-91f0-2132370b801d", false, "bino" }
                });
        }
    }
}
