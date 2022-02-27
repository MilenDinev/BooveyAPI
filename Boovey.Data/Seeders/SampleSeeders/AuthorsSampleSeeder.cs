namespace Boovey.Data.Seeders.SampleSeeders
{
    using System;
    using Entities.Books;
    using System.Threading.Tasks;
    public static class AuthorsSampleSeeder
    {
        public static async Task Seed(BooveyDbContext context)
        {
            var author1 = new Author
            {
                Id = 1,
                Fullname = "J. R. R. Tolkien",
                Summary = @"J.R.R. Tolkien was born on 3rd January 1892. After serving in the First World War, he became best known for The Hobbit and The Lord of the Rings, 
                        selling 150 million copies in more than 40 languages worldwide. Awarded the CBE and an honorary Doctorate of Letters from Oxford University, he died in 1973 at the age of 81.",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author2 = new Author
            {
                Id = 2,
                Fullname = "Ayn Rand",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author3 = new Author
            {
                Id = 3,
                Fullname = "Chuck Palahniuk",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author4 = new Author
            {
                Id = 4,
                Fullname = "Ashlee Vance",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author5 = new Author
            {
                Id = 5,
                Fullname = "Walter Isaacson",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author6 = new Author
            {
                Id = 6,
                Fullname = "Fyodor Dostoyevsky",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author7 = new Author
            {
                Id = 7,
                Fullname = "William Shakespeare",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author8 = new Author
            {
                Id = 8,
                Fullname = "Leo Tolstoy",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author9 = new Author
            {
                Id = 9,
                Fullname = "Charles Dickens",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author10 = new Author
            {
                Id = 10,
                Fullname = "George Orwell",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author11 = new Author
            {
                Id = 11,
                Fullname = "Mark Twain",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author12 = new Author
            {
                Id = 12,
                Fullname = "Edgar Allan Poe",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author13 = new Author
            {
                Id = 13,
                Fullname = "Victor Hugo",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author14 = new Author
            {
                Id = 14,
                Fullname = "Plato",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author15 = new Author
            {
                Id = 15,
                Fullname = "Homer",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author16 = new Author
            {
                Id = 16,
                Fullname = "Oscar Wilde",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author17 = new Author
            {
                Id = 17,
                Fullname = "Ernest Hemingway",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author18 = new Author
            {
                Id = 18,
                Fullname = "Jane Austen",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author19 = new Author
            {
                Id = 19,
                Fullname = "Miguel de Cervantes",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };            
            
            var author20 = new Author
            {
                Id = 20,
                Fullname = "Arthur Conan Doyle",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };            
            
            var author21 = new Author
            {
                Id = 20,
                Fullname = "Tom Shippey",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            await context.Authors.AddRangeAsync(author1, author2, author3, author4, author5, author6, author7, author8, author9, author10, author11, author12, author13, author14,
    author15, author16, author17, author18, author19, author20, author21);

            await context.SaveChangesAsync();
        }
    }
}
