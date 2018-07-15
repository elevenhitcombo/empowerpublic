using Empower.Domain.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Responses
{
    public class FilmCreateResponse : BaseResponse
    {
        public Film Film { get; set; }
    }
}
