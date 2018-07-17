using Empower.Services;
using System;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
using nh = NHibernate;
using en = Empower.NHibernate.Entities;
using Empower.Domain.Client.Models;
using NHibernate.Transform;
using Empower.NHibernate.Interfaces;

namespace Empower.NHibernate.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly nh.ISession _session;
        private readonly IRepository<en.Category> _categoryRepository;

        public CategoryService(
            nh.ISession session,
            IRepository<en.Category> categoryRepository)
        {
            _session = session;
            _categoryRepository = categoryRepository;
        }

        public CategoryCreateResponse Add(CategoryCreateRequest request)
        {
            var response = new CategoryCreateResponse();

            var existing = CheckForExisting(request.Name);

            if (existing != null)
            {
                response.ErrorMessage = "Category already exists";
            }
            else
            {
                var category = new en.Category
                {
                    Name = request.Name,
                    LastUpdate = DateTime.UtcNow
                };

                _session.Save(category);

                response.Category = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    LastUpdate = category.LastUpdate
                };

                response.CompletedAt = DateTime.UtcNow;
            }

            return response;
        }

        public CategoryDeleteResponse Delete(CategoryDeleteRequest request)
        {
            var response = new CategoryDeleteResponse();

            var existing = _categoryRepository.Get(request.Id);

            if (existing == null)
            {
                response.ErrorMessage = "Category not found";
            }
            else
            {
                _categoryRepository.Delete(existing);
                response.RowCount = _categoryRepository.GetQueryOver().RowCount();
                response.CompletedAt = DateTime.UtcNow;
            }

            return response;
        }

        public Category Get(int id)
        {
            en.Category categoryAlias = null;
            en.Film filmAlias = null;
         
            Film result = null;
            
            var repoCategory = _categoryRepository.Get(id);

            var category = new Category()
            {
                Id = repoCategory.Id,
                Name = repoCategory.Name,
                LastUpdate = repoCategory.LastUpdate
            };

            category.Films = 
                _session.QueryOver<en.Category>(() => categoryAlias)
                    .JoinAlias(ca => ca.Films, () => filmAlias)
                    .Where(c => c.Id == id)
                    .SelectList(list => list
                        .Select(() => filmAlias.Id).WithAlias(() => result.Id)
                        .Select(() => filmAlias.Title).WithAlias(() => result.Title)
                        .Select(() => filmAlias.ReleaseYear).WithAlias(() => result.ReleaseYear)
                        .Select(() => filmAlias.RentalDuration).WithAlias(() => result.RentalDuration)
                        .Select(() => filmAlias.Length).WithAlias(() => result.Length)
                        .Select(() => filmAlias.RentalRate).WithAlias(() => result.RentalRate)
                        .Select(() => filmAlias.Rating).WithAlias(() => result.Rating)
                        .Select(() => filmAlias.Description).WithAlias(() => result.Description)
                        .Select(() => filmAlias.LastUpdate).WithAlias(() => result.LastUpdate)
                        .Select(() => filmAlias.ReleaseYear).WithAlias(() => result.ReleaseYear)
                        .Select(() => filmAlias.ReplacementCost).WithAlias(() => result.ReplacementCost)
                        .Select(() => filmAlias.SpecialFeatures).WithAlias(() => result.SpecialFeatures)
                    )
                    .TransformUsing(Transformers.AliasToBean<Film>())
                    .List<Film>();

            return category;
        }

        public CategorySearchResponse Search(CategorySearchRequest request)
        {
            var response = new CategorySearchResponse();
            Category result = null;

            en.Category categoryAlias = null;

            var queryOver = _session.QueryOver<en.Category>(() => categoryAlias);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                queryOver.WhereRestrictionOn(c => c.Name).IsInsensitiveLike(request.Name, nh.Criterion.MatchMode.Anywhere);
            }

            var rowCountQuery =
               queryOver.Clone()
               .ToRowCountQuery()
               .FutureValue<int>();

            response.Items =
                 queryOver
                     .SelectList(list => list
                         .Select(() => categoryAlias.Id).WithAlias(() => result.Id)
                         .Select(() => categoryAlias.Name).WithAlias(() => result.Name)
                         .Select(() => categoryAlias.LastUpdate).WithAlias(() => result.LastUpdate)
                     )
                     .TransformUsing(Transformers.AliasToBean<Category>())
                     .List<Category>();

            response.TotalItems =
                rowCountQuery.Value;


            return response;

        }

        public CategoryUpdateResponse Update(CategoryUpdateRequest request)
        {
            var response = new CategoryUpdateResponse();
            var existing = CheckForExisting(request.Name);

            if (existing != null && existing.Id != request.Id)
            {
                response.ErrorMessage = "Update would create a duplicate";
            }
            else
            {
                var existingById = _categoryRepository.Get(request.Id);

                if (existingById == null)
                {
                    response.ErrorMessage = "Category not found";
                }
                else
                {
                    _session.Flush();
                    existingById.Name = request.Name;
                    existingById.LastUpdate = DateTime.UtcNow;
                    _categoryRepository.Update(existingById);

                    response.Category = new Category()
                    {
                        Id = existingById.Id,
                        Name = existingById.Name,
                        LastUpdate = existingById.LastUpdate
                    };
                }
            }

            return response;
        }

        private en.Category CheckForExisting(string category)
        {
            var existing = _session.QueryOver<en.Category>()
                .Where(c => c.Name == category)
                .SingleOrDefault();

            return existing;
        }
    }
}
