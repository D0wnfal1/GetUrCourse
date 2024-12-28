﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GetUrCourse.Orchestrator.Migrations
{
    [DbContext(typeof(OrchestratorDbContext))]
    partial class OrchestratorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GetUrCourse.Orchestrator.Sagas.RegisterNewUserSagaData", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Faulted")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserAdded")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("UserNotified")
                        .HasColumnType("boolean");

                    b.HasKey("CorrelationId");

                    b.ToTable("RegisterNewUserSagaData");
                });
#pragma warning restore 612, 618
        }
    }
}
