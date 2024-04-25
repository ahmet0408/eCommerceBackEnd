using eCommerce.bll.Services.ImageService;
using eCommerce.bll.Services.LanguageService;
using eCommerce.bll.Services.NotificationService;
using eCommerce.bll.Services.OrderService;
using eCommerce.bll.Services.ProductService;
using eCommerce.bll.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
