using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Models.Responses
{
    public class GetProductByIdResponse
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        public Decimal Price { get; set; }
    }
}
