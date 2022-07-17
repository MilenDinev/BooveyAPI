namespace Boovey.Services.Constants
{
    public static class ErrorMessages
    {
        public const string InvalidCredentials = "Invalid credentials, please try again!";
        public const string InvalidPublicationDate = "Please provide valid publication date in format 'dd/MM/yyyy'!";
        public const string EntityDoesNotExist = @"Entity does not exist!";
        public const string EntityIdDoesNotExist = @"Entity with id '{0}' does not exist!";
        public const string EntityAlreadyExists = @"Entity already exists in our system!";
        public const string EntityAlreadyAssignedId = @"Entity with id '{0}' has already been assigned!";
        public const string EntityAlreadyContained = @"Entity already exists in this collection!";
        public const string EntityHasBeenDeleted = @"Entity has already been deleted and cannot be modified!";
        public const string AlreadyFavoriteId = @"Entity with id '{0}' already exists in favorites!";
        public const string NotFavoriteId = @"Entity with id '{0}' does not exists in favorites!";
        public const string AlreadyFollowing = @"{0} '{1}' has already been followed by you!";
        public const string FollowingItSelf = @"Following yourself is forbidden!";
    }
}
