using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO.Models
{
    public class Patch
    {
        public string PropertyName { get; set; }
        public object? PropertyValue { get; set; }
    }
}
