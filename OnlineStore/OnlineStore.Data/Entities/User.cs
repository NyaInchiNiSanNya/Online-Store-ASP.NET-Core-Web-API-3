using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Entities
{
    public class User : IdentityUser<Int32>, IBaseEntity
    {
        public ICollection<Order> Orders { get; set; }
    }
}
