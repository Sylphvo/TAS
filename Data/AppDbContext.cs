using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TAS.Helpers;
using TAS.Models;

namespace TAS.Data
{
	public class ApplicationDbContext : IdentityDbContext<UserAccountIdentity, IdentityRole<Guid>, Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) { }


		public DbSet<RubberAgent> rubberAgent { get; set; }
		public DbSet<RubberFarmDb> gardens { get; set; }
		public DbSet<RubberIntakeDb> rubberIntakeDb { get; set; }
		//public DbSet<RubberOrderSummary> rubberOrderSummaries { get; set; }
		public DbSet<RubberPalletDb> rubberPallets { get; set; }
		public DbSet<UserAccount> Users { get; set; }
		// Configure entity mappings and relationships
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// RubberAgent configuration
			modelBuilder.Entity<RubberAgent>(e =>
            {
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
				e.Property(x => x.RegisterDate).HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
				e.Property(x => x.UpdateDate).HasColumnType("datetime2");
				e.Property(x => x.RegisterPerson).HasMaxLength(100);
				e.Property(x => x.UpdatePerson).HasMaxLength(100);

				// Unique nếu có TaxCode
				e.HasIndex(x => x.TaxCode).IsUnique().HasFilter("[TaxCode] IS NOT NULL");
			});

			// RubberFarmDb configuration
			modelBuilder.Entity<RubberFarmDb>(e =>
            {
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
				e.Property(x => x.FarmPhone).HasMaxLength(20);
				e.Property(x => x.FarmAddress).HasMaxLength(300);
				e.Property(x => x.Certificates).HasMaxLength(500);

				e.Property(x => x.TotalAreaHa).HasPrecision(12, 2);
				e.Property(x => x.RubberAreaHa).HasPrecision(12, 2);
				e.Property(x => x.TotalExploit).HasPrecision(14, 2);

				e.Property(x => x.IsActive).HasDefaultValue(true);

				// SQL Server
				e.Property(x => x.RegisterDate).HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
				e.Property(x => x.UpdateDate).HasColumnType("datetime2");

				e.Property(x => x.RegisterPerson).HasMaxLength(100);
				e.Property(x => x.UpdatePerson).HasMaxLength(100);
				e.Property(x => x.Polygon).HasMaxLength(500);
			});
			
			// UserAccount configuration
			modelBuilder.Entity<UserAccount>(e =>
			{
                e.ToTable("UserAccount");
                e.Property(x => x.UserId).HasDefaultValueSql("NEWID()");

                e.Property(x => x.FirstName).HasMaxLength(100);
                e.Property(x => x.LastName).HasMaxLength(100);
                e.Property(x => x.UserName).IsRequired().HasMaxLength(256);
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

			// Tùy chọn: đổi tên bảng, KHÔNG đụng tới HasKey/HasNoKey
			base.OnModelCreating(modelBuilder); // bắt buộc
			modelBuilder.ApplyConfiguration(new UserAccountIdentityConfig());

			// Đổi tên các bảng Identity khác nếu muốn thống nhất:
			modelBuilder.Entity<IdentityRole<Guid>>().ToTable("USER_ROLE");
			modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("USER_IN_ROLE");
			modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("USER_CLAIM");
			modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("ROLE_CLAIM");
			modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("USER_LOGIN");
			modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("USER_TOKEN");

			// RubberIntakeDb configuration
			modelBuilder.Entity<RubberIntakeDb>(e =>
			{
				e.HasKey(x => x.IntakeId);
				e.Property(x => x.IntakeId)
					.UseIdentityColumn()
					.ValueGeneratedOnAdd();

				e.Property(x => x.FarmCode)
					.HasMaxLength(200)
					.IsRequired();

				e.Property(x => x.FarmerName)
					.HasMaxLength(200)
					.IsRequired();

				e.Property(x => x.RubberKg).HasPrecision(12, 3);
				e.Property(x => x.TSCPercent).HasPrecision(5, 2);
				e.Property(x => x.DRCPercent).HasPrecision(5, 2);
				e.Property(x => x.FinishedProductKg).HasPrecision(12, 3);
				e.Property(x => x.CentrifugeProductKg).HasPrecision(12, 3);

				e.Property(x => x.Status);

				e.Property(x => x.RegisterPerson).HasMaxLength(50);
				e.Property(x => x.UpdatePerson).HasMaxLength(50);

				e.Property(x => x.RegisterDate).HasColumnType("datetime2");
				e.Property(x => x.UpdateDate).HasColumnType("datetime2");

				// Index phục vụ truy vấn
				e.HasIndex(x => x.FarmCode);
				e.HasIndex(x => new { x.FarmCode, x.RegisterDate });
			});

			// RubberOrderSummary configuration
			//modelBuilder.Entity<RubberOrderSummary>(entity =>
			//{
			//	entity.HasKey(e => e.OrderId);
			//	entity.Property(e => e.OrderCode).IsRequired().HasMaxLength(50);
			//	entity.Property(e => e.OrderName).IsRequired().HasMaxLength(200);
			//	entity.Property(e => e.AgentCode).HasMaxLength(50);
			//	entity.Property(e => e.AgentName).HasMaxLength(200);
			//	entity.Property(e => e.FarmCode).HasMaxLength(50);
			//	entity.Property(e => e.FarmerName).HasMaxLength(200);
			//	entity.Property(e => e.TotalWeightKg).HasColumnType("decimal(18,2)");
			//	entity.Property(e => e.PricePerKg).HasColumnType("decimal(18,2)");
			//	entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
			//	entity.Property(e => e.SortOrder).HasDefaultValue(1);
			//	entity.Property(e => e.IsActive).HasDefaultValue(true);
			//	entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			//});

			// RubberPalletDb configuration
			//modelBuilder.Entity<RubberPalletDb>(e =>	
			//{
			//	e.Property(x => x.WeightKg).HasPrecision(12, 3);
			//	e.HasIndex(x => new { x.OrderId, x.PalletNo }).IsUnique();
			//	e.HasOne<RubberOrderSummary>()
			//	 .WithMany()
			//	 .HasForeignKey(x => x.OrderId)
			//	 .OnDelete(DeleteBehavior.Cascade);
			//});
		}
	}
}
