using NeoBoilerplate.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace NeoBoilerplate.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<Response<IEnumerable<CategoryListVm>>>
    {
    }
}
