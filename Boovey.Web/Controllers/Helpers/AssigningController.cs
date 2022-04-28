namespace Boovey.Web.Controllers.Helpers
{
    using System.Threading.Tasks;
    using Base;
    using Data.Entities;
    using Services.Interfaces;

    public class AssigningController : BooveyBaseController
    {
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IPublisherService publisherService;

        protected AssigningController(IUserService userService, IAuthorService authorService, IGenreService genreService, IPublisherService publisherService) : base(userService)
        {
            this.authorService = authorService;
            this.genreService = genreService;
            this.publisherService = publisherService;
        }

        protected async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await this.authorService.GetActiveByIdAsync(authorId);
        }

        protected async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            return  await this.genreService.GetActiveByIdAsync(genreId);
        }

        protected async Task<Publisher> GetPublisherByIdAsync(int publisherId)
        {
            return await this.publisherService.GetActiveByIdAsync(publisherId);
        }

    }
}
