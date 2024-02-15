using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        public Task InitiateDefaultRolesAsync(CancellationToken cancellationToken);

        public Task<String> GetDefaultRoleAsync(CancellationToken cancellationToken);
    }
}
