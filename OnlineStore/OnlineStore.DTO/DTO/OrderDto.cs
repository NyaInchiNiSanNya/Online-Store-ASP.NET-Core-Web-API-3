using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public IList<OrderItemDto> OrderItemsDto { get; set; }
    }
}
