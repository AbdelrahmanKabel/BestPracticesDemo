using AutoMapper;
using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Application.Models.Mail;
using BestPracticesDemo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BestPracticesDemo.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;
        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService, ILogger<CreateEventCommandHandler> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new Exceptions.ValidationException(validationResult);

            var eventEntity = _mapper.Map<Event>(request);
            eventEntity = await _eventRepository.AddAsync(eventEntity);

            var email = new Email() { To = "test@gmail.com", Body = $"A new event was created: {request}", Subject = "A new event was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing anything else so this can be logged
                _logger.LogError($"Mailing about event {eventEntity.EventId} failed due to an error with the mail service: {ex.Message}");
            }

            return eventEntity.EventId;
        }
    }
}
