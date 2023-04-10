using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Data
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne(x => x.Patient)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Hospital)
                .WithMany(x => x.Patients)
                .HasForeignKey(x => x.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CaregiverPatient)
                .WithMany(x => x.Patients)
                .HasForeignKey(x => x.CaregiverPatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
