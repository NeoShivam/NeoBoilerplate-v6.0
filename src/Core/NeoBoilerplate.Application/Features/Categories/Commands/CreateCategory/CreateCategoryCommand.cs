using NeoBoilerplate.Application.Responses;
using MediatR;

namespace NeoBoilerplate.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand: IRequest<Response<CreateCategoryDto>>
    {
        public string Name { get; set; }
    }
}
