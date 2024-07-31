using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO.DTO
{
    public class CategoriesDto
    {
        public List<CategoryDto>? Categories { get; set; }
        public int TotalCategoriesCount { get; set; }
    }
}
