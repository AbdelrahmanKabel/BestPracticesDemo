using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Application.Features.Events.Queries.GetEventsExport;
using BestPracticesDemo.Application.Models.Mail;
using CsvHelper;

namespace BestPracticesDemo.Infrastructure.Mail
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(eventExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}
