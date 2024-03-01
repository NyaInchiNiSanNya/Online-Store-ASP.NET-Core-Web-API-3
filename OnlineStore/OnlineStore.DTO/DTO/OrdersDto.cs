using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO.DTO
{
    public class OrdersDto
    {
        public List<OrderDto>? Orders { get; set; }
        public int TotalOrdersCount { get; set; }
    }
}
