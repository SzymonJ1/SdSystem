using Microsoft.EntityFrameworkCore;
namespace SDsystem.Entities


{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }
        public DbSet<TicketEntity> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SdSystemDb;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
