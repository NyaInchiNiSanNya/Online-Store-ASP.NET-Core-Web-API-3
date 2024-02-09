using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Models.Requests
{
    public class PatchProductRequest
    {
        public Int32 Id { get; set; }
        public String NewName { get; set; }
        public String? NewDescription { get; set; }
        public Decimal NewPrice { get; set; }

    }
}
