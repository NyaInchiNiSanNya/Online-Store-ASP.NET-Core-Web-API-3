using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
