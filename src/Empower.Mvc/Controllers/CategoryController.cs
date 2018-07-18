using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Empower.Services;
using Empower.Domain.Client.Responses;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Models;

namespace Empower.Mvc.Controllers
{
    [Produces("application/json")]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        // api/category/list/{page?}
        [Route("list/{page?}")]
        public CategorySearchResponse List(int page = 1)
        {
            return _categoryService
                .Search(new CategorySearchRequest { PageNumber = page });
        }

        [HttpPost]
        [Route("create")]
        
        public CategoryCreateResponse Create([FromBody]CategoryCreateRequest request)
        {
            CategoryCreateResponse response;

            if (ModelState.IsValid)
            {
                response = _categoryService.Add(request);
            }
            else
            {
                response = new CategoryCreateResponse() { ErrorMessage = "Model state not valid" };
            }

            return response;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public CategoryDeleteResponse Delete(int id)
        {
            return _categoryService.Delete(new CategoryDeleteRequest() { Id = id });
        }

        [HttpGet]
        [Route("get/{id}")]
        public Category Get(int id)
        {
            return _categoryService.Get(id);
        }

        [HttpPut]
        [Route("update/{id}/{name}")]
        public CategoryUpdateResponse Update(int id, string name)
        {
            if ( ModelState.IsValid)
            {
                return _categoryService.Update(new CategoryUpdateRequest()
                {
                    Id = id,
                    Name = name
                });

            }
            else
            {
                return new CategoryUpdateResponse() { ErrorMessage = "Model doesn't contain enough information" };
            }
        }
    }
}