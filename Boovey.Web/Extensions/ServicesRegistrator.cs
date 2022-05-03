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
            services.AddTransient(typeof(IAssignService<Book>), typeof(AssignService<Book>));
            services.AddTransient(typeof(IAccessorService<Book>), typeof(AccessorService<Book>));
            services.AddTransient(typeof(IAccessorService<Author>), typeof(AccessorService<Author>));
            services.AddTransient(typeof(IAccessorService<Genre>), typeof(AccessorService<Genre>));
            services.AddTransient(typeof(IAccessorService<Publisher>), typeof(AccessorService<Publisher>));
            services.AddTransient(typeof(IAccessorService<Shelve>), typeof(AccessorService<Shelve>));
            services.AddTransient(typeof(IAccessorService<Quote>), typeof(AccessorService<Quote>));
            services.AddTransient(typeof(IAccessorService<Review>), typeof(AccessorService<Review>));
            services.AddTransient(typeof(IAccessorService<Country>), typeof(AccessorService<Country>));
            services.AddHttpContextAccessor();
        }
    }
}