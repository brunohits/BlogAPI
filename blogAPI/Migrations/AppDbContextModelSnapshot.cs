﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using blogAPI.Data;

#nullable disable

namespace blogAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("blogAPI.Data.Models.UserModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("birthDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("createTime")
                        .HasColumnType("date");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("fullName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("blogAPI.Dto.UserDto", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CommunityModelid")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("birthDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("createTime")
                        .HasColumnType("date");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("CommunityModelid");

                    b.ToTable("UserDto");
                });

            modelBuilder.Entity("blogAPI.Entities.Token", b =>
                {
                    b.Property<string>("InvalidToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("InvalidToken");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("blogAPI.Models.CommentModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PostModelid")
                        .HasColumnType("uuid");

                    b.Property<Guid>("authorId")
                        .HasColumnType("uuid");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("deleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("modifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("parentCommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("postId")
                        .HasColumnType("uuid");

                    b.Property<int>("subComments")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PostModelid");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("blogAPI.Models.CommentsPosts", b =>
                {
                    b.Property<Guid>("postId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("commentId")
                        .HasColumnType("uuid");

                    b.HasKey("postId", "commentId");

                    b.ToTable("CommentsPosts");
                });

            modelBuilder.Entity("blogAPI.Models.CommunityModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isClosed")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("subscribersCount")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("id");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("blogAPI.Models.PostModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("addressId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("authorId")
                        .HasColumnType("uuid");

                    b.Property<string>("authorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("commentsCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("communityId")
                        .HasColumnType("uuid");

                    b.Property<string>("communityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("hasLikes")
                        .HasColumnType("boolean");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("likes")
                        .HasColumnType("integer");

                    b.Property<int>("readingTime")
                        .HasColumnType("integer");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("blogAPI.Models.PostTags", b =>
                {
                    b.Property<Guid>("postId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("tagId")
                        .HasColumnType("uuid");

                    b.HasKey("postId", "tagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("blogAPI.Models.TagModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PostModelid")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("createTime")
                        .HasColumnType("date");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("PostModelid");

                    b.HasIndex("id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("blogAPI.Models.UserCommunityRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "CommunityId");

                    b.ToTable("UserCommunityRoles");
                });

            modelBuilder.Entity("blogAPI.Models.UserPostLikes", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Like")
                        .HasColumnType("boolean");

                    b.HasKey("UserId", "PostId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("blogAPI.Models.UsersCommunitiesModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "CommunityId");

                    b.ToTable("UsersSubscriptions");
                });

            modelBuilder.Entity("blogAPI.Dto.UserDto", b =>
                {
                    b.HasOne("blogAPI.Models.CommunityModel", null)
                        .WithMany("administrators")
                        .HasForeignKey("CommunityModelid");
                });

            modelBuilder.Entity("blogAPI.Models.CommentModel", b =>
                {
                    b.HasOne("blogAPI.Models.PostModel", null)
                        .WithMany("comments")
                        .HasForeignKey("PostModelid");
                });

            modelBuilder.Entity("blogAPI.Models.TagModel", b =>
                {
                    b.HasOne("blogAPI.Models.PostModel", null)
                        .WithMany("tags")
                        .HasForeignKey("PostModelid");
                });

            modelBuilder.Entity("blogAPI.Models.CommunityModel", b =>
                {
                    b.Navigation("administrators");
                });

            modelBuilder.Entity("blogAPI.Models.PostModel", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("tags");
                });
#pragma warning restore 612, 618
        }
    }
}
