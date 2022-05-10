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
            services.AddTransient(typeof(ISearchService<Book>), typeof(SearchService<Book>));
            services.AddTransient(typeof(ISearchService<Author>), typeof(SearchService<Author>));
            services.AddTransient(typeof(ISearchService<Genre>), typeof(SearchService<Genre>));
            services.AddTransient(typeof(ISearchService<Publisher>), typeof(SearchService<Publisher>));
            services.AddTransient(typeof(ISearchService<Shelve>), typeof(SearchService<Shelve>));
            services.AddTransient(typeof(ISearchService<Quote>), typeof(SearchService<Quote>));
            services.AddTransient(typeof(ISearchService<Review>), typeof(SearchService<Review>));
            services.AddTransient(typeof(ISearchService<Country>), typeof(SearchService<Country>));
            services.AddHttpContextAccessor();
        }
    }
}