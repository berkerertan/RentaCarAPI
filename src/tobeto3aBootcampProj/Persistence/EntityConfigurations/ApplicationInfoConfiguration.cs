using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ApplicationInfoConfiguration : IEntityTypeConfiguration<ApplicationInfo>
{
    public void Configure(EntityTypeBuilder<ApplicationInfo> builder)
    {
        builder.ToTable("ApplicationInfoes").HasKey(ai => ai.Id);

        builder.Property(ai => ai.Id).HasColumnName("Id").IsRequired();
        builder.Property(ai => ai.ApplicantId).HasColumnName("ApplicantId");
        builder.Property(ai => ai.BootcampId).HasColumnName("BootcampId");
        builder.Property(ai => ai.ApplicationStateId).HasColumnName("ApplicationStateId");
        builder.Property(ai => ai.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ai => ai.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ai => ai.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(x => x.Applicant);
        builder.HasOne(x => x.Bootcamp);
        builder.HasOne(x => x.ApplicationState);

        builder.HasQueryFilter(ai => !ai.DeletedDate.HasValue);
    }
}
