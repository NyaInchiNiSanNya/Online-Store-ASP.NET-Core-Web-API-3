using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO.DTO
{
    public class ProductsDto
    {
        public List<ProductDto>? Products { get; set; }
        public int TotalProductsCount { get; set; }
    }
}
