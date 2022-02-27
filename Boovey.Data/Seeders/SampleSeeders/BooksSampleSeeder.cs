namespace Boovey.Data.Seeders.SampleSeeders
{
    using System;
    using System.Threading.Tasks;
    using Entities.Books;

    public static class BooksSampleSeeder
    {
        public static async Task Seed(BooveyDbContext context)
        {
            var book1 = new Book
            {
                Id = 1,
                CoverUrl = "",
                Title = "The Lord of the Rings",
                Pages = 1248,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("17/04/2007", "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description ="none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book2 = new Book
            {
                Id = 2,
                CoverUrl = "",
                Title = "The Nature of Middle-earth",
                Pages = 464,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("02/09/2021", "dd/MM/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book3 = new Book
            {
                Id = 3,
                CoverUrl = "",
                Title = "Pictures by J.R.R. Tolkien",
                Pages = 112,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("11/11/2021", "dd/MM/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book4 = new Book
            {
                Id = 4,
                CoverUrl = "",
                Title = "Unfinished Tales of Numenor and Middle-Earth",
                Pages = 611,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("14/10/2021", "dd/MM/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book5 = new Book
            {
                Id = 5,
                CoverUrl = "",
                Title = "The Fellowship of the Ring: The Lord of the Rings",
                Pages = 456,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("26/12/2004", "dd/MM/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book6 = new Book
            {
                Id = 6,
                CoverUrl = "",
                Title = "The Hobbit",
                Pages = 368,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("29/08/2013", "dd/MM/yyyy",
               System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book7 = new Book
            {
                Id = 7,
                CoverUrl = "",
                Title = "The Children of Hurin",
                Pages = 326,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("01/08/2013", "dd/MM/yyyy",
               System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book8 = new Book
            {
                Id = 8,
                CoverUrl = "",
                Title = "The Return of the King: The Lord of the Rings",
                Pages = 464,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("17/10/2005", "dd/MM/yyyy",
               System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book9 = new Book
            {
                Id = 9,
                CoverUrl = "",
                Title = "The Two Towers: The Lord of the Rings",
                Pages = 418,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("20/04/2009", "dd/MM/yyyy",
               System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book10 = new Book
            {
                Id = 10,
                CoverUrl = "",
                Title = "The Hobbit (Enhanced Edition)",
                Pages = 322,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("15/11/2011", "dd/MM/yyyy",
               System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book11 = new Book
            {
                Id = 11,
                CoverUrl = "",
                Title = "Morgoth's Ring",
                Pages = 496,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("01/01/1995", "dd/MM/yyyy",
               System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book12 = new Book
            {
                Id = 12,
                CoverUrl = "",
                Title = "Tales from the Perilous Realm",
                Pages = 432,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("27/05/2021", "dd/MM/yyyy",
               System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book13 = new Book
            {
                Id = 13,
                CoverUrl = "",
                Title = "The Fall of Gondolin",
                Pages = 304,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("30/08/2018", "dd/MM/yyyy",
   System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book14 = new Book
            {
                Id = 14,
                CoverUrl = "",
                Title = "Letters of J R R Tolkien: A Selection",
                Pages = 514,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("01/04/2005", "dd/MM/yyyy",
   System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var book15 = new Book
            {
                Id = 15,
                CoverUrl = "",
                Title = "J. R. R. TOLKIEN: Author of the Century",
                Pages = 384,
                PublisherId = 1,
                PublicationDate = DateTime.ParseExact("07/05/2010", "dd/MM/yyyy",
   System.Globalization.CultureInfo.InvariantCulture),
                CountryId = 147,
                Description = "none",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            await context.Books.AddRangeAsync(book1, book2, book3, book4, book5, book6, book7, book8, book9, book10, book11, book12, book13, book14, book15);

            await context.SaveChangesAsync();
        }
    }
}
