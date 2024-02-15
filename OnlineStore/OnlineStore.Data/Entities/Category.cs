using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineStore.Data.Entities
{
    public class Сategory : IBaseEntity
    {
        
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
