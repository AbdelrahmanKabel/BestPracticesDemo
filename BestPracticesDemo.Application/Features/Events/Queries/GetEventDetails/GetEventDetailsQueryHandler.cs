
using AutoMapper;
using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Domain.Entities;
using MediatR;

namespace BestPracticesDemo.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, EventDetailsVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public GetEventDetailsQueryHandler(IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(request.Id);
            var eventDetailsDto = _mapper.Map<EventDetailsVm>(eventEntity);

            var category = await _categoryRepository.GetByIdAsync(eventDetailsDto.CategoryId);

            eventDetailsDto.Category = _mapper.Map<CategoryDto>(category);
            
            return eventDetailsDto;
        }
    }
}
