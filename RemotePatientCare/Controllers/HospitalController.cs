using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.API.Models;
using System.Net;
using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public HospitalController(IHospitalService hospitalService, IMapper mapper)
        {
            _hospitalService = hospitalService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHospitals()
        {
            try
            {
                var hospital = await _hospitalService.GetAsync();

                _response.Result = _mapper.Map<List<HospitalViewModel>>(hospital);
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
        public async Task<ActionResult<APIResponse>> GetHospitalById(string id)
        {
            try
            {
                var hospital = await _hospitalService.GetByIdAsync(id);

                _response.Result = _mapper.Map<HospitalViewModel>(hospital);
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
        public async Task<ActionResult<APIResponse>> Post([FromBody] HospitalCreateViewModel request)
        {
            try
            {
                var hospitalDTO = _mapper.Map<HospitalCreateDTO>(request);
                var hospital = await _hospitalService.CreateAsync(hospitalDTO);

                _response.Result = _mapper.Map<HospitalViewModel>(hospital);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Put(string id, [FromBody] HospitalUpdateViewModel request)
        {
            try
            {
                var hospitalDTO = _mapper.Map<HospitalUpdateDTO>(request);
                var hospital = await _hospitalService.UpdateAsync(id, hospitalDTO);

                _response.Result = _mapper.Map<HospitalViewModel>(hospital);
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
                await _hospitalService.DeleteAsync(id); 

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

        [HttpGet("{id}/doctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHospitalDoctors(string id)
        {
            try
            {
                var doctors = await _hospitalService.GetDoctorsAsync(id);

                _response.Result = _mapper.Map<List<DoctorViewModel>>(doctors);
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

        [HttpGet("{id}/patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHospitalPatients(string id)
        {
            try
            {
                var doctors = await _hospitalService.GetPatientsAsync(id);

                _response.Result = _mapper.Map<List<PatientViewModel>>(doctors);
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
    }
}
