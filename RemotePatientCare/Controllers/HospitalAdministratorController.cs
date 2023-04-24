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
    public class HospitalAdministratorController : ControllerBase
    {
        private readonly IHospitalAdministratorService _hospitalAdministrator;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public HospitalAdministratorController(IHospitalAdministratorService hospitalAdministrator, IMapper mapper)
        {
            _hospitalAdministrator = hospitalAdministrator;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [Authorize(Roles = CustomRoles.GlobalAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetHospitalAdministrators()
        {
            try
            {
                var doctor = await _hospitalAdministrator.GetAsync();

                _response.Result = _mapper.Map<List<HospitalAdministratorViewModel>>(doctor);
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
        [Authorize(Roles = CustomRoles.GlobalAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHospitalAdministratorById(string id)
        {
            try
            {
                var doctor = await _hospitalAdministrator.GetByIdAsync(id);

                _response.Result = _mapper.Map<HospitalAdministratorViewModel>(doctor);
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
        [Authorize(Roles = CustomRoles.GlobalAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> Post([FromBody] HospitalAdministratorCreateViewModel request)
        {
            try
            {
                var hospitalAdministratorDTO = _mapper.Map<HospitalAdministratorCreateDTO>(request);
                var hospitalAdministrator = await _hospitalAdministrator.CreateAsync(hospitalAdministratorDTO);

                _response.Result = _mapper.Map<HospitalAdministratorViewModel>(hospitalAdministrator);
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
        [Authorize(Roles = CustomRoles.HospitalAdministrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Put(string id, [FromBody] HospitalAdministratorUpdateViewModel request)
        {
            try
            {
                var hospitalAdministratorDTO = _mapper.Map<HospitalAdministratorUpdateDTO>(request);
                var hospitalAdministrator = await _hospitalAdministrator.UpdateAsync(id, hospitalAdministratorDTO);

                _response.Result = _mapper.Map<HospitalAdministratorViewModel>(hospitalAdministrator);
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
        [Authorize(Roles = CustomRoles.GlobalAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(string id)
        {
            try
            {
                await _hospitalAdministrator.DeleteAsync(id);

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
    }
}
