﻿// <auto-generated />
using System;
using MagazineConnector.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagazineConnector.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20210826225716_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("MagazineConnector.Model.Article", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long>("idAuthor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("viewCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("id")
                        .IsUnique();

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("MagazineConnector.Model.Author", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("id")
                        .IsUnique();

                    b.ToTable("Authors");
                });
#pragma warning restore 612, 618
        }
    }
}
