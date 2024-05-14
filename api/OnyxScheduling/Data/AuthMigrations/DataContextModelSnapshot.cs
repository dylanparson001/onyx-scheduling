﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("OnyxScheduling.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("OnyxScheduling.Models.InvoiceInvoice_Item", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InvoiceItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("InvoiceId", "JobId", "InvoiceItemId");

                    b.HasIndex("InvoiceItemId");

                    b.HasIndex("JobId");

                    b.ToTable("InvoiceInvoice_Item");

                    b.HasData(
                        new
                        {
                            InvoiceId = 1,
                            JobId = 0,
                            InvoiceItemId = 123,
                            Quantity = 2
                        },
                        new
                        {
                            InvoiceId = 2,
                            JobId = 0,
                            InvoiceItemId = 234,
                            Quantity = 0
                        },
                        new
                        {
                            InvoiceId = 2,
                            JobId = 0,
                            InvoiceItemId = 123,
                            Quantity = 10
                        });
                });

            modelBuilder.Entity("OnyxScheduling.Models.Invoice_Items", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Category_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Item_Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Invoice_Items");

                    b.HasData(
                        new
                        {
                            Id = 123,
                            Category_Id = 0,
                            Item_Name = "234 Spring Red",
                            Price = 210.0
                        },
                        new
                        {
                            Id = 234,
                            Category_Id = 0,
                            Item_Name = "4' Nylon Rollers",
                            Price = 15.0
                        });
                });

            modelBuilder.Entity("OnyxScheduling.Models.Invoices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Assigned_Customer_Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Assigned_Technician_Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Processing_Status")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScheduledEndDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScheduledStartDateTime")
                        .HasColumnType("TEXT");

                    b.Property<double>("Total_Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Invoices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Assigned_Customer_Id = "01c84c0a-84f1-4504-94ba-ce28a4c99245",
                            Assigned_Technician_Id = "2",
                            CreatedDateTime = new DateTime(2024, 5, 14, 13, 46, 36, 979, DateTimeKind.Local).AddTicks(7921),
                            FinishedDateTime = new DateTime(2024, 5, 14, 13, 46, 36, 979, DateTimeKind.Local).AddTicks(7964),
                            InvoiceNumber = "INV001",
                            JobId = 0,
                            ScheduledEndDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ScheduledStartDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total_Price = 0.0
                        },
                        new
                        {
                            Id = 2,
                            Assigned_Customer_Id = "01c84c0a-84f1-4504-94ba-ce28a4c99245",
                            Assigned_Technician_Id = "2",
                            CreatedDateTime = new DateTime(2024, 5, 14, 13, 46, 36, 979, DateTimeKind.Local).AddTicks(7969),
                            FinishedDateTime = new DateTime(2024, 5, 14, 13, 46, 36, 979, DateTimeKind.Local).AddTicks(7970),
                            InvoiceNumber = "INV002",
                            JobId = 0,
                            ScheduledEndDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ScheduledStartDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total_Price = 0.0
                        });
                });

            modelBuilder.Entity("OnyxScheduling.Models.Jobs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Assigned_Customer_Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Assigned_Technician_Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InvoiceNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Processing_Status")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScheduledEndDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScheduledStartDateTime")
                        .HasColumnType("TEXT");

                    b.Property<double>("Total_Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("OnyxScheduling.Models.InvoiceInvoice_Item", b =>
                {
                    b.HasOne("OnyxScheduling.Models.Invoices", "Invoice")
                        .WithMany("InvoiceInvoice_Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnyxScheduling.Models.Invoice_Items", "InvoiceItem")
                        .WithMany("InvoiceInvoiceItems")
                        .HasForeignKey("InvoiceItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnyxScheduling.Models.Jobs", "Job")
                        .WithMany("InvoiceInvoice_Items")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("InvoiceItem");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("OnyxScheduling.Models.Invoice_Items", b =>
                {
                    b.Navigation("InvoiceInvoiceItems");
                });

            modelBuilder.Entity("OnyxScheduling.Models.Invoices", b =>
                {
                    b.Navigation("InvoiceInvoice_Items");
                });

            modelBuilder.Entity("OnyxScheduling.Models.Jobs", b =>
                {
                    b.Navigation("InvoiceInvoice_Items");
                });
#pragma warning restore 612, 618
        }
    }
}
