
using BestPracticesDemo.Application.Features.Events.Queries.GetEventsExport;
using BestPracticesDemo.Application.Models.Mail;

namespace BestPracticesDemo.Application.Contracts.Persistence
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}
