using Microsoft.AspNetCore.Identity;
using OnlineStore.DTO.Models;


namespace OnlineStore.Data.Entities
{
    public class Role : IdentityRole<int>, IBaseEntity
    {
    }
}
