using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using System.Net;
using System.Security.Claims;

namespace RemotePatientCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                    return Unauthorized();

                var result = await _userService.GetProfileAsync(userId);

                _response.Result = _mapper.Map<ProfileViewModel>(result);
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
        }
    }
}
