namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        public Task InitiateDefaultRolesAsync(CancellationToken cancellationToken);

        public Task<string> GetDefaultRoleAsync(CancellationToken cancellationToken);
    }
}
