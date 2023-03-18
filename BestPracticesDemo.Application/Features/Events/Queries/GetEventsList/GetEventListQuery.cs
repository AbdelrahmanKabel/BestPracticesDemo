
using MediatR;

namespace BestPracticesDemo.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventListQuery : IRequest<List<EventListVm>>
    {

    }
}
