﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Entities
{
    public class Order : IBaseEntity
    {
        public Int32 Id { get; set; }
        public String UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
