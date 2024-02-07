﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Contexts;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Interfaces;

namespace OnlineStore.Data.Repositories.Implementations
{
    internal class CategoryRepository : Repository<Сategory>, ICategoryRepository
    {
        public CategoryRepository(ProductsOrdersContext newsAggregatorContext)
            : base(newsAggregatorContext)
        {
        }

    }
}
