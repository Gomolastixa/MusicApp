using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApp.Models;

namespace MusicApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordMember>()
                .HasOne(mr => mr.MusicRecord)
                .WithMany(rm => rm.RecordMembers)
                .HasForeignKey(ri => ri.MusicRecordId);

            modelBuilder.Entity<RecordMember>()
                        .HasOne(m => m.Musician)
                        .WithMany(rm => rm.RecordMembers)
                        .HasForeignKey(mi => mi.MusicianId);

            modelBuilder.Entity<MusicRecord>().HasData(
                new MusicRecord { Id = 8, Name = "Tragedy", Artist = "Tragedy", Year = 2000, Genre = "Hardcore Punk"},
                new MusicRecord { Id = 9, Name = "Nerve Damage", Artist = "Tragedy", Year = 2006, Genre = "Hardcore Punk" },
                new MusicRecord { Id = 10, Name = "Hello Skinny", Artist = "Hello Skinny", Year = 2012, Genre = "Experimental" }
                );

            modelBuilder.Entity<Musician>().HasData(
                new Musician { Id = 6, FullName = "Todd Burdette", Instrument = "Guitar, Vocals" },
                new Musician { Id = 7, FullName = "Paul Burdette", Instrument = "Drums" },
                new Musician { Id = 8, FullName = "Billy Davis", Instrument = "Bass" },
                new Musician { Id = 9, FullName = "Yannick Lorrain", Instrument = "Guitar" }
                
                );

            modelBuilder.Entity<RecordMember>().HasData(
                new RecordMember {Id = 28, MusicRecordId = 8 ,MusicianId = 6 },
                new RecordMember { Id = 29, MusicRecordId = 8, MusicianId = 7 },
                new RecordMember { Id = 30, MusicRecordId = 8, MusicianId = 8 },
                new RecordMember { Id = 31, MusicRecordId = 8, MusicianId = 9 }
               
                );
        }
    
        public DbSet<MusicRecord> MusicRecord { get; set; } = default!;

        public DbSet<Musician> Musicians { get; set; }

        public DbSet<RecordMember> RecordMembers { get; set; }
    }
}
