using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Empower.Domain.Client.Models
{
    public class Film
    {
        
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ReleaseYear { get; set; }
       
        public string Language { get; set; }
        public string OriginalLanguage { get; set; }

        [Required]
        public byte RentalDuration { get; set; }
        [Required]
        public short Length { get; set; }
        [Required]
        public decimal RentalRate { get; set; }
        [Required]
        public string Rating { get; set; }
        [Required]
        public string SpecialFeatures { get; set; }
        [Required]
        public decimal ReplacementCost { get; set; }
        public DateTime LastUpdate { get; set; }
        [Required]
        public int LanguageId { get; set; }
        public int? OriginalLanguageId { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Category> Categories { get; set; }
    }
}
