using System.Security.Claims;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        public Task<string> GetJwtTokenStringAsync(List<Claim> user, CancellationToken cancellationToken);
    }
}
