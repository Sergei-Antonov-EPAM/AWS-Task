using BLL;
using BLL.Interfaces;
using DAL.DynamoDB;
using DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DI
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IBookLogic, BookLogic>();
            services.AddTransient<IBookDAO, BookDAO>();
        }
    }

}
