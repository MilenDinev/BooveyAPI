﻿namespace Boovey.Web.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Services.Managers;
    using Services.Interfaces;

    public static class ServicesRegistrator
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserManager, BooveyUserManager>();
        }
    }
}