﻿using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;

        public HospitalService(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;

        }
        public async Task<List<HospitalDTO>> GetAsync()
        {
            var source = await _hospitalRepository.GetAllAsync();
            return _mapper.Map<List<HospitalDTO>>(source); ;
        }

        public async Task<HospitalDTO> GetByIdAsync(string id)
        {
            var source = await _hospitalRepository.GetByIdAsync(id);

            if (source == null)
            {
                throw new Exception($"Hospital with id \"{id}\" not found");
            }

            return _mapper.Map<HospitalDTO>(source);
        }
        public async Task<HospitalDTO> CreateAsync(HospitalCreateDTO request)
        {
            var existing = await _hospitalRepository.GetAsync(x => x.Name == request.Name);

            if (existing != null)
            {
                throw new Exception($"Hospital with name {request.Name} already exists");
            }
            if (request == null)
            {
                throw new Exception("The request model of Hospital is null");
            }

            var createEntity = _mapper.Map<Hospital>(request);
            await _hospitalRepository.CreateAsync(createEntity);

            var result = _mapper.Map<HospitalDTO>(createEntity);
            return result;
        }

        public async Task<HospitalDTO> UpdateAsync(string id, HospitalUpdateDTO request)
        {
            if (request == null)
                throw new Exception("The received model of Hospital is null");

            if (await _hospitalRepository.GetAsync(x => x.Name == request.Name && x.Id != id) != null)
                throw new Exception("Hospital with such name already exists");

            if (await _hospitalRepository.GetAsync(x => x.Address == request.Address && x.Id != id) != null)
                throw new Exception("Hospital with such address already exists");

            var updateEntity = _mapper.Map<Hospital>(request);
            await _hospitalRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<HospitalDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var hospital = await _hospitalRepository.GetAsync(v => v.Id == id);

            if (hospital == null)
                throw new Exception($"Hospital with such id {id} not found for deletion");

            await _hospitalRepository.RemoveAsync(hospital);
        }
    }
}
