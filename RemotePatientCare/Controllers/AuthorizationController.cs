using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using System.Net;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public AuthorizationController(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _response = new APIResponse();
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestViewModel request)
        {
            try
            {
                var resultDTO = _mapper.Map<LoginRequestDTO>(request);
                var result = await _loginService.LoginAsync(resultDTO);

                _response.Result = _mapper.Map<LoginResultViewModel>(result);

                if (result.IsSuccess)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.Unauthorized;
                    return Unauthorized(result);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

                return _response;
            }
        }
    }
}
