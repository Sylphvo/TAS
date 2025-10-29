using Microsoft.EntityFrameworkCore;
using TAS.Helpers;
using TAS.Models;

namespace TAS.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        public DbSet<RubberAgent> rubberAgent { get; set; }
		public DbSet<RubberFarmDb> gardens { get; set; }
		public DbSet<RubberIntakeDb> rubberIntakeDb { get; set; }
		public DbSet<RubberOrderSummary> rubberOrderSummaries { get; set; }
		public DbSet<UserAccount> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<RubberAgent>(e =>
            {
                e.ToTable("RubberAgent");
				e.HasKey(x => x.AgentId);
				e.Property(x => x.AgentId)
					.UseIdentityColumn()
					.ValueGeneratedOnAdd();

				e.Property(x => x.AgentCode).IsRequired().HasMaxLength(200);
				e.HasIndex(x => x.AgentCode).IsUnique();
				e.Property(x => x.AgentName).HasMaxLength(200);
				e.Property(x => x.OwnerName).HasMaxLength(200);
				e.Property(x => x.TaxCode).HasMaxLength(50);
				e.Property(x => x.AgentAddress).HasMaxLength(500);
				e.Property(x => x.IsActive).HasDefaultValue(true);

				// SQL Server
				e.Property(x => x.CreatedAt).HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
				e.Property(x => x.UpdatedAt).HasColumnType("datetime2");
				e.Property(x => x.CreatedBy).HasMaxLength(100);
				e.Property(x => x.UpdatedBy).HasMaxLength(100);

				// Unique nếu có TaxCode
				e.HasIndex(x => x.TaxCode).IsUnique().HasFilter("[TaxCode] IS NOT NULL");
			});

            modelBuilder.Entity<RubberFarmDb>(e =>
            {
                e.ToTable("RubberFarm");
				e.HasKey(x => x.FarmId);
				e.Property(x => x.FarmId)
					.UseIdentityColumn()
					.ValueGeneratedOnAdd();

				e.Property(x => x.FarmCode).IsRequired().HasMaxLength(200);
				e.HasIndex(x => x.FarmCode).IsUnique();

				e.Property(x => x.AgentCode).IsRequired().HasMaxLength(200);
				e.HasIndex(x => x.AgentCode);

				// FK dùng Alternate Key: Agent.AgentCode
				e.HasOne(x => x.Rubber_Agent)
				 .WithMany() // hoặc .WithMany(a => a.Farms) nếu bạn thêm ICollection<Farm> vào Agent
				 .HasPrincipalKey(a => a.AgentCode)
				 .HasForeignKey(x => x.AgentCode)
				 .OnDelete(DeleteBehavior.Restrict);

				e.Property(x => x.FarmerName).HasMaxLength(200);
				e.Property(x => x.FarmerPhone).HasMaxLength(20);
				e.Property(x => x.FarmerAddress).HasMaxLength(300);
				e.Property(x => x.FarmerMap).HasMaxLength(300);
				e.Property(x => x.Certificates).HasMaxLength(500);

				e.Property(x => x.TotalAreaHa).HasPrecision(12, 2);
				e.Property(x => x.RubberAreaHa).HasPrecision(12, 2);
				e.Property(x => x.TotalExploit).HasPrecision(14, 2);

				e.Property(x => x.IsActive).HasDefaultValue(true);

				// SQL Server
				e.Property(x => x.CreatedAt).HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
				e.Property(x => x.UpdatedAt).HasColumnType("datetime2");

				e.Property(x => x.CreatedBy).HasMaxLength(100);
				e.Property(x => x.UpdatedBy).HasMaxLength(100);
			});
			modelBuilder.Entity<UserAccount>(e =>
			{
                e.ToTable("UserAccount");
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
			modelBuilder.Entity<RubberIntakeDb>(e =>
			{
				e.ToTable("RubberIntake");
				e.HasKey(x => x.Id);
				e.Property(x => x.FarmCode).IsRequired().HasMaxLength(200);
				e.Property(x => x.FarmerName).IsRequired().HasMaxLength(200);
				e.Property(x => x.Kg).HasColumnType("decimal(12,3)");
				e.Property(x => x.TSCPercent).HasColumnType("decimal(5,2)");
				e.Property(x => x.DRCPercent).HasColumnType("decimal(5,2)");
				e.Property(x => x.FinishedProductKg).HasColumnType("decimal(12,3)");
				e.Property(x => x.CentrifugeProductKg).HasColumnType("decimal(12,3)");
				e.Property(x => x.BatchCode).HasMaxLength(50);
			});
			modelBuilder.Entity<RubberOrderSummary>(entity =>
			{
				entity.ToTable("RubberOrderSummary");
				entity.HasKey(e => e.OrderId);
				entity.Property(e => e.OrderCode).IsRequired().HasMaxLength(50);
				entity.Property(e => e.OrderName).IsRequired().HasMaxLength(200);
				entity.Property(e => e.AgentCode).HasMaxLength(50);
				entity.Property(e => e.AgentName).HasMaxLength(200);
				entity.Property(e => e.FarmCode).HasMaxLength(50);
				entity.Property(e => e.FarmerName).HasMaxLength(200);
				entity.Property(e => e.TotalWeightKg).HasColumnType("decimal(18,2)");
				entity.Property(e => e.PricePerKg).HasColumnType("decimal(18,2)");
				entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
				entity.Property(e => e.SortOrder).HasDefaultValue(1);
				entity.Property(e => e.IsActive).HasDefaultValue(true);
				entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			});
		}
	}
}
