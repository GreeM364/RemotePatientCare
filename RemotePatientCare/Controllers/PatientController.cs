using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using System.Net;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPatients()
        {
            try
            {
                var doctor = await _patientService.GetAsync();

                _response.Result = _mapper.Map<List<PatientViewModel>>(doctor);
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
        public async Task<ActionResult<APIResponse>> GetPatientById(string id)
        {
            try
            {
                var doctor = await _patientService.GetByIdAsync(id);

                _response.Result = _mapper.Map<PatientViewModel>(doctor);
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
        public async Task<ActionResult<APIResponse>> Post([FromBody] PatientCreateViewModel request)
        {
            try
            {
                var patientDTO = _mapper.Map<PatientCreateDTO>(request);
                var patient = await _patientService.CreateAsync(patientDTO);

                _response.Result = _mapper.Map<PatientViewModel>(patient);
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
        public async Task<ActionResult<APIResponse>> Put(string id, [FromBody] PatientUpdateViewModel request)
        {
            try
            {
                var patientDTO = _mapper.Map<PatientUpdateDTO>(request);
                var patient = await _patientService.UpdateAsync(id, patientDTO);

                _response.Result = _mapper.Map<PatientViewModel>(patient);
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
                await _patientService.DeleteAsync(id);

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

        [HttpGet("{id}/caretaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPatientCaretaker(string id)
        {
            try
            {
                var patientCaretaker = await _patientService.GetPatientCaretakerAsync(id);

                _response.Result = _mapper.Map<PatientViewModel>(patientCaretaker);
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
