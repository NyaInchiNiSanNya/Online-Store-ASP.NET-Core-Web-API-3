using AutoMapper;
using OnlineStore.BusinessLogic.Interfaces;

namespace OnlineStore.WebApi.ServiceFactory
{
    public interface IServiceFactory
    {
        IMapper CreateMapperService();
        ICategoryService CreateCategoryService();
        IProductService CreateProductService();
        IIdentityService CreateIdentityService();
        IJwtService CreateJwtService();
    }
}
