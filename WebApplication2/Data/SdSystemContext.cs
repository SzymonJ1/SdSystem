using Microsoft.EntityFrameworkCore;
using SdSystem.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SdSystem.Data
{
    public class SdSystemContext : DbContext
    {
        public SdSystemContext(DbContextOptions<SdSystemContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedTo)
                .WithMany()
                .HasForeignKey(t => t.AssignedToId);
        }
    }
}
