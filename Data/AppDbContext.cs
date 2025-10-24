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

        public DbSet<Agent> Agents => Set<Agent>();
        public DbSet<Garden> Gardens => Set<Garden>();
        public DbSet<UserAccount> Users => Set<UserAccount>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Agent>(e =>
            {
                e.ToTable("Agent");
                e.HasKey(x => x.AgentId);
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
                e.ToTable("Graden");
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
                e.ToTable("User");
                e.HasKey(x => x.UserId);
                e.Property(x => x.UserId).HasDefaultValueSql("NEWID()");

                e.Property(x => x.FirstName).HasMaxLength(100);
                e.Property(x => x.LastName).HasMaxLength(100);
                e.Property(x => x.UserName).IsRequired().HasMaxLength(100);
                e.Property(x => x.Email).IsRequired().HasMaxLength(256);

                // varbinary cho hash
                e.Property(x => x.PasswordHash).HasColumnType("varbinary(max)").IsRequired();

                e.Property(x => x.AcceptTerms).HasDefaultValue(true);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.Property(x => x.TwoFactorEnabled).HasDefaultValue(false);

                e.Property(x => x.LogIn).HasColumnType("datetime2");
                e.Property(x => x.LogOut).HasColumnType("datetime2");

                // index duy nhất
                e.HasIndex(x => x.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
                e.HasIndex(x => x.UserName).IsUnique().HasFilter("[UserName] IS NOT NULL");
            });
		}
	}
}
