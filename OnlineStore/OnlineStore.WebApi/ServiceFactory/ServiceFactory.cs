using AutoMapper;
using OnlineStore.BusinessLogic.Interfaces;

namespace OnlineStore.WebApi.ServiceFactory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ??
                               throw new NullReferenceException(nameof(serviceProvider));
        }

        IMapper IServiceFactory.CreateMapperService()
        {
            return _serviceProvider.GetService<IMapper>()
                   ?? throw new NullReferenceException(nameof(IMapper));
        }

        ICategoryService IServiceFactory.CreateCategoryService()
        {
            return _serviceProvider.GetService<ICategoryService>()
                   ?? throw new NullReferenceException(nameof(ICategoryService));
        }

        IProductService IServiceFactory.CreateProductService()
        {
            return _serviceProvider.GetService<IProductService>()
                   ?? throw new NullReferenceException(nameof(IProductService));
        }

        IIdentityService IServiceFactory.CreateIdentityService()
        {
            return _serviceProvider.GetService<IIdentityService>()
                   ?? throw new NullReferenceException(nameof(IIdentityService));
        }

        IJwtService IServiceFactory.CreateJwtService()
        {
            return _serviceProvider.GetService<IJwtService>()
                   ?? throw new NullReferenceException(nameof(IJwtService));
        }
    }
}
