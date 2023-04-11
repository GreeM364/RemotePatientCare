﻿namespace RemotePatientCare.BLL.DataTransferObjects
{
    public class HospitalDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public int DoctorsCount { get; set; }
        public int PatientsCount { get; set; }
    }
}
