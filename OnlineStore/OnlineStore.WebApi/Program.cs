using OnlineStore.BusinessLogic.Extensions;
using OnlineStore.Data.Extensions;
using OnlineStore.WebApi.Filters.Errors;
using OnlineStore.WebApi.ServiceFactory;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace OnlineStore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilterAttribute>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.JwtConfiguration();
            builder.Services.AddDbConfiguration(builder.Configuration);
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddScoped<IServiceFactory, ServiceFactory.ServiceFactory>();
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}