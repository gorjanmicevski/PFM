﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PFM.Database;

namespace PFM.Migrations
{
    [DbContext(typeof(TransactionsDbContext))]
    [Migration("20211122102635_MccMigration")]
    partial class MccMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PFM.Database.Entities.CategoryEntity", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParentCode")
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.HasIndex("ParentCode");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("PFM.Database.Entities.SplitTransactionEntity", b =>
                {
                    b.Property<string>("CatCode")
                        .HasColumnType("text");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.HasKey("CatCode", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("splits");
                });

            modelBuilder.Entity("PFM.Database.Entities.TransactionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("BeneficiaryName")
                        .HasColumnType("text");

                    b.Property<string>("CatCode")
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Mcc")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CatCode");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("PFM.Database.Entities.CategoryEntity", b =>
                {
                    b.HasOne("PFM.Database.Entities.CategoryEntity", "ParentCat")
                        .WithMany("ChildCat")
                        .HasForeignKey("ParentCode");

                    b.Navigation("ParentCat");
                });

            modelBuilder.Entity("PFM.Database.Entities.SplitTransactionEntity", b =>
                {
                    b.HasOne("PFM.Database.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CatCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PFM.Database.Entities.TransactionEntity", "Transaction")
                        .WithMany("splits")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("PFM.Database.Entities.TransactionEntity", b =>
                {
                    b.HasOne("PFM.Database.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CatCode");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PFM.Database.Entities.CategoryEntity", b =>
                {
                    b.Navigation("ChildCat");
                });

            modelBuilder.Entity("PFM.Database.Entities.TransactionEntity", b =>
                {
                    b.Navigation("splits");
                });
#pragma warning restore 612, 618
        }
    }
}
