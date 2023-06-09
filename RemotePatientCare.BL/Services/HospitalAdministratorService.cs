﻿using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class HospitalAdministratorService : IHospitalAdministratorService
    {
        private readonly IHospitalAdministratorRepository _hospitalAdministratorRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;


        public HospitalAdministratorService(IHospitalAdministratorRepository hospitalAdministratorRepository, 
                                            IMapper mapper, IHospitalRepository hospitalRepository)
        {
            _hospitalAdministratorRepository = hospitalAdministratorRepository;
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
        }

        public async Task<HospitalAdministratorDTO> GetByIdAsync(string id)
        {
            var source = await _hospitalAdministratorRepository.GetAsync(x => x.Id == id, "User");

            if (source == null)
            {
                throw new NotFoundException($"Hospital Administrator with id {id} not found");
            }

            return _mapper.Map<HospitalAdministratorDTO>(source);
        }

        public async Task<List<HospitalAdministratorDTO>> GetAsync()
        {
            var source = await _hospitalAdministratorRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<HospitalAdministratorDTO>>(source);
        }

        public async Task<HospitalAdministratorDTO> CreateAsync(HospitalAdministratorCreateDTO request)
        {
            if (await _hospitalRepository.GetAsync(x => x.Id == request.HospitalId) == null)
                throw new BadRequestException($"Hospital with id {request.HospitalId} doesn't exist");

            var existing = await _hospitalAdministratorRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                                && x.User.LastName == request.LastName
                                                                                && x.User.Phone == request.Phone);

            if (existing != null)
                throw new BadRequestException("HospitalAdministrator with such parameters already exists");

            var createEntity = _mapper.Map<HospitalAdministrator>(request);
            await _hospitalAdministratorRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<HospitalAdministratorDTO>(createEntity);
            return result;
        }

        public async Task<HospitalAdministratorDTO> UpdateAsync(string id, HospitalAdministratorUpdateDTO request)
        {
            var updateEntity = await _hospitalAdministratorRepository.GetAsync(a => a.Id == id, "User");

            if (request == null)
                throw new BadRequestException("The received model of Hospital Administrator is null");

            if (updateEntity == null)
                throw new NotFoundException($"Hospital Administrator with id {id} not found");


            _mapper.Map(request, updateEntity);
            await _hospitalAdministratorRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<HospitalAdministratorDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var hospitalAdministrator = await _hospitalAdministratorRepository.GetAsync(x => x.Id == id);

            if (hospitalAdministrator == null)
                throw new NotFoundException($"Hospital Administrator with such id {id} not found for deletion");

            await _hospitalAdministratorRepository.RemoveAsync(hospitalAdministrator);
        }
    }
}
