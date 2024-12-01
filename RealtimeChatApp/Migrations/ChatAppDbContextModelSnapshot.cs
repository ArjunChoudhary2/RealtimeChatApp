﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RealtimeChatApp.Migrations
{
    [DbContext(typeof(ChatAppDbContext))]
    partial class ChatAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Challenges", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ChallangeText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("challange_text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("InitiatorId")
                        .HasColumnType("integer");

                    b.Property<int?>("MessageId")
                        .HasColumnType("integer")
                        .HasColumnName("message_id");

                    b.Property<string>("Penalty")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("penalty");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.ChatParticipants", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("integer")
                        .HasColumnName("chat_id");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("joined_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("ChatParticipants");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Chats", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ChatName")
                        .HasColumnType("text")
                        .HasColumnName("chat_name");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("User1_Id")
                        .HasColumnType("uuid")
                        .HasColumnName("user1_id");

                    b.Property<Guid>("User2_Id")
                        .HasColumnType("uuid")
                        .HasColumnName("user2_id");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.MessageMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("MediaType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("media_type");

                    b.Property<string>("MediaUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("media_url");

                    b.Property<int>("MessageId")
                        .HasColumnType("integer")
                        .HasColumnName("message_id");

                    b.HasKey("Id");

                    b.ToTable("MessageMetadata");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Messages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<bool>("IsChallenge")
                        .HasColumnType("boolean")
                        .HasColumnName("is_challange");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean")
                        .HasColumnName("read");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid")
                        .HasColumnName("recevier_id");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid")
                        .HasColumnName("sender_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Notifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.UserStatusHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MoodStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("mood_status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("UserStatusHistories");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("MoodStatus")
                        .HasColumnType("text")
                        .HasColumnName("mood_status");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("bytea")
                        .HasColumnName("profile_pic");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Messages", b =>
                {
                    b.HasOne("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Chats", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("RealtimeChatApp.RealtimeChatApp.Domain.Entities.Chats", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
