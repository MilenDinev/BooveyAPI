namespace Boovey.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.AuthorModels;
    using Models.Responses.AuthorModels;

    public class AuthorService : IAuthorService
    {
        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;
        public AuthorService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AddedAuthorModel> AddAsync(AddAuthorModel authorModel, int currentUserId)
        {
            await AuthorDuplicationChecker(authorModel.Fullname);
            await CountryExistenceChecker(authorModel.CountryId);

            var author = mapper.Map<Author>(authorModel);

            author.CreatorId = currentUserId;
            author.LastModifierId = currentUserId;

            await this.dbContext.Authors.AddAsync(author);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedAuthorModel>(author);
        }

        public async Task<EditedAuthorModel> EditAsync(int authorId, EditAuthorModel authorModel, int currentUserId)
        {
            var author = await FindAuthorById(authorId);

            await CountryExistenceChecker(authorModel.CountryId);

            author.CountryId = authorModel.CountryId;
            author.Fullname = authorModel.Fullname;
            author.Summary = authorModel.Summary;
            author.LastModifierId = currentUserId;
            author.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedAuthorModel>(author);
        }

        public async Task<AddedFavoriteAuthorModel> AddFavoriteAuthor(int authorId, User currentUser)
        {
            var author = await FindAuthorById(authorId);

            var isFavorite = await IsFavoriteAuthor(authorId, currentUser)
                ? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.NotFavoriteId, nameof(Author), authorId))
                : false;

            currentUser.FavoriteAuthors.Add(author) ;

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteAuthorModel>(author);
        }

        public async Task<RemovedFavoriteAuthorModel> RemoveFavoriteAuthor(int authorId, User currentUser)
        {
            var author = await FindAuthorById(authorId);

            var isFavorite = await IsFavoriteAuthor(authorId, currentUser)
                ? currentUser.FavoriteAuthors.Remove(author) 
                : throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Author), authorId));

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteAuthorModel>(author);
        }

        public async Task<ICollection<AuthorListingModel>> GetAllAuthorsAsync()
        {
            var authors = await this.dbContext.Authors.ToListAsync();

            return mapper.Map<ICollection<AuthorListingModel>>(authors);
        }

        public async Task<AuthorListingModel> GetAuthorById(int authorId)
        {
            return mapper.Map<AuthorListingModel>(await FindAuthorById(authorId));
        }

        private async Task<Author> FindAuthorById(int authorId)
        {
            var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Author), authorId));

            return author;
        }

        private async Task AuthorDuplicationChecker(string authorFullName)
        {
            var IsAuthorExists = await this.dbContext.Authors.AnyAsync(a => a.Fullname == authorFullName)
                ? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Author), authorFullName))
                : false;
        }

        private async Task<bool> IsFavoriteAuthor(int authorId, User user)
        {
            return await Task.Run(() => user.FavoriteAuthors.Any(a => a.Id == authorId));
        }

        private async Task CountryExistenceChecker(int countryId)
        {
            var isCountryExists = await this.dbContext.Countries.AnyAsync(c => c.Id == countryId)
                ? true 
                : throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), countryId));
        }
    }
}

