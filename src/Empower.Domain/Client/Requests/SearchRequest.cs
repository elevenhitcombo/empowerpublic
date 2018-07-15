using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Requests
{
    public abstract class SearchRequest
    {
        public bool IgnorePageSize { get; set; }
        public int ItemsPerPage { get; set; }
        public int PageNumber { get; set; }

        public SearchRequest()
        {
            ItemsPerPage = 100;
            PageNumber = 1;
        }
    }
}
