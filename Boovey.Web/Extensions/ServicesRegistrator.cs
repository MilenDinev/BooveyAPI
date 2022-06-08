namespace Boovey.Web.Extensions
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Constants;
    using Services;
    using Services.Managers;
    using Services.Interfaces.IEntities;
    using Services.Interfaces.IManagers;
    using Services.Interfaces.IHandlers;
    using Services.Handlers;

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
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IEntityChecker, EntityChecker>();
            services.AddTransient<IValidator, Validator>();
            services.AddHttpContextAccessor();
        }
    }
}