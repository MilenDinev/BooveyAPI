namespace Boovey.Web.Extensions
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Constants;
    using Services;
    using Services.Managers;
    using Services.Interfaces;
    using Services.Interfaces.IHandlers;
    using Services.Handlers;
    using Data.Entities;

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
            services.AddTransient<ICountryManager, CountryManager>();
            services.AddTransient(typeof(IAssigningService<Book>), typeof(AssigningService<Book>));
            services.AddTransient(typeof(IContextAccessorServices<Book>), typeof(ContextAccessorService<Book>));
            services.AddTransient(typeof(IContextAccessorServices<Author>), typeof(ContextAccessorService<Author>));
            services.AddTransient(typeof(IContextAccessorServices<Genre>), typeof(ContextAccessorService<Genre>));
            services.AddTransient(typeof(IContextAccessorServices<Publisher>), typeof(ContextAccessorService<Publisher>));
            services.AddHttpContextAccessor();
        }
    }
}