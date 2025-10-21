using Microsoft.EntityFrameworkCore;
using TAS.Models;

namespace TAS.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        public DbSet<Dealer> Dealers => Set<Dealer>();
        public DbSet<Garden> Gardens => Set<Garden>();
        public DbSet<UserAccount> UserAccount => Set<UserAccount>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Dealer>(e =>
            {
                e.HasKey(x => x.DealerId);
                e.HasIndex(x => x.Name);
                e.HasIndex(x => new { x.Province, x.District });
                e.HasIndex(x => x.Code).IsUnique();
                e.Property(x => x.Code).HasMaxLength(20).IsUnicode(false);
                e.Property(x => x.TaxCode).HasMaxLength(20).IsUnicode(false);
                e.Property(x => x.Phone).HasMaxLength(20).IsUnicode(false);
                e.Property(x => x.Email).HasMaxLength(120);
            });

            modelBuilder.Entity<Garden>(e =>
            {
                e.HasKey(x => x.GardenId);
                e.Property(x => x.Code).HasMaxLength(20).IsUnicode(false);
                e.Property(x => x.OwnerPhone).HasMaxLength(20).IsUnicode(false);
                e.Property(x => x.OwnerIdNo).HasMaxLength(20).IsUnicode(false);
                e.Property(x => x.AreaHa).HasPrecision(10, 2);
                e.Property(x => x.AvgLatexKgDay).HasPrecision(10, 2);
                e.Property(x => x.Latitude).HasPrecision(9, 6);
                e.Property(x => x.Longitude).HasPrecision(9, 6);

                e.HasIndex(x => x.DealerId);
                e.HasIndex(x => x.OwnerPhone);
                e.HasIndex(x => new { x.Province, x.District });
                e.HasIndex(x => new { x.DealerId, x.Code }).IsUnique();

                e.HasOne(x => x.Dealer)
                 .WithMany(d => d.Gardens)
                 .HasForeignKey(x => x.DealerId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
			modelBuilder.Entity<UserAccount>(e =>
			{
				e.ToTable("USER_ACCOUNT");
				e.HasKey(x => x.UserId);
				e.Property(x => x.UserId).HasDefaultValueSql("NEWSEQUENTIALID()");
				e.Property(x => x.Email).HasMaxLength(256).IsRequired();

				e.Property<string>("NormalizedEmail")
					.HasColumnName("NormalizedEmail")
					.HasComputedColumnSql("UPPER([Email])", stored: true); // PERSISTED

				// Quan trọng: filter dùng [Email], không dùng [NormalizedEmail]
				e.HasIndex("NormalizedEmail")
					.IsUnique()
					.HasFilter("[Email] IS NOT NULL");
			});
		}
	}
}
