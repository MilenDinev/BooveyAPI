namespace Boovey.Services.Constants
{
    public static class ErrorMessages
    {
        public const string InvalidCredentials = "Invalid credentials, please try again!";
        public const string InvalidPublicationDate = "Please provide valid publication date in format 'dd/MM/yyyy'!";
        public const string EntityDoesNotExist = @"{0} '{1}' does not exist!";
        public const string EntityIdDoesNotExist = @"{0} with id '{1}' does not exist!";
        public const string EntityAlreadyExists = @"{0} '{1}' already exists in our system!";
        public const string EntityAlreadyAssignedId = @"{0} with id '{1}' has already been assigned to {2} with id '{3}'!";
        public const string EntityAlreadyContained = @"{0} has already exists in current collection!";
        public const string EntityHasBeenDeleted = @"{0} has already been deleted and cannot be modified!";
        public const string AlreadyFavoriteId = @"{0} with id '{1}' already exists in favorites!";
        public const string NotFavoriteId = @"{0} with id '{1}' does not exists in favorites!";
        public const string AlreadyFollowing = @"{0} '{1}' has already been followed by you!";
        public const string FollowingItSelf = @"Following yourself is forbidden!";
    }
}
