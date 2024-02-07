using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineStore.DTO.Models;


namespace OnlineStore.Data.Entities
{
    public class Role : IdentityRole<Int32>, IBaseEntity
    {
    }
}
