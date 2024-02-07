using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Models.Responses
{
    public class GetCategoryByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
