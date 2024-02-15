using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Entities;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IIdentityService
    {
        public Task<Boolean> RegistrationAsync
            (UserRegistrationDto model, CancellationToken cancellationToken);

        public Task LoginAsync(UserLoginDto model, CancellationToken cancellationToken);

        public Task<List<Claim>> GetUserClaimsAsync(String email, CancellationToken cancellationToken);
    }
}
