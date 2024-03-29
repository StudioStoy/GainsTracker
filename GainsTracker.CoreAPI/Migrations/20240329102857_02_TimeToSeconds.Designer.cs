﻿// <auto-generated />
using System;
using GainsTracker.CoreAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GainsTracker.CoreAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240329102857_02_TimeToSeconds")]
    partial class _02_TimeToSeconds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Friends.Models.Friend", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("FriendHandle")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("friend_handle");

                    b.Property<string>("FriendName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("friend_name");

                    b.Property<DateTime>("FriendsSince")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("friends_since");

                    b.Property<string>("GainsAccountId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gains_account_id");

                    b.HasKey("Id")
                        .HasName("pk_friends");

                    b.HasIndex("GainsAccountId")
                        .HasDatabaseName("ix_friends_gains_account_id");

                    b.ToTable("friends", (string)null);
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Friends.Models.FriendRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("RecipientId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("recipient_id");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("request_time");

                    b.Property<string>("RequesterId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("requester_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_friend_requests");

                    b.HasIndex("RecipientId")
                        .HasDatabaseName("ix_friend_requests_recipient_id");

                    b.HasIndex("RequesterId")
                        .HasDatabaseName("ix_friend_requests_requester_id");

                    b.ToTable("friend_requests", (string)null);
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.HealthMetrics.Models.Metric", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("discriminator");

                    b.Property<string>("GainsAccountId")
                        .HasColumnType("text")
                        .HasColumnName("gains_account_id");

                    b.Property<bool>("IsInGoal")
                        .HasColumnType("boolean")
                        .HasColumnName("is_in_goal");

                    b.Property<DateTime>("LoggingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("logging_date");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_metric");

                    b.HasIndex("GainsAccountId")
                        .HasDatabaseName("ix_metric_gains_account_id");

                    b.ToTable("metric");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Metric");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Security.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("GainsAccountId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gains_account_id");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("GainsAccountId")
                        .IsUnique()
                        .HasDatabaseName("ix_asp_net_users_gains_account_id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.UserProfiles.Models.ProfileIcon", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("PictureColor")
                        .HasColumnType("integer")
                        .HasColumnName("picture_color");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<string>("UserProfileId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_profile_id");

                    b.HasKey("Id")
                        .HasName("pk_profile_icons");

                    b.HasIndex("UserProfileId")
                        .IsUnique()
                        .HasDatabaseName("ix_profile_icons_user_profile_id");

                    b.ToTable("profile_icons", (string)null);
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.UserProfiles.Models.UserProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<string>("GainsAccountId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gains_account_id");

                    b.HasKey("Id")
                        .HasName("pk_user_profiles");

                    b.ToTable("user_profiles", (string)null);
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("UserHandle")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_handle");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("UserProfileId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_profile_id");

                    b.HasKey("Id")
                        .HasName("pk_gains_accounts");

                    b.HasIndex("UserProfileId")
                        .IsUnique()
                        .HasDatabaseName("ix_gains_accounts_user_profile_id");

                    b.ToTable("gains_accounts", (string)null);
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("discriminator");

                    b.Property<bool>("IsInGoal")
                        .HasColumnType("boolean")
                        .HasColumnName("is_in_goal");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<DateTime>("TimeOfRecord")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_of_record");

                    b.Property<string>("UserProfileId")
                        .HasColumnType("text")
                        .HasColumnName("user_profile_id");

                    b.Property<string>("WorkoutId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("workout_id");

                    b.HasKey("Id")
                        .HasName("pk_measurement");

                    b.HasIndex("UserProfileId")
                        .HasDatabaseName("ix_measurement_user_profile_id");

                    b.HasIndex("WorkoutId")
                        .HasDatabaseName("ix_measurement_workout_id");

                    b.ToTable("measurement");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Measurement");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Workouts.Workout", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("BestMeasurementId")
                        .HasColumnType("text")
                        .HasColumnName("best_measurement_id");

                    b.Property<int>("Category")
                        .HasColumnType("integer")
                        .HasColumnName("category");

                    b.Property<string>("GainsAccountId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gains_account_id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_workout");

                    b.HasIndex("BestMeasurementId")
                        .HasDatabaseName("ix_workout_best_measurement_id");

                    b.HasIndex("GainsAccountId")
                        .HasDatabaseName("ix_workout_gains_account_id");

                    b.ToTable("workout", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_role_claims_role_id");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_claims_user_id");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_logins_user_id");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_user_roles_role_id");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.HealthMetrics.Models.LiterWaterMetric", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.HealthMetrics.Models.Metric");

                    b.Property<double>("Liters")
                        .HasColumnType("double precision")
                        .HasColumnName("liters");

                    b.ToTable("metric");

                    b.HasDiscriminator().HasValue("LiterWaterMetric");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.HealthMetrics.Models.ProteinMetric", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.HealthMetrics.Models.Metric");

                    b.Property<long>("ProteinIntake")
                        .HasColumnType("bigint")
                        .HasColumnName("protein_intake");

                    b.ToTable("metric");

                    b.HasDiscriminator().HasValue("ProteinMetric");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.HealthMetrics.Models.WeightMetric", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.HealthMetrics.Models.Metric");

                    b.Property<long>("Weight")
                        .HasColumnType("bigint")
                        .HasColumnName("weight");

                    b.ToTable("metric");

                    b.HasDiscriminator().HasValue("WeightMetric");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.GeneralMeasurement", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement");

                    b.Property<string>("GeneralAchievement")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("general_achievement");

                    b.ToTable("measurement");

                    b.HasDiscriminator().HasValue("GeneralMeasurement");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.RepsMeasurement", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement");

                    b.Property<int>("Reps")
                        .HasColumnType("integer")
                        .HasColumnName("reps");

                    b.ToTable("measurement");

                    b.HasDiscriminator().HasValue("RepsMeasurement");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.StrengthMeasurement", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement");

                    b.Property<int>("Reps")
                        .HasColumnType("integer")
                        .HasColumnName("reps");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<string>("WeightUnit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("weight_unit");

                    b.ToTable("measurement", t =>
                        {
                            t.Property("Reps")
                                .HasColumnName("strength_measurement_reps");
                        });

                    b.HasDiscriminator().HasValue("StrengthMeasurement");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.TimeAndDistanceEnduranceMeasurement", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision")
                        .HasColumnName("distance");

                    b.Property<string>("DistanceUnit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("distance_unit");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("time");

                    b.ToTable("measurement");

                    b.HasDiscriminator().HasValue("TimeAndDistanceEnduranceMeasurement");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.TimeEnduranceMeasurement", b =>
                {
                    b.HasBaseType("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("time");

                    b.ToTable("measurement", t =>
                        {
                            t.Property("Time")
                                .HasColumnName("time_endurance_measurement_time");
                        });

                    b.HasDiscriminator().HasValue("TimeEnduranceMeasurement");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Friends.Models.Friend", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", null)
                        .WithMany("Friends")
                        .HasForeignKey("GainsAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_friends_gains_accounts_gains_account_id");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Friends.Models.FriendRequest", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", "Recipient")
                        .WithMany("ReceivedFriendRequests")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_friend_requests_gains_accounts_recipient_id");

                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", "Requester")
                        .WithMany("SentFriendRequests")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_friend_requests_gains_accounts_requester_id");

                    b.Navigation("Recipient");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.HealthMetrics.Models.Metric", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", null)
                        .WithMany("Metrics")
                        .HasForeignKey("GainsAccountId")
                        .HasConstraintName("fk_metric_gains_accounts_gains_account_id");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Security.Models.User", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", "GainsAccount")
                        .WithOne()
                        .HasForeignKey("GainsTracker.CoreAPI.Components.Security.Models.User", "GainsAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_users_gains_accounts_gains_account_id");

                    b.Navigation("GainsAccount");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.UserProfiles.Models.ProfileIcon", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.UserProfiles.Models.UserProfile", null)
                        .WithOne("Icon")
                        .HasForeignKey("GainsTracker.CoreAPI.Components.UserProfiles.Models.ProfileIcon", "UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_profile_icons_user_profiles_user_profile_id");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.UserProfiles.Models.UserProfile", "UserProfile")
                        .WithOne()
                        .HasForeignKey("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", "UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_gains_accounts_user_profiles_user_profile_id1");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.UserProfiles.Models.UserProfile", null)
                        .WithMany("PinnedPBs")
                        .HasForeignKey("UserProfileId")
                        .HasConstraintName("fk_measurement_user_profiles_user_profile_id");

                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.Workouts.Workout", null)
                        .WithMany("Measurements")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_measurement_workout_workout_id");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Workouts.Workout", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Measurement", "PersonalBest")
                        .WithMany()
                        .HasForeignKey("BestMeasurementId")
                        .HasConstraintName("fk_workout_measurement_best_measurement_id");

                    b.HasOne("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", null)
                        .WithMany("Workouts")
                        .HasForeignKey("GainsAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_workout_gains_accounts_gains_account_id");

                    b.Navigation("PersonalBest");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Security.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Security.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                    b.HasOne("GainsTracker.CoreAPI.Components.Security.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GainsTracker.CoreAPI.Components.Security.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.UserProfiles.Models.UserProfile", b =>
                {
                    b.Navigation("Icon")
                        .IsRequired();

                    b.Navigation("PinnedPBs");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.GainsAccount", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("Metrics");

                    b.Navigation("ReceivedFriendRequests");

                    b.Navigation("SentFriendRequests");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("GainsTracker.CoreAPI.Components.Workouts.Models.Workouts.Workout", b =>
                {
                    b.Navigation("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
