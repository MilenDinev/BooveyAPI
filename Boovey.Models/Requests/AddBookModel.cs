﻿namespace Boovey.Models.Requests
{
    public class AddBookModel
    {
        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string PublicationDate { get; set; }
        public string ASIN { get; set; }
        public string ISBN { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Publisher  { get; set; }
        public  string Genres { get; set; }
        public  string Authors { get; set; }
    }
}
