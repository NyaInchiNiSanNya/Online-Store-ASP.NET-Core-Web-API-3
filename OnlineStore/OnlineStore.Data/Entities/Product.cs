using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.Models;


namespace OnlineStore.Data.Entities
{
    public class Product : IBaseEntity
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        public Decimal Price { get; set; }
        public ICollection<Сategory>? Categories { get; set; }
    }
}
