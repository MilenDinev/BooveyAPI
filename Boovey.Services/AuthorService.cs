namespace Boovey.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Constants;
    using Interfaces;
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
            var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.Fullname == authorModel.Fullname);
            if (author != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Author), authorModel.Fullname));

            author = mapper.Map<Author>(authorModel);

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Id == authorModel.CountryId)
                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), authorModel.CountryId));
            author.Country = country;

            author.CreatorId = currentUserId;
            author.LastModifierId = currentUserId;

            await this.dbContext.Authors.AddAsync(author);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedAuthorModel>(author);
        }

        public async Task<EditedAuthorModel> EditAsync(int authorId, EditAuthorModel authorModel, int currentUserId)
        {
            var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId)
               ?? throw new KeyNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Author), authorId));

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Id == authorModel.CountryId)
                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), authorModel.CountryId));
            author.Country = country;

            author.Fullname = authorModel.Fullname;
            author.Summary = authorModel.Summary;
            author.LastModifierId = currentUserId;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedAuthorModel>(author);
        }

        public async Task<AddedFavoriteAuthorModel> AddFavoriteAuthor(int authorId, User currentUser)
        {
            var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId)
                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Author), authorId));

            var isAlreadyFavoriteAuthor = currentUser.FavoriteAuthors.FirstOrDefault(a => a.Id == authorId);

            if (isAlreadyFavoriteAuthor != null)
                throw new ArgumentException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Author), author.Fullname));

            currentUser.FavoriteAuthors.Add(author);

            foreach (var genre in author.Genres)
            {
                var isAlreadyFavoriteGenre = currentUser.FavoriteGenres.FirstOrDefault(g => g.Id == genre.Id);
                if (isAlreadyFavoriteGenre == null)
                {
                    currentUser.FavoriteGenres.Add(genre);
                }
            }

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteAuthorModel>(author);
        }

        public async Task<RemovedFavoriteAuthorModel> RemoveFavoriteAuthor(int authorId, User currentUser)
        {
            var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId)
                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Author), authorId));

            var isFavoriteAuthor = currentUser.FavoriteAuthors.FirstOrDefault(a => a.Id == authorId);

            if (isFavoriteAuthor == null)
                throw new KeyNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Author), author.Fullname));

            currentUser.FavoriteAuthors.Remove(author);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteAuthorModel>(author);
        }

        public async Task<ICollection<AuthorsListingModel>> GetAllAuthorsAsync()
        {
            var authors = await this.dbContext.Authors.ToListAsync();

            return mapper.Map<ICollection<AuthorsListingModel>>(authors);
        }

    }
}

