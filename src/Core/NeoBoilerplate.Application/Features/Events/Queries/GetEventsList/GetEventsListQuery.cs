using NeoBoilerplate.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace NeoBoilerplate.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery: IRequest<Response<IEnumerable<EventListVm>>>
    {

    }
}
