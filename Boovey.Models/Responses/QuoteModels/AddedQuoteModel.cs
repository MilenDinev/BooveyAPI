﻿namespace Boovey.Models.Responses.QuoteModels
{
    public class AddedQuoteModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? AuthorId { get; set; }
        public int? BookId { get; set; }
    }
}
