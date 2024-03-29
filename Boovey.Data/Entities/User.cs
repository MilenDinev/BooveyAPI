﻿namespace Boovey.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Interfaces.IEntities;
    using Microsoft.AspNetCore.Identity;
    using Requests;

    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            this.FavoriteBooks = new HashSet<Book>();
            this.FavoriteGenres = new HashSet<Genre>();
            this.FavoriteAuthors = new HashSet<Author>();
            this.FavoriteQuotes = new HashSet<Quote>();
            this.Shelves = new HashSet<Shelve>();
            this.FavoriteShelves = new HashSet<Shelve>();
            this.Reviews = new HashSet<Review>();
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
            this.RequestsToApprove = new HashSet<Request>();
            this.SentRequests = new HashSet<Request>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NormalizedName { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifierId { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Book> FavoriteBooks { get; set; }
        public virtual ICollection<Genre> FavoriteGenres { get; set; }
        public virtual ICollection<Author> FavoriteAuthors { get; set; }
        public virtual ICollection<Quote> FavoriteQuotes { get; set; }
        public virtual ICollection<Shelve> Shelves { get; set; }
        public virtual ICollection<Shelve> FavoriteShelves { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Request> RequestsToApprove { get; set; }
        public virtual ICollection<Request> SentRequests { get; set; }
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Following { get; set; }
    }
}
