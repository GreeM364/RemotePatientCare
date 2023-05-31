using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Data
{
    public class CaregiverPatientConfiguration : IEntityTypeConfiguration<CaregiverPatient>
    {
        public void Configure(EntityTypeBuilder<CaregiverPatient> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne(x => x.CaregiverPatient)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Patients)
                .WithOne(x => x.CaregiverPatient)
                .HasForeignKey(x => x.CaregiverPatientId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
