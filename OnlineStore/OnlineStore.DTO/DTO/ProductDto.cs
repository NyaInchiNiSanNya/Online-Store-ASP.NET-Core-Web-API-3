using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO.DTO
{
    public class ProductDto
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        public Decimal Price { get; set; }
        public ICollection<Int32>? CategoriesId { get; set; }

    }
}
