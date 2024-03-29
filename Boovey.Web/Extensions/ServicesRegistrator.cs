﻿namespace Boovey.Web.Extensions
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Constants;
    using Services.MainServices;
    using Services.MainServices.Interfaces;
    using Services.Managers;
    using Services.Handlers;
    using Services.Managers.Interfaces;
    using Services.Handlers.Interfaces;

    public static class ServicesRegistrator
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load(AutoMapperConfigValues.Assembly));
            services.AddTransient<IUserManager, BooveyUserManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IShelveService, ShelveService>();
            services.AddTransient<IAssigner, Assigner>();
            services.AddTransient<IFinder, Finder>();
            services.AddTransient<IEntityChecker, EntityChecker>();
            services.AddTransient<IValidator, Validator>();
            services.AddTransient<IFavoritesManager, FavoritesManager>();
            services.AddTransient<IFollowersManager, FollowersManager>();
            services.AddHttpContextAccessor();
        }
    }
}