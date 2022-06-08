﻿namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Interfaces.IEntities;
    using Services.Interfaces.IHandlers;
    using Data.Entities;
    using Models.Requests.AuthorModels;
    using Models.Responses.AuthorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BooveyBaseController
    {
        private readonly IAuthorService authorService;
        private readonly ISearchService searchService;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public AuthorsController(IAuthorService authorService, 
            ISearchService searchService,
            IValidator validator,
            IMapper mapper, 
            IUserService userService) 
            : base(userService)
        {
            this.authorService = authorService;
            this.searchService = searchService;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<AuthorListingModel>>> Get()
        {
            var allGenres = await this.searchService.GetAllActiveAsync<Author>();
            return mapper.Map<ICollection<AuthorListingModel>>(allGenres).ToList();
        }

        [HttpGet("Get/Author/{authorId}")]
        public async Task<ActionResult<AuthorListingModel>> GetById(int authorId)
        {
            var author = await this.searchService.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            return mapper.Map<AuthorListingModel>(author);
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Create(CreateAuthorModel authorInput)
        {
            await AssignCurrentUserAsync();
            var author = await this.searchService.FindByStringOrDefaultAsync<Author>(authorInput.Fullname);
            await this.validator.ValidateUniqueEntityAsync(author);

            var country = await this.searchService.FindByIdOrDefaultAsync<Country>(authorInput.CountryId);
            await this.validator.ValidateEntityAsync(country, authorInput.CountryId.ToString());

            author = await this.authorService.CreateAsync(authorInput, CurrentUser.Id);
            var createdAuthor = mapper.Map<CreatedAuthorModel>(author);

            return CreatedAtAction(nameof(Get), "Authors", new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("Edit/Author/{authorId}")]
        public async Task<ActionResult<EditedAuthorModel>> Edit(EditAuthorModel authorInput, int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.searchService.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            var country = await this.searchService.FindByIdOrDefaultAsync<Country>(authorInput.CountryId);
            await this.validator.ValidateEntityAsync(country, authorInput.CountryId.ToString());

            await this.authorService.EditAsync(author, authorInput, CurrentUser.Id);

            return mapper.Map<EditedAuthorModel>(author);
        }

        [HttpPut("Favorites/Add/Author/{authorId}")]
        public async Task<AddedFavoriteAuthorModel> AddFavorite(int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.searchService.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            await this.authorService.AddFavoriteAuthorAsync(author, CurrentUser);
            return mapper.Map<AddedFavoriteAuthorModel>(author);
        }

        [HttpPut("Favorites/Remove/Author/{authorId}")]
        public async Task<RemovedFavoriteAuthorModel> RemoveFavorite(int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.searchService.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            await this.authorService.RemoveFavoriteAuthorAsync(author, CurrentUser);
            var removedFavoriteAuthor = mapper.Map<RemovedFavoriteAuthorModel>(author);
            removedFavoriteAuthor.UserId = CurrentUser.Id;
            return removedFavoriteAuthor;
        }

        [HttpDelete("Delete/Author/{authorId}")]
        public async Task<DeletedAuthorModel> Delete(int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.searchService.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            await this.authorService.DeleteAsync(author, CurrentUser.Id);
            return mapper.Map<DeletedAuthorModel>(author);
        }
    }
}
