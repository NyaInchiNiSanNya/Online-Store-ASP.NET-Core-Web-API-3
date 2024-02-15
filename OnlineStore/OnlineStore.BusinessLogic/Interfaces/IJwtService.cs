﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        public Task<String> GetJwtTokenStringAsync(List<Claim> user, CancellationToken cancellationToken);
    }
}
