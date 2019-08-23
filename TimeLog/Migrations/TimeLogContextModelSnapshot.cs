﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeLog.Models;

namespace TimeLog.Migrations
{
    [DbContext(typeof(TimeLogContext))]
    partial class TimeLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeLog.Models.ActivityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityTypeId");

                    b.Property<bool>("Billable");

                    b.Property<int?>("ClientId");

                    b.Property<DateTime?>("EndTime");

                    b.Property<string>("InvoiceStatement");

                    b.Property<int?>("LocationId");

                    b.Property<int?>("ParentId");

                    b.Property<int?>("ProjectId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("Tasks");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.HasIndex("ClientId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ActivityEntity");
                });

            modelBuilder.Entity("TimeLog.Models.ActivityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ActivityTypes");
                });

            modelBuilder.Entity("TimeLog.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TimeLog.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TimeLog.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DefaultActivityTypeId");

                    b.Property<int?>("DefaultClientId");

                    b.Property<int?>("DefaultLocationId");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DefaultActivityTypeId");

                    b.HasIndex("DefaultClientId");

                    b.HasIndex("DefaultLocationId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TimeLog.Models.ActivityEntity", b =>
                {
                    b.HasOne("TimeLog.Models.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeLog.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("TimeLog.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("TimeLog.Models.ActivityEntity", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TimeLog.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("TimeLog.Models.Project", b =>
                {
                    b.HasOne("TimeLog.Models.ActivityType", "DefaultActivityType")
                        .WithMany()
                        .HasForeignKey("DefaultActivityTypeId");

                    b.HasOne("TimeLog.Models.Client", "DefaultClient")
                        .WithMany()
                        .HasForeignKey("DefaultClientId");

                    b.HasOne("TimeLog.Models.Location", "DefaultLocation")
                        .WithMany()
                        .HasForeignKey("DefaultLocationId");
                });
#pragma warning restore 612, 618
        }
    }
}
