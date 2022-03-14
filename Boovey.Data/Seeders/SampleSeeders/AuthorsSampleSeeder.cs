namespace Boovey.Data.Seeders.SampleSeeders
{
    using System;
    using Entities;
    using System.Threading.Tasks;

    public static class AuthorsSampleSeeder
    {
        public static async Task Seed(BooveyDbContext context)
        {
            var author1 = new Author
            {
                Fullname = "J. R. R. Tolkien",
                Summary = @"J.R.R. Tolkien was born on 3rd January 1892. After serving in the First World War, he became best known for The Hobbit and The Lord of the Rings, 
                        selling 150 million copies in more than 40 languages worldwide. Awarded the CBE and an honorary Doctorate of Letters from Oxford University, he died in 1973 at the age of 81.",
                CountryId = 147,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author2 = new Author
            {
                Fullname = "Ayn Rand",
                CountryId = 144,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author3 = new Author
            {
                Fullname = "Chuck Palahniuk",
                CountryId = 144,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author4 = new Author
            {
                Fullname = "Ashlee Vance",
                CountryId = 144,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author5 = new Author
            {
                Fullname = "Walter Isaacson",
                CountryId = 144,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author6 = new Author
            {
                Fullname = "Fyodor Dostoyevsky",
                CountryId = 131,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author7 = new Author
            {
                Fullname = "William Shakespeare",
                CountryId = 147,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author8 = new Author
            {
                Fullname = "Leo Tolstoy",
                CountryId = 131,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author9 = new Author
            {
                Fullname = "Charles Dickens",
                CountryId = 147,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author10 = new Author
            {
                Fullname = "George Orwell",
                CountryId = 147,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author11 = new Author
            {
                Fullname = "Mark Twain",
                CountryId = 144,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author12 = new Author
            {
                Fullname = "Edgar Allan Poe",
                CountryId = 144,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author13 = new Author
            {
                Fullname = "Victor Hugo",
                CountryId = 138,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author14 = new Author
            {
                Fullname = "Plato",
                CountryId = 225,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author15 = new Author
            {
                Fullname = "Homer",
                CountryId = 225,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author16 = new Author
            {
                Fullname = "Oscar Wilde",
                CountryId = 191,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author17 = new Author
            {
                Fullname = "Ernest Hemingway",
                CountryId = 144,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author18 = new Author
            {
                Fullname = "Jane Austen",
                CountryId = 147,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var author19 = new Author
            {
                Fullname = "Miguel de Cervantes",
                CountryId = 129,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };            
            
            var author20 = new Author
            {
                Fullname = "Arthur Conan Doyle",
                CountryId = 147,
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };            
            
            var author21 = new Author
            {
                Fullname = "Tom Shippey",
                CountryId = 147,
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
