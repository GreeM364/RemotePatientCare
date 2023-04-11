﻿using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;
using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.BLL.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IHospitalRepository hospitalRepository,
                              IDoctorRepository doctorRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _hospitalRepository = hospitalRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<PatientDTO> GetByIdAsync(string id)
        {
            var source = await _patientRepository.GetAsync(x => x.Id == id, includeProperties: "User");

            if (source == null)
            {
                throw new Exception($"Patient with id {id} not found");
            }

            return _mapper.Map<PatientDTO>(source);
        }

        public async Task<List<PatientDTO>> GetAsync()
        {
            var source = await _patientRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<PatientDTO>>(source);
        }

        public async Task<PatientDTO> CreateAsync(PatientCreateDTO request)
        {
            if (await _hospitalRepository.GetAsync(x => x.Id == request.HospitalId) == null)
                throw new Exception($"Hospital with id {request.HospitalId} doesn't exist");

            var existing = await _patientRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                  && x.User.LastName == request.LastName
                                                                  && x.User.Phone == request.Phone);

            if (existing != null)
                throw new ValidationException("Patient with such parameters already exists");

            if (!string.IsNullOrWhiteSpace(request.DoctorId))
            {
                if (await _doctorRepository.GetAsync(x => x.Id == request.DoctorId) == null)
                    throw new ValidationException($"Doctor with id {request.DoctorId} doesn't exist");
            }

            var createEntity = _mapper.Map<Patient>(request);
            await _patientRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<PatientDTO>(createEntity);
            return result;
        }

        public async Task<PatientDTO> UpdateAsync(string id, PatientUpdateDTO request)
        {
            if (request == null)
                throw new Exception("The received model of Patient is null");

            if (await _patientRepository.GetAsync(x => x.Id == id) != null)
                throw new Exception($"Patient with id {id} not found");

            var updateEntity = _mapper.Map<Patient>(request);
            await _patientRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<PatientDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var doctor = await _patientRepository.GetAsync(x => x.Id == id);

            if (doctor == null)
                throw new Exception($"Patient with such id {id} not found for deletion");

            await _patientRepository.RemoveAsync(doctor);
        }
    }
}