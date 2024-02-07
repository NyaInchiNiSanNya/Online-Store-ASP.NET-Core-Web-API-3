using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Models.Requests
{
    public class PatchCategoryRequest
    {
        public int Id { get; set; }
        public string newName { get; set; }

    }
}
