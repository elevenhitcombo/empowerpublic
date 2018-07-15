using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Responses
{
    public abstract class SearchResponse<T> : BaseResponse
    {
        public IList<T> Items { get; set; }
        public int TotalItems { get; set; }
    }
}
