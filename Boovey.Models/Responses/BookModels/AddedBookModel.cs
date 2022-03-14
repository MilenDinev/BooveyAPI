﻿namespace Boovey.Models.Responses.BookModels
{
    public class AddedBookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Genres { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }
        public string PublicationDate { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
