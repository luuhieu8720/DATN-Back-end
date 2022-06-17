﻿// <auto-generated />
using System;
using DATN_Back_end;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DATN_Back_end.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220616151031_Update-nullable-fields")]
    partial class Updatenullablefields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DATN_Back_end.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CommentedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DATN_Back_end.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("DATN_Back_end.Models.ForgetPassword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ForgetPasswords");
                });

            modelBuilder.Entity("DATN_Back_end.Models.FormRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SubmittedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("FormRequests");
                });

            modelBuilder.Entity("DATN_Back_end.Models.FormStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FormStatuses");
                });

            modelBuilder.Entity("DATN_Back_end.Models.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UploadFileLink")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("DATN_Back_end.Models.RequestType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("TypeName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RequestTypes");
                });

            modelBuilder.Entity("DATN_Back_end.Models.Timekeeping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CheckinTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("CheckoutTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Timekeepings");
                });

            modelBuilder.Entity("DATN_Back_end.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Email = "lethiluuhieu@gmail.com",
                            FirstName = "Admin",
                            LastName = "01",
                            Password = "3e9b3a802f5a25bb4c4956a896b0dc84",
                            Role = 3,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("DATN_Back_end.Models.Comment", b =>
                {
                    b.HasOne("DATN_Back_end.Models.Report", "Report")
                        .WithMany("Comments")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("DATN_Back_end.Models.Department", b =>
                {
                    b.HasOne("DATN_Back_end.Models.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("DATN_Back_end.Models.ForgetPassword", b =>
                {
                    b.HasOne("DATN_Back_end.Models.User", "User")
                        .WithMany("ForgetPasswords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DATN_Back_end.Models.FormRequest", b =>
                {
                    b.HasOne("DATN_Back_end.Models.FormStatus", "FormStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_Back_end.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DATN_Back_end.Models.Report", b =>
                {
                    b.HasOne("DATN_Back_end.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DATN_Back_end.Models.Timekeeping", b =>
                {
                    b.HasOne("DATN_Back_end.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DATN_Back_end.Models.User", b =>
                {
                    b.HasOne("DATN_Back_end.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DATN_Back_end.Models.Report", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DATN_Back_end.Models.User", b =>
                {
                    b.Navigation("ForgetPasswords");
                });
#pragma warning restore 612, 618
        }
    }
}
