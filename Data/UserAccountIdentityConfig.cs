using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TAS.Helpers;

namespace TAS.Data
{
	public class UserAccountIdentityConfig : IEntityTypeConfiguration<UserAccountIdentity>
	{
		public void Configure(EntityTypeBuilder<UserAccountIdentity> e)
		{
			e.ToTable("USER_ACCOUNT");                  // đổi tên bảng
			e.Property(x => x.Id).HasColumnName("UserId"); // map Id -> UserId (uniqueidentifier)

			e.Property(x => x.UserName).HasMaxLength(256);
			e.Property(x => x.NormalizedUserName).HasMaxLength(256);
			e.Property(x => x.Email).HasMaxLength(256);
			e.Property(x => x.NormalizedEmail).HasMaxLength(256);

			e.Property(x => x.IsActive).HasDefaultValue(true);
			e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("SYSUTCDATETIME()");

			// Index chuẩn Identity. Base.OnModelCreating cũng sẽ tạo, nên không cần lặp lại.
			// e.HasIndex(x => x.NormalizedUserName).IsUnique();
			// e.HasIndex(x => x.NormalizedEmail);
		}
	}
}
