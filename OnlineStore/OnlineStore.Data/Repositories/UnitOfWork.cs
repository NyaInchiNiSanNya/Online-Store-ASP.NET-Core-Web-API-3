using OnlineStore.Data.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Contexts;
using OnlineStore.Data.Interfaces;

namespace OnlineStore.Data.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {

        private readonly ProductsOrdersContext _dbContext;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;


        public UnitOfWork(ProductsOrdersContext dbContext,
            ICategoryRepository categoryRepository,
            IOrderItemRepository orderItemRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _orderItemRepository = orderItemRepository;
            _dbContext = dbContext;
            _productRepository = productRepository;
            _orderRepository = orderRepository;

        }

        public ICategoryRepository Categories => _categoryRepository;
        public IOrderItemRepository OrderItems => _orderItemRepository;
        public IOrderRepository Orders => _orderRepository;
        public IProductRepository Products => _productRepository;

        
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
