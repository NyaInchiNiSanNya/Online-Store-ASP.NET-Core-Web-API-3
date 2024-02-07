using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Entities
{
    public class OrderItem : IBaseEntity
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 Count { get; set; }
        public Decimal TotalPrice { get; set; }
        public Product Product { get; set; }
        public Int32 OrderId { get; set; }
        public Order Order { get; set; }
    }
}
