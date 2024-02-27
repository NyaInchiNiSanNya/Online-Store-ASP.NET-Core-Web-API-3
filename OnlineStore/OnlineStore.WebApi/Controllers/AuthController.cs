using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtService _jwtService;

        public AuthController(IIdentityService identityService, IJwtService jwtService)
        {
            _identityService = identityService ??
                               throw new NullReferenceException(nameof(identityService));
            _jwtService = jwtService ??
                          throw new NullReferenceException(nameof(jwtService));
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationDto registrationDto)
        {
            await _identityService
                        .RegistrationAsync(registrationDto, CancellationToken.None);

            return Ok(NoContent());
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var jwtToken = await _identityService
                    .LoginAndGetJwtTokenAsync(loginDto, CancellationToken.None);

            return Ok(jwtToken);

        }

    }
}


