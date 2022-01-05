using NeoBoilerplate.Application.Features.Events.Queries.GetEventsExport;
using System.Collections.Generic;

namespace NeoBoilerplate.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}
