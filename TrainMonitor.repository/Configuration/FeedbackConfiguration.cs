using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Configuration;

public class FeedbackConfiguration : IEntityTypeConfiguration<FeedBack>
{
    public void Configure(EntityTypeBuilder<FeedBack> builder)
    {
        builder.ToTable("feedbacks");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Username).HasColumnName("username");
        builder.Property(x => x.ReasonForDelay).HasColumnName("reason_for_delay");
        builder.Property(x => x.AditionalMessage).HasColumnName("aditional_message");
        builder.Property(x => x.TrainNumber).HasColumnName("train_number");
        builder.Property(x => x.TrainId).HasColumnName("train_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.HasOne(t => t.Train).WithMany(x => x.FeedBacks).HasForeignKey(x => x.TrainId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(a => a.Account).WithMany(x => x.FeedBacks).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}