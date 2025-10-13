using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Configuration;

public class TrainConfiguration : IEntityTypeConfiguration<Train>
{
    public void Configure(EntityTypeBuilder<Train> builder)
    {
        builder.ToTable("trains");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name");
        builder.Property(t => t.TrainNumber).HasColumnName("train_number");
        builder.Property(t => t.DelayMinutes).HasColumnName("delay_minutes");
        builder.Property(t => t.NextStop).HasColumnName("next_stop");
        builder.Property(t => t.LastUpdated).HasColumnName("last_updated");
        builder.Property(t=>t.HasFeedback).HasColumnName("has_feedback");
    
    }
}