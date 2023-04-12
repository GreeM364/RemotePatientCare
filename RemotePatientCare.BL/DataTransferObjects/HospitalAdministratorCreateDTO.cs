﻿namespace RemotePatientCare.BLL.DataTransferObjects
{
    public class HospitalAdministratorCreateDTO
    {
        public string HospitalId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Password { get; set; } = null!; 
    }
}
