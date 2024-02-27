using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.Data.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public class RoleService:IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public RoleService(IConfiguration configuration, RoleManager<Role> roleManager)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }
        
        public async Task InitiateDefaultRolesAsync(CancellationToken cancellationToken)
        {
            var rolesFromConfig = _configuration["Roles:all"];

            if (String.IsNullOrEmpty(rolesFromConfig))
            {
                throw new ArgumentException("No roles are defined in the configuration file");
            }

            var roles = rolesFromConfig!.Split(" ");

            foreach (var role in roles)
            {
                if (!await _roleManager.Roles
                        .AnyAsync(r => r.Name.Equals(role), cancellationToken: cancellationToken))
                {
                    var newRole = new Role { Name = role };

                    await _roleManager.CreateAsync(newRole);
                }

            }
        }

        public async Task<String> GetDefaultRoleAsync(CancellationToken cancellationToken)
        {
            var defaultRoleFromConfigFile = _configuration["Roles:default"];

            if (String.IsNullOrEmpty(defaultRoleFromConfigFile))
            {
                throw new ArgumentException("No default role is defined in the configuration file");
            }

            var isRoleExist = await _roleManager.Roles.AnyAsync(r => r.Name.Equals(defaultRoleFromConfigFile)
                , cancellationToken: cancellationToken);

            if (!isRoleExist)
            {
                await InitiateDefaultRolesAsync(cancellationToken: cancellationToken);

                isRoleExist = await _roleManager.Roles.AnyAsync(r => r.Name.Equals(defaultRoleFromConfigFile)
                    , cancellationToken: cancellationToken);

            }

            if (!isRoleExist)
            {
                throw new NullReferenceException("Cant create default role");
            }

            return defaultRoleFromConfigFile;
        }
    }
}
