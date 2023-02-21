﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicApp.Data;

#nullable disable

namespace MusicApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230221092754_SeedInitialData")]
    partial class SeedInitialData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MusicApp.Models.MusicRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MusicRecord");

                    b.HasData(
                        new
                        {
                            Id = 8,
                            Artist = "Tragedy",
                            Genre = "Hardcore Punk",
                            Name = "Tragedy",
                            Year = 2000
                        },
                        new
                        {
                            Id = 9,
                            Artist = "Tragedy",
                            Genre = "Hardcore Punk",
                            Name = "Nerve Damage",
                            Year = 2006
                        },
                        new
                        {
                            Id = 10,
                            Artist = "Hello Skinny",
                            Genre = "Experimental",
                            Name = "Hello Skinny",
                            Year = 2012
                        });
                });

            modelBuilder.Entity("MusicApp.Models.Musician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instrument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Musicians");

                    b.HasData(
                        new
                        {
                            Id = 6,
                            FullName = "Todd Burdette",
                            Instrument = "Guitar, Vocals"
                        },
                        new
                        {
                            Id = 7,
                            FullName = "Paul Burdette",
                            Instrument = "Drums"
                        },
                        new
                        {
                            Id = 8,
                            FullName = "Billy Davis",
                            Instrument = "Bass"
                        },
                        new
                        {
                            Id = 9,
                            FullName = "Yannick Lorrain",
                            Instrument = "Guitar"
                        });
                });

            modelBuilder.Entity("MusicApp.Models.RecordMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MusicRecordId")
                        .HasColumnType("int");

                    b.Property<int>("MusicianId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MusicRecordId");

                    b.HasIndex("MusicianId");

                    b.ToTable("RecordMembers");

                    b.HasData(
                        new
                        {
                            Id = 28,
                            MusicRecordId = 8,
                            MusicianId = 6
                        },
                        new
                        {
                            Id = 29,
                            MusicRecordId = 8,
                            MusicianId = 7
                        },
                        new
                        {
                            Id = 30,
                            MusicRecordId = 8,
                            MusicianId = 8
                        },
                        new
                        {
                            Id = 31,
                            MusicRecordId = 8,
                            MusicianId = 9
                        });
                });

            modelBuilder.Entity("MusicApp.Models.RecordMember", b =>
                {
                    b.HasOne("MusicApp.Models.MusicRecord", "MusicRecord")
                        .WithMany("RecordMembers")
                        .HasForeignKey("MusicRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicApp.Models.Musician", "Musician")
                        .WithMany("RecordMembers")
                        .HasForeignKey("MusicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MusicRecord");

                    b.Navigation("Musician");
                });

            modelBuilder.Entity("MusicApp.Models.MusicRecord", b =>
                {
                    b.Navigation("RecordMembers");
                });

            modelBuilder.Entity("MusicApp.Models.Musician", b =>
                {
                    b.Navigation("RecordMembers");
                });
#pragma warning restore 612, 618
        }
    }
}