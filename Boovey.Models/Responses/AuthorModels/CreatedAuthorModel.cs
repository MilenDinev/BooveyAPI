﻿namespace Boovey.Models.Responses.AuthorModels
{
    public class CreatedAuthorModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Summary { get; set; }
        public int CountryId { get; set; }
    }
}
