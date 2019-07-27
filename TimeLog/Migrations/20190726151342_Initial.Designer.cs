﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeLog.Models;

namespace TimeLog.Migrations
{
    [DbContext(typeof(TimeLogContext))]
    [Migration("20190726151342_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeLog.Models.ActivityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Billable");

                    b.Property<string>("Client");

                    b.Property<string>("ETag");

                    b.Property<string>("EndTime");

                    b.Property<string>("PartitionKey");

                    b.Property<string>("RowKey");

                    b.Property<string>("Tasks");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("ActivityEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
