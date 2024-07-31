using OnlineStore.BusinessLogic.Interfaces;
using System.Security.Claims;
using OnlineStore.DTO.DTO;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Data.Entities;
using OnlineStore.BusinessLogic.Excpetions;
using Microsoft.AspNetCore.Http;

namespace OnlineStore.BusinessLogic.Services
{
    internal class IdentityService : IIdentityService
    {

        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(UserManager<User> userManager, 
            IRoleService roleService, IJwtService jwtService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }


        public async Task RegistrationAsync(UserRegistrationDto model, CancellationToken cancellationToken)
        {
            var isUserExistByEmail = await _userManager.FindByEmailAsync(model.Email);

            if (isUserExistByEmail != null)
            {
                throw new ObjectAlreadyExistException("User with this email already exist.");
            }

            var newUser = new User { Email = model.Email, UserName = model.Name };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, await _roleService
                    .GetDefaultRoleAsync(cancellationToken: cancellationToken));
                
                return;
            }

            var errors = result.Errors.Select(e => e.Description).ToList();

            throw new BadRegistrationException(String.Join(", ", errors));
        }

        private async Task LoginAsync(UserLoginDto userLogin, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user == null )
            {
                throw new ObjectNotFoundException($"User {userLogin.Email} not found");
            }

            if (!await _userManager.CheckPasswordAsync(user, userLogin.Password))
            {
                throw new BadAuthorizeException($"Password verification failed");
            }
        }

        private async Task<List<Claim>> GetUserClaimsAsync(String email, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new List<Claim>();
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            return claims;
        }

        public async Task<string> LoginAndGetJwtTokenAsync(UserLoginDto loginDto, CancellationToken cancellationToken)
        {
            await LoginAsync(loginDto, cancellationToken);

            var claims = await GetUserClaimsAsync(loginDto.Email, CancellationToken.None);

            var jwtToken = await _jwtService
                .GetJwtTokenStringAsync(claims, CancellationToken.None);

            return jwtToken;
        }

        public async Task<int> GetUserIdByNameAsync(CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext?.User.Identity!.Name;
            
            if (String.IsNullOrEmpty(userName))
            {
                throw new UnauthorizedException("User unauthorized");
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new UnauthorizedException("User unauthorized");
            }

            return user.Id;
        }
    }
}
