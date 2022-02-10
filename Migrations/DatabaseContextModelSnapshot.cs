﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using digify.Shared;

#nullable disable

namespace digify.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClassUser", b =>
                {
                    b.Property<Guid>("ClassesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeachersId")
                        .HasColumnType("uuid");

                    b.HasKey("ClassesId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("ClassUser");
                });

            modelBuilder.Entity("digify.Models.Class", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("digify.Models.Timetable", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Timetables");
                });

            modelBuilder.Entity("digify.Models.TimeTableElement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SubjectColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Teacher")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TimeTableElements");
                });

            modelBuilder.Entity("digify.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("Roles")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid?>("SchoolClassId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClassUser", b =>
                {
                    b.HasOne("digify.Models.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("digify.Models.User", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("digify.Models.Timetable", b =>
                {
                    b.HasOne("digify.Models.User", "OwningUser")
                        .WithOne("Timetable")
                        .HasForeignKey("digify.Models.Timetable", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwningUser");
                });

            modelBuilder.Entity("digify.Models.TimeTableElement", b =>
                {
                    b.HasOne("digify.Models.Timetable", "Parent")
                        .WithMany("TableElements")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("digify.Models.User", b =>
                {
                    b.HasOne("digify.Models.Class", "SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId");

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("digify.Models.Class", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("digify.Models.Timetable", b =>
                {
                    b.Navigation("TableElements");
                });

            modelBuilder.Entity("digify.Models.User", b =>
                {
                    b.Navigation("Timetable");
                });
#pragma warning restore 612, 618
        }
    }
}
