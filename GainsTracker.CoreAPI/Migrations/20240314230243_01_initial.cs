using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GainsTracker.CoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class _01_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_profiles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    gains_account_id = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_profiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gains_accounts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_handle = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    user_profile_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gains_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_gains_accounts_user_profiles_user_profile_id1",
                        column: x => x.user_profile_id,
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    table.ForeignKey(
                        name: "fk_profile_icons_user_profiles_user_profile_id",
                        column: x => x.user_profile_id,
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    gains_account_id = table.Column<string>(type: "text", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_users_gains_accounts_gains_account_id",
                        column: x => x.gains_account_id,
                        principalTable: "gains_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friend_requests",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    requester_id = table.Column<string>(type: "text", nullable: false),
                    recipient_id = table.Column<string>(type: "text", nullable: false),
                    request_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_friend_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_friend_requests_gains_accounts_recipient_id",
                        column: x => x.recipient_id,
                        principalTable: "gains_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_friend_requests_gains_accounts_requester_id",
                        column: x => x.requester_id,
                        principalTable: "gains_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friends",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    gains_account_id = table.Column<string>(type: "text", nullable: false),
                    friends_since = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    friend_name = table.Column<string>(type: "text", nullable: false),
                    friend_handle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_friends", x => x.id);
                    table.ForeignKey(
                        name: "fk_friends_gains_accounts_gains_account_id",
                        column: x => x.gains_account_id,
                        principalTable: "gains_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "metric",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    logging_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_in_goal = table.Column<bool>(type: "boolean", nullable: false),
                    discriminator = table.Column<string>(type: "text", nullable: false),
                    gains_account_id = table.Column<string>(type: "text", nullable: true),
                    liters = table.Column<double>(type: "double precision", nullable: true),
                    protein_intake = table.Column<long>(type: "bigint", nullable: true),
                    weight = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_metric", x => x.id);
                    table.ForeignKey(
                        name: "fk_metric_gains_accounts_gains_account_id",
                        column: x => x.gains_account_id,
                        principalTable: "gains_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "measurement",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    time_of_record = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false),
                    is_in_goal = table.Column<bool>(type: "boolean", nullable: false),
                    workout_id = table.Column<string>(type: "text", nullable: false),
                    user_profile_id = table.Column<string>(type: "text", nullable: true),
                    discriminator = table.Column<string>(type: "text", nullable: false),
                    general_achievement = table.Column<string>(type: "text", nullable: true),
                    reps = table.Column<int>(type: "integer", nullable: true),
                    weight_unit = table.Column<string>(type: "text", nullable: true),
                    weight = table.Column<double>(type: "double precision", nullable: true),
                    strength_measurement_reps = table.Column<int>(type: "integer", nullable: true),
                    time = table.Column<string>(type: "text", nullable: true),
                    distance_unit = table.Column<string>(type: "text", nullable: true),
                    distance = table.Column<double>(type: "double precision", nullable: true),
                    time_endurance_measurement_time = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measurement", x => x.id);
                    table.ForeignKey(
                        name: "fk_measurement_user_profiles_user_profile_id",
                        column: x => x.user_profile_id,
                        principalTable: "user_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "workout",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<int>(type: "integer", nullable: false),
                    best_measurement_id = table.Column<string>(type: "text", nullable: true),
                    gains_account_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workout", x => x.id);
                    table.ForeignKey(
                        name: "fk_workout_gains_accounts_gains_account_id",
                        column: x => x.gains_account_id,
                        principalTable: "gains_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workout_measurement_best_measurement_id",
                        column: x => x.best_measurement_id,
                        principalTable: "measurement",
                        principalColumn: "id");
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_gains_account_id",
                table: "AspNetUsers",
                column: "gains_account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_friend_requests_recipient_id",
                table: "friend_requests",
                column: "recipient_id");

            migrationBuilder.CreateIndex(
                name: "ix_friend_requests_requester_id",
                table: "friend_requests",
                column: "requester_id");

            migrationBuilder.CreateIndex(
                name: "ix_friends_gains_account_id",
                table: "friends",
                column: "gains_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_gains_accounts_user_profile_id",
                table: "gains_accounts",
                column: "user_profile_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_measurement_user_profile_id",
                table: "measurement",
                column: "user_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_workout_id",
                table: "measurement",
                column: "workout_id");

            migrationBuilder.CreateIndex(
                name: "ix_metric_gains_account_id",
                table: "metric",
                column: "gains_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_profile_icons_user_profile_id",
                table: "profile_icons",
                column: "user_profile_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_workout_best_measurement_id",
                table: "workout",
                column: "best_measurement_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_gains_account_id",
                table: "workout",
                column: "gains_account_id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_workout_workout_id",
                table: "measurement",
                column: "workout_id",
                principalTable: "workout",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workout_gains_accounts_gains_account_id",
                table: "workout");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_user_profiles_user_profile_id",
                table: "measurement");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_workout_workout_id",
                table: "measurement");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "friend_requests");

            migrationBuilder.DropTable(
                name: "friends");

            migrationBuilder.DropTable(
                name: "metric");

            migrationBuilder.DropTable(
                name: "profile_icons");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "gains_accounts");

            migrationBuilder.DropTable(
                name: "user_profiles");

            migrationBuilder.DropTable(
                name: "workout");

            migrationBuilder.DropTable(
                name: "measurement");
        }
    }
}
