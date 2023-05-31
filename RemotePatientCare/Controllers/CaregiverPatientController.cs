using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.Utility;
using System.Net;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaregiverPatientController : ControllerBase
    {
        private readonly ICaregiverPatientService _caregiverPatientService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public CaregiverPatientController(ICaregiverPatientService caregiverPatientService, IMapper mapper)
        {
            _caregiverPatientService = caregiverPatientService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [Authorize(Roles = CustomRoles.HospitalAdministrator + "," + CustomRoles.GlobalAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetCaregiverPatients()
        {
            try
            {
                var caregiverPatient = await _caregiverPatientService.GetAsync();

                _response.Result = _mapper.Map<List<CaregiverPatientViewModel>>(caregiverPatient);
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
        [Authorize(Roles = CustomRoles.HospitalAdministrator + "," + CustomRoles.Doctor + "," + CustomRoles.Patient + "," + CustomRoles.CaregiverPatient)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCaregiverPatientById(string id)
        {
            try
            {
                var caregiverPatient = await _caregiverPatientService.GetByIdAsync(id);

                _response.Result = _mapper.Map<CaregiverPatientViewModel>(caregiverPatient);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (NotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.Message };

                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

                return _response;
            }
        }

        [HttpPost]
        [Authorize(Roles = CustomRoles.HospitalAdministrator)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> Post([FromBody] CaregiverPatientCreateViewModel request)
        {
            try
            {
                var caregiverPatientDTO = _mapper.Map<CaregiverPatientCreateDTO>(request);
                var caregiverPatient = await _caregiverPatientService.CreateAsync(caregiverPatientDTO);

                _response.Result = _mapper.Map<CaregiverPatientViewModel>(caregiverPatient);
                _response.StatusCode = HttpStatusCode.Created;

                return StatusCode(StatusCodes.Status201Created, _response);

            }
            catch (BadRequestException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = CustomRoles.HospitalAdministrator + "," + CustomRoles.CaregiverPatient)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Put(string id, [FromBody] CaregiverPatientUpdateViewModel request)
        {
            try
            {
                var caregiverPatientDTO = _mapper.Map<CaregiverPatientUpdateDTO>(request);
                var caregiverPatient = await _caregiverPatientService.UpdateAsync(id, caregiverPatientDTO);

                _response.Result = _mapper.Map<CaregiverPatientViewModel>(caregiverPatient);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (BadRequestException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return BadRequest(_response);
            }
            catch (NotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.Message };

                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = CustomRoles.HospitalAdministrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(string id)
        {
            try
            {
                await _caregiverPatientService.DeleteAsync(id);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (NotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.Message };

                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpGet("{id}/patients")]
        [Authorize(Roles = CustomRoles.CaregiverPatient)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPatients(string id)
        {
            try
            {
                var doctors = await _caregiverPatientService.GetPatientsAsync(id);

                _response.Result = _mapper.Map<List<PatientViewModel>>(doctors);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (NotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.Message };

                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpPatch("{id}/add-patient")]
        [Authorize(Roles = CustomRoles.CaregiverPatient)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> AddPatientToCaregiver(string id, [FromBody] AddPatientToCaregiverViewModel request)
        {
            try
            {
                var patientToCaregiverDTO = _mapper.Map<AddPatientToCaregiverDTO>(request);
                await _caregiverPatientService.AddPatientToCaregiverAsync(id, patientToCaregiverDTO);

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (NotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.Message };

                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpPatch("{patientId}/delete-patient")]
        [Authorize(Roles = CustomRoles.CaregiverPatient)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeletePatientToDoctorAsync(string patientId)
        {
            try
            {
                await _caregiverPatientService.DeletePatientToCaregiverAsync(patientId);

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (NotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.Message };

                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }
    }
}
