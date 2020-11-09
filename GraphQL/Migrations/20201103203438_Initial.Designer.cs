﻿// <auto-generated />
using System;
using Eshop.GraphQL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eshop.GraphQL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201103203438_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("Eshop.GraphQL.Data.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("OrderDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Eshop.GraphQL.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Eshop.GraphQL.Data.UserOrder", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("UserOrder");
                });

            modelBuilder.Entity("Eshop.GraphQL.Data.UserOrder", b =>
                {
                    b.HasOne("Eshop.GraphQL.Data.Order", "Order")
                        .WithMany("UserOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eshop.GraphQL.Data.User", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop.GraphQL.Data.Order", b =>
                {
                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("Eshop.GraphQL.Data.User", b =>
                {
                    b.Navigation("UserOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
