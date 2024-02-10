using Microsoft.AspNetCore.Mvc;
using OnlineStore.WebApi.ServiceFactory;
using System.ComponentModel.DataAnnotations;
using OnlineStore.BusinessLogic.Models.Requests;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;

        public AuthController(
            IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new NullReferenceException(nameof(serviceFactory));
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest registrationRequest)
        {
            //valid

            try
            {
                await _serviceFactory
                        .CreateIdentityService()
                        .RegistrationAsync(_serviceFactory
                            .CreateMapperService()
                            .Map<UserRegistrationDto>(registrationRequest)!);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            if (await _serviceFactory
                    .CreateIdentityService()
                    .LoginAsync(_serviceFactory
                        .CreateMapperService()
                        .Map<UserLoginDto>(loginRequest)))
            {

                var claims = await _serviceFactory.CreateIdentityService()
                    .GetUserClaimsAsync(loginRequest.Email);

                var jwtToken = await _serviceFactory.CreateJwtService().GetJwtTokenString(claims);
                
                return Ok(jwtToken);

            }
            else
            {
                return BadRequest("Invalid email or password");
            }

        }

    }
}


