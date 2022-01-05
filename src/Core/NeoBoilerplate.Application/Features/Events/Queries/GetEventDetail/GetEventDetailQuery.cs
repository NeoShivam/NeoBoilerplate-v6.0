using NeoBoilerplate.Application.Responses;
using MediatR;
using System;

namespace NeoBoilerplate.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery: IRequest<Response<EventDetailVm>>
    {
        public string Id { get; set; }
    }
}
