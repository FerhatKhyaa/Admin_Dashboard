﻿// <auto-generated />
using System;
using Admin_Panel_Olusturma.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Admin_Panel_Olusturma.Migrations
{
    [DbContext(typeof(FerhatKahyaDbContext))]
    [Migration("20220723080155_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Admin_Panel_Olusturma.Entities.BlogKategorileri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<string>("KategoriAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KategoriId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("BlogKategorileri");
                });

            modelBuilder.Entity("Admin_Panel_Olusturma.Entities.Bloglar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<string>("Icerik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<string>("KisaAciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resim")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("Bloglar");
                });

            modelBuilder.Entity("Admin_Panel_Olusturma.Entities.BlogKategorileri", b =>
                {
                    b.HasOne("Admin_Panel_Olusturma.Entities.BlogKategorileri", "BlogKategori")
                        .WithMany()
                        .HasForeignKey("KategoriId");

                    b.Navigation("BlogKategori");
                });

            modelBuilder.Entity("Admin_Panel_Olusturma.Entities.Bloglar", b =>
                {
                    b.HasOne("Admin_Panel_Olusturma.Entities.BlogKategorileri", "BlogKategori")
                        .WithMany("Bloglar")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogKategori");
                });

            modelBuilder.Entity("Admin_Panel_Olusturma.Entities.BlogKategorileri", b =>
                {
                    b.Navigation("Bloglar");
                });
#pragma warning restore 612, 618
        }
    }
}
