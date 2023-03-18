
using MediatR;

namespace BestPracticesDemo.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQuery : IRequest<EventDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
