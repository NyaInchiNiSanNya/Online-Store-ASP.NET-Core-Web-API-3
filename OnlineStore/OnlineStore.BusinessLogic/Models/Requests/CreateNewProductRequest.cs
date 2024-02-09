using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Models.Requests
{
    public class CreateNewProductRequest
    {
        public String Name { get; set; }
        public String? Description { get; set; }
        public Decimal Price { get; set; }
        public ICollection<Int32>? CategoriesId { get; set; }
    }
}
