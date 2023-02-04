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
        }
        public DbSet<MusicRecord> MusicRecord { get; set; } = default!;

        public DbSet<Musician> Musicians { get; set; }

        public DbSet<RecordMember> RecordMembers { get; set; }
    }
}
