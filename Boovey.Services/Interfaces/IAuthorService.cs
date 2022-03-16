namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data.Entities;
    using Models.Requests.AuthorModels;
    using Models.Responses.AuthorModels;

    public interface IAuthorService
    {
        Task<AddedAuthorModel> AddAsync(AddAuthorModel authorModel, int currentUserId);
        Task<EditedAuthorModel> EditAsync(int authorId, EditAuthorModel authorModel, int currentUserId);
        Task<AddedFavoriteAuthorModel> AddFavoriteAuthor(int authorId, User currentUser);
        Task<RemovedFavoriteAuthorModel> RemoveFavoriteAuthor(int authorId, User currentUser);
        Task<AuthorListingModel> GetAuthorById(int authorId);
        Task<ICollection<AuthorListingModel>> GetAllAuthorsAsync();
    }
}
