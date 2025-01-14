﻿// <auto-generated />
using System;
using FreeLancer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FreeLancer.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240920090929_Application")]
    partial class Application
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FreeLancer.Models.Models.Domain.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FreeLancer_Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsApplied")
                        .HasColumnType("bit");

                    b.Property<int>("Job_Hirer_Id")
                        .HasColumnType("int");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<string>("SkillLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Create a website for an existing e-commerce app",
                            Duration = "2 months",
                            FreeLancer_Id = 0,
                            IsApplied = false,
                            Job_Hirer_Id = 0,
                            PaymentAmount = 2500m,
                            SkillId = 1,
                            SkillLevel = "Intermediate"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Design the landing page for a website on figma",
                            Duration = "2 months",
                            FreeLancer_Id = 0,
                            IsApplied = false,
                            Job_Hirer_Id = 0,
                            PaymentAmount = 100500m,
                            SkillId = 2,
                            SkillLevel = "Intermediate"
                        });
                });

            modelBuilder.Entity("FreeLancer.Models.Models.Domain.JobApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Application")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FreelancerID")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeUpdated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("FreeLancer.Models.Models.Domain.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SkillName = "Programmer"
                        },
                        new
                        {
                            Id = 2,
                            SkillName = "Graphics Designer"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
