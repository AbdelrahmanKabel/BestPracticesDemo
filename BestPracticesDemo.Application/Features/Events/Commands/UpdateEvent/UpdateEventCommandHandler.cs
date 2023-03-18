using AutoMapper;
using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Domain.Entities;
using MediatR;

namespace BestPracticesDemo.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);
            _mapper.Map(request, eventEntity, typeof(UpdateEventCommand), typeof(Event));
            await _eventRepository.UpdateAsync(eventEntity);

            return Unit.Value;
        }
    }
}
