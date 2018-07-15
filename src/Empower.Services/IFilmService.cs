using Empower.Domain.Client.Models;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Services
{
    public interface IFilmService
    {
        FilmSearchResponse Search(FilmSearchRequest request);
        FilmCreateResponse Add(FilmCreateRequest request);
        Film Get(int id);
        
    }
}
