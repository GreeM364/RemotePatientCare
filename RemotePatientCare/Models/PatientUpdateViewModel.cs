﻿using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.API.Models
{
    public class PatientUpdateViewModel
    {
        [Required] 
        public string HospitalId { get; set; } = null!;

        public string? DoctorId { get; set; }

        public string? CaregiverPatientId { get; set; }

        [Required] 
        public string FirstName { get; set; } = null!;

        [Required] 
        public string LastName { get; set; } = null!;

        [Required] 
        public string Patronymic { get; set; } = null!;

        [Required] 
        public string Phone { get; set; } = null!;

        [Required] 
        public string Email { get; set; } = null!;

        [Required] 
        public DateTime BirthDate { get; set; }
    }
}
