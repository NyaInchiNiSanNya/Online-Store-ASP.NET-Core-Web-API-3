using Microsoft.AspNetCore.Identity;
using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Entities
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public ICollection<Order> Orders { get; set; }
    }
}
