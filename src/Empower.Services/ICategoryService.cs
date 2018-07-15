using Empower.Domain.Client.Models;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Services
{
    public interface ICategoryService
    {
        CategorySearchResponse Search(CategorySearchRequest request);
        CategoryCreateResponse Add(CategoryCreateRequest request);
        CategoryDeleteResponse Delete(CategoryDeleteRequest request);
        CategoryUpdateResponse Update(CategoryUpdateRequest request);
        Category Get(int id);
    }
}
