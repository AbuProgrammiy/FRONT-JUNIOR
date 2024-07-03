using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.Services.EmailServices;
using FrontJunior.Application.Services.PasswordServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FrontJunior.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ISendEmailService, SendEmailService>();

            services.AddScoped<IAuthService,AuthService>();

            services.AddScoped<IPasswordService,PasswordService>();

            return services;
        }
    }
}
