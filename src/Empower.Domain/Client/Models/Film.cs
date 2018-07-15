using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReleaseYear { get; set; }
        public string Language { get; set; }
        public string OriginalLanguage { get; set; }
        public byte RentalDuration { get; set; }
        public short Length { get; set; }
        public decimal RentalRate { get; set; }
        public string Rating { get; set; }
        public string SpecialFeatures { get; set; }
        public decimal ReplacementCost { get; set; }
        public DateTime LastUpdate { get; set; }
        public int LanguageId { get; set; }
        public int? OriginalLanguageId { get; set; }
    }
}
