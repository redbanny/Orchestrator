﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrchestratorAPI.Contexts;

#nullable disable

namespace OrchestratorAPI.Migrations
{
    [DbContext(typeof(TurnDbContext))]
    [Migration("20231011125848_firstMigration")]
    partial class firstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrchestratorAPI.Models.Turn", b =>
                {
                    b.Property<int>("TurnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TurnId"));

                    b.Property<string>("TurnName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TurnId");

                    b.ToTable("Turns");
                });

            modelBuilder.Entity("OrchestratorAPI.Models.TurnItem", b =>
                {
                    b.Property<int>("TurnItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TurnItemId"));

                    b.Property<DateTime>("Create_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("InputDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Item_Status")
                        .HasColumnType("int");

                    b.Property<int?>("TurnId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("TurnItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("TurnItemId");

                    b.HasIndex("TurnId");

                    b.ToTable("TurnItems");
                });

            modelBuilder.Entity("OrchestratorAPI.Models.TurnItem", b =>
                {
                    b.HasOne("OrchestratorAPI.Models.Turn", "Turn")
                        .WithMany("TurnItems")
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turn");
                });

            modelBuilder.Entity("OrchestratorAPI.Models.Turn", b =>
                {
                    b.Navigation("TurnItems");
                });
#pragma warning restore 612, 618
        }
    }
}
