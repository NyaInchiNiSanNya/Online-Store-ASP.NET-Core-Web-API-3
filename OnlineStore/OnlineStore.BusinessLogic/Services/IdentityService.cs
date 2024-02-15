using AutoMapper;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.DTO;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Data.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace OnlineStore.BusinessLogic.Services
{
    internal class IdentityService : IIdentityService
    {

        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;

        public IdentityService(UserManager<User> userManager, IRoleService roleService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }


        public async Task<Boolean> RegistrationAsync(UserRegistrationDto model, CancellationToken cancellationToken)
        {
            var isUserExistByEmail = await _userManager.FindByEmailAsync(model.Email);

            if (isUserExistByEmail != null)
            {
                throw new InvalidOperationException("User with this email already exist.");
            }

            var newUser = new User { Email = model.Email, UserName = model.Name };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, await _roleService
                    .GetDefaultRoleAsync(cancellationToken: cancellationToken));
                
                return true;
            }

            var errors = result.Errors.Select(e => e.Description).ToList();

            throw new InvalidOperationException(String.Join(", ", errors));
        }

        public async Task LoginAsync(UserLoginDto userLogin, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user != null )
            {
                throw new InvalidOperationException($"User {userLogin.Email} not found");
            }

            if (await _userManager.CheckPasswordAsync(user, userLogin.Password))
            {
                throw new InvalidOperationException($"password verification failed: {userLogin.Email}");
            }
        }

        public async Task<List<Claim>> GetUserClaimsAsync(String email, CancellationToken cancellationToken)
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
    }
}
