using BestPracticesDemo.Application.Contracts.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPracticesDemo.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;

            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(e => e.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(DateTime.Now);

            RuleFor(e => e.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);
            RuleFor(e => e)
                .MustAsync(EventNameAndDateIsUnique)
                .WithMessage("There is an event with the same name and date");
        }

        private async Task<bool> EventNameAndDateIsUnique(CreateEventCommand eventEntity, CancellationToken cancellationToken)
        {
            return !(await _eventRepository.IsEventNameAndDateUnique(eventEntity.Name, eventEntity.Date));
        }
    }
}
