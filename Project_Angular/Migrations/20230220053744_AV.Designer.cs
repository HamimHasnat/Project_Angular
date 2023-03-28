﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Angular;

#nullable disable

namespace Project_Angular.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20230220053744_AV")]
    partial class AV
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project_Angular.Appointment", b =>
                {
                    b.Property<string>("Appointment_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Appointment_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Appointment_ID");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("Project_Angular.Service", b =>
                {
                    b.Property<string>("Service_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Appointment_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service_Fee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Service_ID");

                    b.HasIndex("Appointment_ID");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Project_Angular.Service", b =>
                {
                    b.HasOne("Project_Angular.Appointment", "Appointment")
                        .WithMany("Service")
                        .HasForeignKey("Appointment_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Project_Angular.Appointment", b =>
                {
                    b.Navigation("Service");
                });
#pragma warning restore 612, 618
        }
    }
}
