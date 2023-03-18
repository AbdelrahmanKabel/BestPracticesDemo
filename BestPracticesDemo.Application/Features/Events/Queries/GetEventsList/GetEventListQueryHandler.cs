
using AutoMapper;
using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Domain.Entities;
using MediatR;

namespace BestPracticesDemo.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, List<EventListVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        public GetEventListQueryHandler(IAsyncRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<List<EventListVm>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            var events = (await _eventRepository.GetAllAsync()).OrderBy( e => e.Date);
            var eventsDto = _mapper.Map<List<EventListVm>>(events);

            return eventsDto;
        }
    }
}
