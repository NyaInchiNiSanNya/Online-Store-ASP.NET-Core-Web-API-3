using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        
        public async Task InitiateDefaultRolesAsync()
        {
            String? rolesFromConfig = _configuration["Roles:all"];

            if (String.IsNullOrEmpty(rolesFromConfig))
            {
                throw new ArgumentException("No roles are defined in the configuration file");
            }

            var roles = rolesFromConfig!.Split(" ");

            foreach (var role in roles)
            {
                if (!await _roleManager.Roles.AnyAsync(r => r.Name.Equals(role)))
                {
                    var newRole = new Role { Name = role };

                    await _roleManager.CreateAsync(newRole);
                }

            }
        }

        public async Task<String> GetDefaultRoleAsync()
        {
            String? defaultRoleFromConfigFile = _configuration["Roles:default"];

            if (String.IsNullOrEmpty(defaultRoleFromConfigFile))
            {
                throw new ArgumentException("No default role is defined in the configuration file");
            }

            var isRoleExist = await _roleManager.Roles.AnyAsync(r => r.Name.Equals(defaultRoleFromConfigFile));

            if (!isRoleExist)
            {
                await InitiateDefaultRolesAsync();

                isRoleExist = await _roleManager.Roles.AnyAsync(r => r.Name.Equals(defaultRoleFromConfigFile));

            }

            if (!isRoleExist)
            {
                throw new NullReferenceException("Cant create default role");
            }

            return defaultRoleFromConfigFile;
        }
    }
}
