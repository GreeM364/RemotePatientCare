﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services;
using RemotePatientCare.BLL.Services.Interfaces;
using System.Net;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetDoctors()
        {
            try
            {
                var doctor = await _doctorService.GetAsync();

                _response.Result = _mapper.Map<List<DoctorViewModel>>(doctor);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDoctorById(string id)
        {
            try
            {
                var doctor = await _doctorService.GetByIdAsync(id);

                _response.Result = _mapper.Map<DoctorViewModel>(doctor);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return NotFound(_response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Post([FromBody] DoctorCreateViewModel request)
        {
            try
            {
                var doctorDTO = _mapper.Map<DoctorCreateDTO>(request);
                var doctor = await _doctorService.CreateAsync(doctorDTO);

                _response.Result = _mapper.Map<DoctorViewModel>(doctor);
                _response.StatusCode = HttpStatusCode.Created;

                return StatusCode(StatusCodes.Status201Created, _response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return BadRequest(_response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Put(string id, [FromBody] DoctorUpdateViewModel request)
        {
            try
            {
                var doctorDTO = _mapper.Map<DoctorUpdateDTO>(request);
                var hospital = await _doctorService.UpdateAsync(id, doctorDTO);

                _response.Result = _mapper.Map<DoctorUpdateViewModel>(hospital);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(string id)
        {
            try
            {
                await _doctorService.DeleteAsync(id);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return NotFound(_response);
            }
        }
    }
}