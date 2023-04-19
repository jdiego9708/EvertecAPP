using Microsoft.EntityFrameworkCore;
using SISEvertec.Entities.Models;

namespace SISEvertec.APP.Services
{
    public class EvertecDBContext : DbContext
    {
        public DbSet<Configuration_evertec> Configurations_evertec { get; set; }
        public DbSet<Visitors> Visitors { get; set; }
        public EvertecDBContext(DbContextOptions<EvertecDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitors>(entity =>
            {
                entity.ToTable("Visitors");

                entity.Property(e => e.Id)
                   .HasColumnName("Id")
                   .ValueGeneratedOnAdd();

                entity.Property(e => e.Name_visitor)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Image_visitor)
                    .IsRequired()
                    .HasMaxLength(8000);

                entity.Property(e => e.Date_visitor)
                    .IsRequired();

                entity.Property(e => e.Hour_visitor)
                    .IsRequired();
            });

            modelBuilder.Entity<Configuration_evertec>(entity =>
            {
                entity.ToTable("Configuration_evertec");

                entity.Property(e => e.Id)
                   .HasColumnName("Id")
                   .ValueGeneratedOnAdd();

                entity.Property(e => e.Theme_default)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Languaje_default)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Last_changed)
                    .IsRequired();
            });
        }
        public async Task CreateBDSQLite()
        {
            await this.Database.EnsureCreatedAsync();
        }
    }
}
