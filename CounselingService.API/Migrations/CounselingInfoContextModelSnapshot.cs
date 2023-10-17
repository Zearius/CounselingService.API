﻿// <auto-generated />
using CounselingService.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CounselingService.API.Migrations
{
    [DbContext(typeof(CounselingInfoContext))]
    partial class CounselingInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CounselingService.API.Entities.Counseling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Counselor")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Counselings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Counselor = "Catherine Forestrad",
                            Description = "Bi-weekly group, designed to support those recovering from Gambling Addiction",
                            Name = "Gambelers Anonymous"
                        },
                        new
                        {
                            Id = 2,
                            Counselor = "Brian Brackett",
                            Description = "Bi-weekly group, designed to support those recovering from Narcotics Addiction.",
                            Name = "Narcotics Anonymous"
                        },
                        new
                        {
                            Id = 3,
                            Counselor = "Sarah Johnson",
                            Description = "Bi-weekly group, designed to support those recovering from Alcohol Addiction.",
                            Name = "Alcholics Anonymous"
                        });
                });

            modelBuilder.Entity("CounselingService.API.Entities.SpecialEvents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CounselingId")
                        .HasColumnType("int");

                    b.Property<string>("Dscription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CounselingId");

                    b.ToTable("SpecialEvents");
                });

            modelBuilder.Entity("CounselingService.API.Entities.SpecialEvents", b =>
                {
                    b.HasOne("CounselingService.API.Entities.Counseling", "Counseling")
                        .WithMany("SpecialEvents")
                        .HasForeignKey("CounselingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Counseling");
                });

            modelBuilder.Entity("CounselingService.API.Entities.Counseling", b =>
                {
                    b.Navigation("SpecialEvents");
                });
#pragma warning restore 612, 618
        }
    }
}