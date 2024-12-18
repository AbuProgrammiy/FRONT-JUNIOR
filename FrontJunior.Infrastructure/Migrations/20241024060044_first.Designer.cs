﻿// <auto-generated />
using System;
using FrontJunior.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FrontJunior.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241024060044_first")]
    partial class first
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FrontJunior.Domain.Entities.Models.PrimaryModels.DataStorage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Column1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Column10")
                        .HasColumnType("text");

                    b.Property<string>("Column11")
                        .HasColumnType("text");

                    b.Property<string>("Column12")
                        .HasColumnType("text");

                    b.Property<string>("Column13")
                        .HasColumnType("text");

                    b.Property<string>("Column14")
                        .HasColumnType("text");

                    b.Property<string>("Column15")
                        .HasColumnType("text");

                    b.Property<string>("Column16")
                        .HasColumnType("text");

                    b.Property<string>("Column17")
                        .HasColumnType("text");

                    b.Property<string>("Column18")
                        .HasColumnType("text");

                    b.Property<string>("Column19")
                        .HasColumnType("text");

                    b.Property<string>("Column2")
                        .HasColumnType("text");

                    b.Property<string>("Column20")
                        .HasColumnType("text");

                    b.Property<string>("Column3")
                        .HasColumnType("text");

                    b.Property<string>("Column4")
                        .HasColumnType("text");

                    b.Property<string>("Column5")
                        .HasColumnType("text");

                    b.Property<string>("Column6")
                        .HasColumnType("text");

                    b.Property<string>("Column7")
                        .HasColumnType("text");

                    b.Property<string>("Column8")
                        .HasColumnType("text");

                    b.Property<string>("Column9")
                        .HasColumnType("text");

                    b.Property<bool>("IsData")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("DataStorage");
                });

            modelBuilder.Entity("FrontJunior.Domain.Entities.Models.PrimaryModels.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte>("ColumnCount")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Stars")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("FrontJunior.Domain.Entities.Models.PrimaryModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<byte[]>("PassworSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FrontJunior.Domain.Entities.Models.SecondaryModels.Verification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SentPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Verifications");
                });

            modelBuilder.Entity("FrontJunior.Domain.Entities.Models.PrimaryModels.DataStorage", b =>
                {
                    b.HasOne("FrontJunior.Domain.Entities.Models.PrimaryModels.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("FrontJunior.Domain.Entities.Models.PrimaryModels.Table", b =>
                {
                    b.HasOne("FrontJunior.Domain.Entities.Models.PrimaryModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
