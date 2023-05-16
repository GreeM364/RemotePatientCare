using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using System.Net;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriticalСonditionController : ControllerBase
    {
        private readonly ICriticalСonditionService _criticalСonditionService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public CriticalСonditionController(ICriticalСonditionService criticalСonditionService, IMapper mapper)
        {
            _criticalСonditionService = criticalСonditionService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCriticalConditions()
        {
            try
            {
                var criticalCondition = await _criticalСonditionService.GetAsync();

                _response.Result = _mapper.Map<List<CriticalСonditionViewModel>>(criticalCondition);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

                return _response;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCriticalConditionById(string id)
        {
            try
            {
                var criticalСondition = await _criticalСonditionService.GetByIdAsync(id);

                _response.Result = _mapper.Map<CriticalСonditionViewModel>(criticalСondition);
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

        [HttpGet("{id}/patient")]
        //[Authorize(Roles = CustomRoles.HospitalAdministrator + "," + CustomRoles.CaregiverPatient)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCriticalConditionByPatient(string id)
        {
            try
            {
                var criticalСondition = await _criticalСonditionService.GetCriticalСonditionByPatientAsync(id);

                _response.Result = _mapper.Map<List<CriticalСonditionViewModel>>(criticalСondition);
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
