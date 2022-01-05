using MediatR;

namespace NeoBoilerplate.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQuery: IRequest<EventExportFileVm>
    {
    }
}
