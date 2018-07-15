using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Requests
{
    public class FilmSearchRequest : SearchRequest
    {
        public string Title { get; set; }
        public int? LanguageId { get; set; }
        public int? CategoryId { get; set; }
    }
}
