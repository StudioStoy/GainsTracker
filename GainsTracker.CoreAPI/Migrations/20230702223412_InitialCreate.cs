using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GainsTracker.CoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    description = table.Column<string>(type: "text", nullable: false),
                    picture_url = table.Column<string>(type: "text", nullable: false)
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
                    user_id = table.Column<string>(type: "text", nullable: false),
                    user_profile_id = table.Column<string>(type: "text", nullable: false),
                    user_handle = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false)
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
                    discriminator = table.Column<string>(type: "text", nullable: false),
                    user_profile_id = table.Column<string>(type: "text", nullable: true),
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
                columns: new[] { "id", "description", "gains_account_id", "picture_url" },
                values: new object[,]
                {
                    { "4524ad66-78f5-4952-a144-255adda2b35b", "", "0bd85f22-6fbc-401e-8dac-ed23ce459d76", "" },
                    { "6ac1ba55-3e89-4e92-8889-dca9c22d86b3", "", "f2aa01d1-1b96-4c4d-99ef-fcd4c4208d31", "" },
                    { "7212d4fd-765f-4515-a612-19d72e0ed6f5", "", "2c79bba1-a6e1-402a-bd20-246a4ffa5b47", "" },
                    { "94993ee3-c1f1-4fdd-83a6-37685075018b", "", "260e0355-f7c5-4b35-bfd3-f085a13200d6", "" },
                    { "b7991944-4729-4ce1-b90d-981516109bd7", "", "d7b07d94-5d17-4f50-93f4-c863cee7e116", "" },
                    { "be4e806f-1493-4982-b84f-2d5c035c0f7d", "", "60a56bc1-95c7-45f9-ae06-4176efa921a2", "" },
                    { "ca8adf96-377d-42ed-99a5-13b359f14a2c", "", "dffc4e16-28e7-4e67-889a-fd982fe75248", "" },
                    { "fbbcfb7b-e6e4-45a3-b483-2ce36bac2e8c", "", "a2943a09-67f7-48e5-9493-16c71bfbaf7b", "" }
                });

            migrationBuilder.InsertData(
                table: "gains_accounts",
                columns: new[] { "id", "display_name", "user_handle", "user_id", "user_profile_id" },
                values: new object[,]
                {
                    { "0bd85f22-6fbc-401e-8dac-ed23ce459d76", "", "BINO", "21ff9abe-cb37-4a63-a3be-40aea8d8fd7a", "4524ad66-78f5-4952-a144-255adda2b35b" },
                    { "260e0355-f7c5-4b35-bfd3-f085a13200d6", "", "naoh", "225a2c18-9e35-49e7-b6d9-02d05facb51c", "94993ee3-c1f1-4fdd-83a6-37685075018b" },
                    { "2c79bba1-a6e1-402a-bd20-246a4ffa5b47", "", "Joyo", "2519eb74-ccb3-4d14-9055-569d7fa36f25", "7212d4fd-765f-4515-a612-19d72e0ed6f5" },
                    { "60a56bc1-95c7-45f9-ae06-4176efa921a2", "DavrozzGaining", "stije", "67e16748-e62f-44ec-8525-f3bd60730c47", "be4e806f-1493-4982-b84f-2d5c035c0f7d" },
                    { "a2943a09-67f7-48e5-9493-16c71bfbaf7b", "", "soep", "ef745569-8ba4-4108-b689-cd804e55a15d", "fbbcfb7b-e6e4-45a3-b483-2ce36bac2e8c" },
                    { "d7b07d94-5d17-4f50-93f4-c863cee7e116", "", "eef", "03b0da2a-70af-48ed-9082-cd4e19d281bd", "b7991944-4729-4ce1-b90d-981516109bd7" },
                    { "dffc4e16-28e7-4e67-889a-fd982fe75248", "", "sanda", "1bc8e283-d64d-4a1e-9152-af6fc308de98", "ca8adf96-377d-42ed-99a5-13b359f14a2c" },
                    { "f2aa01d1-1b96-4c4d-99ef-fcd4c4208d31", "", "jordt", "e6d506fc-1691-4471-8b14-5cc455695255", "6ac1ba55-3e89-4e92-8889-dca9c22d86b3" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "gains_account_id", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[,]
                {
                    { "03b0da2a-70af-48ed-9082-cd4e19d281bd", 0, "43a93c58-d554-42b7-bb66-47135be533b9", "test@studiostoy.nl", false, "d7b07d94-5d17-4f50-93f4-c863cee7e116", false, null, "TEST@STUDIOSTOY.NL", "EEF", "AQAAAAIAAYagAAAAELMeYUsk/Dhcs3aas//CykNZ3ZazA+7A9GZolomGqtplXpYreBmuY/IL5xN3mSg+uA==", null, false, "b85cd636-d792-4972-8951-901a11a32462", false, "eef" },
                    { "1bc8e283-d64d-4a1e-9152-af6fc308de98", 0, "38ac6db5-9a48-4805-87e9-85ebac49f72a", "test@studiostoy.nl", false, "dffc4e16-28e7-4e67-889a-fd982fe75248", false, null, "TEST@STUDIOSTOY.NL", "SANDA", "AQAAAAIAAYagAAAAEAkz+ff/9gp46RBLgTPROgUlCZOfSnofw6tNya/yKpAC5krSuCH8vtb5Y2OIXjmEDg==", null, false, "1d829106-9301-4404-a71f-85add8d174de", false, "sanda" },
                    { "21ff9abe-cb37-4a63-a3be-40aea8d8fd7a", 0, "7d5b1b42-9e89-4e21-acd0-44762d975dbc", "test@studiostoy.nl", false, "0bd85f22-6fbc-401e-8dac-ed23ce459d76", false, null, "TEST@STUDIOSTOY.NL", "BINO", "AQAAAAIAAYagAAAAEIznVI6SLir5a/63ZNK2CNa+ImfNwZdUUiZsPQwyY6gqgp0pLrEUyYyWZA66Sh6vbw==", null, false, "424d0747-4747-4e00-98e1-a024b907ec1d", false, "BINO" },
                    { "225a2c18-9e35-49e7-b6d9-02d05facb51c", 0, "2d15f5f0-2a21-4397-ae9a-2e704402feba", "test@studiostoy.nl", false, "260e0355-f7c5-4b35-bfd3-f085a13200d6", false, null, "TEST@STUDIOSTOY.NL", "NAOH", "AQAAAAIAAYagAAAAEC8fv7lIbCiryX3rWDS43VdNRSlzV4OVbE0Kl33+U80C8H6IUUa7VLDggCaRor6xhg==", null, false, "7d4a3874-ce3f-4d0e-8bd6-ec49a6cfa240", false, "naoh" },
                    { "2519eb74-ccb3-4d14-9055-569d7fa36f25", 0, "f5b413a9-ff2f-458e-921b-6cbf88ba2318", "joy@studiostoy.nl", false, "2c79bba1-a6e1-402a-bd20-246a4ffa5b47", false, null, "JOY@STUDIOSTOY.NL", "JOYO", "AQAAAAIAAYagAAAAEMOb4lK5ayGll0ZeDGOVIDTIWH0Z7JRldo6zrtD4BdyHeAFQgvRz30eSI00UkARZww==", null, false, "5097f9aa-299a-4318-bcd6-0e53f9d7fc64", false, "Joyo" },
                    { "67e16748-e62f-44ec-8525-f3bd60730c47", 0, "93458379-5d65-42d7-a7d6-70dc1eb4a0a2", "stije@studiostoy.nl", true, "60a56bc1-95c7-45f9-ae06-4176efa921a2", false, null, "STIJE@STUDIOSTOY.NL", "STIJE", "AQAAAAIAAYagAAAAEDJhTY++Q0d/LW61sWRVYIOFuJwEXZFrG6BCq3hBO5NGwWT6+p1EmwcQTgUn6loa6Q==", null, false, "1ba784a3-654b-4c49-bca4-676aad9a6823", false, "stije" },
                    { "e6d506fc-1691-4471-8b14-5cc455695255", 0, "3f9fe415-e6f6-4faa-9608-f9798a0cc3d2", "test@studiostoy.nl", false, "f2aa01d1-1b96-4c4d-99ef-fcd4c4208d31", false, null, "TEST@STUDIOSTOY.NL", "JORDT", "AQAAAAIAAYagAAAAEIR1AuV6Je8xMmQoSwLEfwoV3Zzy9JpkEKHHKPZQp+D+CFwbd4NibauL1RV6303/2Q==", null, false, "d99fb239-4fa3-4a29-9ce1-4edb2ce371b4", false, "jordt" },
                    { "ef745569-8ba4-4108-b689-cd804e55a15d", 0, "e7c1c170-3727-40e8-b69a-272adf6f199d", "test@studiostoy.nl", false, "a2943a09-67f7-48e5-9493-16c71bfbaf7b", false, null, "TEST@STUDIOSTOY.NL", "SOEP", "AQAAAAIAAYagAAAAEE62U/vZW7u3iwIQMGnmskap+RRI8PUp062Cr/8rmfrnHiFFdxj/NWpL+xheCRyFEA==", null, false, "c1deef9b-6ae6-4ff0-a7e4-785ab5fd9193", false, "soep" }
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
