﻿using System.Security.Claims;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IIdentityService
    {
        public Task RegistrationAsync
            (UserRegistrationDto model, CancellationToken cancellationToken);

        public Task LoginAsync(UserLoginDto model, CancellationToken cancellationToken);

        public Task<List<Claim>> GetUserClaimsAsync(string email, CancellationToken cancellationToken);

        public Task<String> LoginAndGetJwtTokenAsync(UserLoginDto model, CancellationToken cancellationToken);
    }
}
