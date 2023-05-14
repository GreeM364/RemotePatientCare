using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.Utility;
using System.Net;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalConditionController : ControllerBase
    {
        private readonly IPhysicalConditionService _physicalConditionService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public PhysicalConditionController(IPhysicalConditionService physicalConditionService, IMapper mapper)
        {
            _physicalConditionService = physicalConditionService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPhysicalConditions()
        {
            try
            {
                var physicalCondition = await _physicalConditionService.GetAsync();

                _response.Result = _mapper.Map<List<PhysicalConditionViewModel>>(physicalCondition);
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
        public async Task<ActionResult<APIResponse>> GetPhysicalConditionById(string id)
        {
            try
            {
                var physicalCondition = await _physicalConditionService.GetByIdAsync(id);

                _response.Result = _mapper.Map<PhysicalConditionViewModel>(physicalCondition);
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
        public async Task<ActionResult<APIResponse>> GetPhysicalConditionByPatient(string id)
        {
            try
            {
                var _physicalCondition = await _physicalConditionService.GetPhysicalConditionByPatientAsync(id);

                _response.Result = _mapper.Map<List<PhysicalConditionViewModel>>(_physicalCondition);
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
