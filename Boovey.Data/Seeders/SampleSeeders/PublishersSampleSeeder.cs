namespace Boovey.Data.Seeders.SampleSeeders
{
    using System;
    using System.Threading.Tasks;
    using Entities.Books;

    public class PublishersSampleSeeder
    {
        public static async Task Seed(BooveyDbContext context)
        {
            var publisher1 = new Publisher
            {
                Id = 1,
                Name = "HarperCollins",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher2 = new Publisher
            {
                Id = 2,
                Name = "BBC Worldwide Ltd",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher3 = new Publisher
            {
                Id = 3,
                Name = "Penguin Classics",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher4 = new Publisher
            {
                Id = 4,
                Name = "E-artnow",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher5 = new Publisher
            {
                Id = 5,
                Name = "Strelbytskyy Multimedia Publishing",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher6 = new Publisher
            {
                Id = 6,
                Name = "Throne Classics",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher7 = new Publisher
            {
                Id = 7,
                Name = "Quirk Books",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher8 = new Publisher
            {
                Id = 8,
                Name = "Canterbury Classics",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher9 = new Publisher
            {
                Id = 9,
                Name = "OUP Oxford",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher10 = new Publisher
            {
                Id = 10,
                Name = "Argo Classics",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher11 = new Publisher
            {
                Id = 11,
                Name = "Cambridge University Press",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher12 = new Publisher
            {
                Id = 12,
                Name = "Puffin",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher13 = new Publisher
            {
                Id = 13,
                Name = "Walker Books",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher14 = new Publisher
            {
                Id = 14,
                Name = "CreateSpace",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher15 = new Publisher
            {
                Id = 15,
                Name = "Wordsworth Editions",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher16 = new Publisher
            {
                Id = 16,
                Name = "Everyman",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher17 = new Publisher
            {
                Id = 17,
                Name = "Naxos",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher18 = new Publisher
            {
                Id = 18,
                Name = "Harper Perennial Modern Classics",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher19 = new Publisher
            {
                Id = 19,
                Name = "KTHTK",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };            
            
            var publisher20 = new Publisher
            {
                Id = 20,
                Name = "Firebird Distributing",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            var publisher9999 = new Publisher
            {
                Id = 99999,
                Name = "Independently published",
                CreatorId = 1,
                CreatedOn = DateTime.Now,
                LastModifierId = 1,
                LastModifiedOn = DateTime.Now,
            };

            await context.Publishers.AddRangeAsync(publisher1, publisher2, publisher3, publisher4, publisher5, publisher6, publisher7, publisher8, publisher9, publisher10, publisher11,
                publisher12, publisher13, publisher14, publisher15, publisher16, publisher17, publisher18, publisher19, publisher20, publisher9999);

            await context.SaveChangesAsync();
        }
    }
}

