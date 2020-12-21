﻿// <auto-generated />
using System;
using HomeWork34;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeWork34.Migrations
{
    [DbContext(typeof(DocumentStorageContext))]
    [Migration("20201221223118_FirstNameChanged")]
    partial class FirstNameChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeWork34.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HomeWork34.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("HomeWork34.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512)
                        .IsUnicode(true);

                    b.Property<int>("Pages")
                        .HasColumnType("int")
                        .HasMaxLength(512);

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("FirstName")
                        .IsUnique()
                        .HasFilter("[FirstName] IS NOT NULL");

                    b.ToTable("DocPositions");
                });

            modelBuilder.Entity("HomeWork34.DocumentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverAddressId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverEmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("SenderAddressId")
                        .HasColumnType("int");

                    b.Property<int>("SenderEmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("ReceiverAddressId");

                    b.HasIndex("ReceiverEmployeeId");

                    b.HasIndex("SenderAddressId");

                    b.HasIndex("SenderEmployeeId");

                    b.HasIndex("StatusId");

                    b.ToTable("DocumentStatuses");
                });

            modelBuilder.Entity("HomeWork34.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FullName")
                        .IsUnique()
                        .HasFilter("[FullName] IS NOT NULL");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HomeWork34.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("HomeWork34.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("HomeWork34.Address", b =>
                {
                    b.HasOne("HomeWork34.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeWork34.DocumentStatus", b =>
                {
                    b.HasOne("HomeWork34.Document", "Document")
                        .WithMany("DocumentStatuses")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HomeWork34.Address", "ReceiverAddress")
                        .WithMany("ReceiverDocumentStatuses")
                        .HasForeignKey("ReceiverAddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HomeWork34.Employee", "ReceiverEmployee")
                        .WithMany("ReceiverDocumentStatuses")
                        .HasForeignKey("ReceiverEmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HomeWork34.Address", "SenderAddress")
                        .WithMany("SenderDocumentStatuses")
                        .HasForeignKey("SenderAddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HomeWork34.Employee", "SenderEmployee")
                        .WithMany("SenderDocumentStatuses")
                        .HasForeignKey("SenderEmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HomeWork34.Status", "Status")
                        .WithMany("DocumentStatuses")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeWork34.Employee", b =>
                {
                    b.HasOne("HomeWork34.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
