using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Configuration;

public class AccountConfiguration: IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(a => a.Email).HasColumnName("email").HasMaxLength(320);
        builder.Property(a => a.Password).HasColumnName("password").HasMaxLength(120);
        builder.Property(a => a.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(a => a.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}